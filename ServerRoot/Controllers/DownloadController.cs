using Microsoft.AspNetCore.Mvc;


namespace ServerRoot.Controllers
{
    public class DownloadController : Controller
    {

        [Route("/download/TaskSchedulingSimulator")]
        public IActionResult TaskSchedulingSimulator() => File("/Assets/Files/Parallel Processors Simulator V2.zip", "application/x-zip-compressed", "Parallel -Processors-Simulator-V2.zip");
    }
}
   