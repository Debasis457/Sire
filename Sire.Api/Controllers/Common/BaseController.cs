using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sire.Api.Controllers.Common
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}