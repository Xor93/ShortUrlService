using System.Threading.Tasks;

namespace TinyURL.Services.Interfaces
{
    public interface IHashService
    {
        Task<string> GenerateHash(string url);
    }
}
