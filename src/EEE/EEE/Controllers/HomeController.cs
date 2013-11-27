using System.Linq;
using System.Web.Mvc;
using EEE.Domain.Entities;
using EEE.Domain.Repositories;
using EEE.Domain.Services;

namespace EEE.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IEntityRepository<Problem> _problemRepository;

        public HomeController(
            IEntityRepository<User> userRepository,
            IEntityRepository<Problem> problemRepository,
            IFormsAuthenticationService formsAuthenticationService)
            : base(userRepository, formsAuthenticationService)
        {
            _problemRepository = problemRepository;
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Index()
        {
            var problems = _problemRepository.FindAll().ToList();
            if (problems.Any())
            {
                return View(problems);    
            }

            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
    }
}