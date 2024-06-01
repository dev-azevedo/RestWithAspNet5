using Microsoft.AspNetCore.Http;
using RestWithASPNET.Data.VO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithASPNET.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}
