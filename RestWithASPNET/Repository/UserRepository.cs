using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;

namespace RestWithASPNET.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlContext _context;
        private DbSet<User> dataset;

        public UserRepository(MySqlContext context)
        {
            _context = context;
            dataset = _context.Set<User>();
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return dataset.FirstOrDefault(u =>
                u.UserName.Equals(user.UserName) && u.Password.Equals(pass)
            );
        }

        public User ValidateCredentials(string username)
        {
            return dataset.SingleOrDefault(u => u.UserName.Equals(username));
        }

        public bool RevokeToken(string username)
        {
            var user = dataset.SingleOrDefault(u => u.UserName.Equals(username));

            if (user == null)
                return false;

            user.RefreshToken = null;
            _context.SaveChanges();

            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if (!dataset.Any(u => u.Id.Equals(user.Id)))
                return null;

            var result = dataset.SingleOrDefault(p => p.Id.Equals(user.Id));

            if (result == null)
                return null;

            try
            {
                _context.Entry(result).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
