using System.Web;
using System.Web.Mvc;

using EEE.Domain.Services;

namespace EEE.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            IUserService userService,
            IFormsAuthenticationService formsAuthenticationService)
            : base(userService, formsAuthenticationService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}