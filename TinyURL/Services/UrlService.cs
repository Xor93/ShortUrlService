using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyURL.EntityFramework;
using TinyURL.Models;
using TinyURL.Services.Interfaces;

namespace TinyURL.Services
{
    public class UrlService : IUrlService
    {
        private readonly UrlDatabaseContext context;
        private readonly IHashService hashService;

        public UrlService(UrlDatabaseContext context, IHashService hashService)
        {
            this.context = context;
            this.hashService = hashService;
        }


        public async Task<Link> IsLongUrlExist(string url)
        {
            var link = await context.Links.FirstOrDefaultAsync(l => url == l.LongURL);
            return link;
        }


        public async Task<Link> AddRandomUrl(Link url)
        {
            string hash;
            while (true)
            {
                hash = await hashService.GenerateHash(url.LongURL);
                var isExist = await IsShortUrlExist(hash);
                if (!isExist)
                {
                    break;
                }
            }
            url.ShortURL = hash;
            await context.Links.AddAsync(url);
            await context.SaveChangesAsync();
            return url;
        }

        public async Task<Link> AddMyUrl(Link url)
        {
            await context.Links.AddAsync(url);
            await context.SaveChangesAsync();
            return url;
        }


        public IEnumerable<Link> GetLinks()
        {
            return  context.Links;
        }


        public async Task<Link> RedirectionLink(string hash)
        {
            var link = await context.Links.Where(l => hash == l.ShortURL).FirstOrDefaultAsync();
            if (link != null)
            {
                link.ClickCounter++;
                await context.SaveChangesAsync();
            }
            return link;
        }


        public async Task RemoveLink(int id)
        {
            var link = await context.Links.Where(l => id == l.Id).FirstOrDefaultAsync();
            context.Links.Remove(link);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsShortUrlExist(string hash)
        {
            var isExist = await context.Links.AnyAsync(l => hash == l.ShortURL);
            return isExist;
        }

        public int GetTotalCount()
        {
            return context.Links.Count();
        }
    }
}
