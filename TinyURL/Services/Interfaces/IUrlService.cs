using System.Collections.Generic;
using System.Threading.Tasks;
using TinyURL.Models;

namespace TinyURL.Services.Interfaces
{
    public interface IUrlService
    {

        Task<Link> RedirectionLink(string hash);
        IEnumerable<Link> GetLinks();
        Task<Link> AddRandomUrl(Link url);
        Task<Link> AddMyUrl(Link url);
        Task RemoveLink(int id);
        Task<Link> IsLongUrlExist(string url);
        Task<bool> IsShortUrlExist(string hash);
        int GetTotalCount();

    }
}
