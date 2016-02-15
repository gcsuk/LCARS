using System.Collections.Generic;

namespace LCARS.Services
{
    public interface IGitHub<out T> where T : class
    {
        IEnumerable<T> Get(string url, string repository);

        int GetCount(string url, string repository);
    }
}