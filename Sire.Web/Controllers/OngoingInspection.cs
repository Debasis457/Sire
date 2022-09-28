using Microsoft.AspNetCore.Mvc;

namespace Sire.Web.Controllers
{
    public class OngoingInspection : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
