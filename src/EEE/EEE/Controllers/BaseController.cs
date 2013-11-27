using System.Linq;
using System.Web.Mvc;

using MongoDB.Bson;

using EEE.Domain.Services;
using EEE.Domain.Entities;
using EEE.Domain.Repositories;

namespace EEE.Controllers
{
    public class BaseController : Controller
    {
        public readonly IEntityRepository<User> _userRepository;
        public readonly IFormsAuthenticationService _formsAuthenticationService;

        public BaseController(
            IEntityRepository<User> userRepository,
            IFormsAuthenticationService formsAuthenticationService)
        {
            _userRepository = userRepository;
            _formsAuthenticationService = formsAuthenticationService;
        }

        public ActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                if (_currentUser != null) return _currentUser;

                ObjectId id;
                if (!User.Identity.IsAuthenticated || !ObjectId.TryParse(User.Identity.Name, out id)) return null;

                var user = _userRepository.AsQueryable().FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    _currentUser = user;
                    return _currentUser;
                }

                _formsAuthenticationService.SignOut();
                return null;
            }
        }

    }
}