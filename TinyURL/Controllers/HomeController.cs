using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyURL.Models;
using TinyURL.Services.Interfaces;

namespace TinyURL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlService urlService;
        private readonly IDateService dateService;

        public HomeController(IUrlService urllService, IDateService dateService)
        {
            this.urlService = urllService;
            this.dateService = dateService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var links = urlService.GetLinks().ToList();
            return View(links);
        }


        [HttpGet]
        [Route("Add")]

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(Link url)
        {
            if (ModelState.IsValid)
            {
                if (url.ShortURL == null)
                {
                    Link isExist = await urlService.IsLongUrlExist(url.LongURL);
                    if (isExist == null)
                    {
                        var response = await GenerateRandomHash(url);
                        return View(response);
                    }
                    return View(isExist);
                }
                else
                {
                    if (!await urlService.IsShortUrlExist(url.ShortURL))
                    {
                        var response = await AddMyHash(url);
                        return View(response);
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(url.ShortURL), "Hash is exist");
                        url.ShortURL = null;
                        return View(url);
                    }
                }
            }
            return View(url);
        }


        private async Task<Link> GenerateRandomHash(Link url)
        {
            url.Added = dateService.CurrentDate();
            url = await urlService.AddRandomUrl(url);
            return url;
        }

        private async Task<Link> AddMyHash(Link url)
        {
            url.Added = dateService.CurrentDate();
            url = await urlService.AddMyUrl(url);
            return url;
        }


        [Route("{hash}")]
        public async Task<IActionResult> Link(string hash)
        {
            if (hash != null)
            {
                var link = await urlService.RedirectionLink(hash);
                if (link != null)
                {
                    return Redirect(link.LongURL);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error", "HomeController");
        }

        public async Task<IActionResult> RemoveLink(int id)
        {
            await urlService.RemoveLink(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
