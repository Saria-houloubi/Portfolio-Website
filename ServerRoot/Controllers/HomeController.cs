using Microsoft.AspNetCore.Mvc;


namespace ServerRoot.Controllers
{
    [Route("/[action]")]
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index() => View();
        public IActionResult About() => View();
        public IActionResult ContactMe() => View();
        public IActionResult Projects() => View();
        public IActionResult Work() => View();
        public IActionResult Youtube() => View();
    }
}
   