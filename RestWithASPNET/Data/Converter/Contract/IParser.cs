using System.Collections.Generic;

namespace RestWithASPNET.Data.Converter.Contract
{
    public interface IParser<O, D>
    {
        D Parse(O origem);
        List<D> Parse(List<O> origem);
    }
}
