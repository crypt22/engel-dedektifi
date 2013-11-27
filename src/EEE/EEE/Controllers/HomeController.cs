using System.Web;
using System.Web.Mvc;
using EEE.Domain.Entities;
using EEE.Domain.Repositories;
using EEE.Domain.Services;

namespace EEE.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            IEntityRepository<User> userRepository,
            IFormsAuthenticationService formsAuthenticationService)
            : base(userRepository, formsAuthenticationService)
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}