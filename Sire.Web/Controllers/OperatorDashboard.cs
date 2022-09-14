using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sire.Web.Controllers
{
    [Authorize]
    public class OperatorDashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
