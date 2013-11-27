using System.Linq;
using System.Web.Mvc;

using MongoDB.Bson;

using EEE.Domain.Entities;
using EEE.Domain.Repositories;
using EEE.Models;
using EEE.Domain.Services;
using EEE.Utils;


namespace EEE.Controllers
{
    public class UserController : BaseController
    {
        public UserController(
            IEntityRepository<User> userRepository,
            IFormsAuthenticationService formsAuthenticationService)
            : base(userRepository, formsAuthenticationService)
        {

        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            return View(new LoginModel());
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            if (!model.IsValid(model))
            {
                ViewBag.Msg = ConstHelper.FailMsg;
                return View();    
            }

            var user = _userRepository.AsQueryable().FirstOrDefault(x => x.Email == model.Email && x.PasswordHash != null);
            if (user != null &&
                BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                _formsAuthenticationService.SignIn(user.IdStr, true);
                return RedirectToHome();
            }

            ViewBag.Msg = ConstHelper.FailMsg;
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                _formsAuthenticationService.SignOut();
            }

            return RedirectToHome();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

            return View(new SignupModel());
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult SignUp(SignupModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToHome();
            }

             if (!model.IsValid(model)
                || _userRepository.AsQueryable().Any(x => x.Email == model.Email))
            {
                model.Msg = ConstHelper.FailMsg;
                return View(model);
            }

            var memberId = ObjectId.GenerateNewId();
            var img = GravatarHelper.GetGravatarURL(model.Email, 35, "mm");

            var user = new User
            {
                Id = memberId,
                Email = model.Email.Trim(),
                Name = model.Name.Trim(),
                Surname = model.Surname.Trim(),
                Phone = model.Phone.Trim(),
                Image = img,
                CreatedBy = memberId.ToString(),
                UpdatedBy = memberId.ToString(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password, BCrypt.Net.BCrypt.GenerateSalt(12))
            };

            var result = _userRepository.Add(user);
            if (result.Ok)
            {
                _formsAuthenticationService.SignIn(user.IdStr, true);
                return RedirectToHome();
            }

            model.Msg =  ConstHelper.FailMsg;
            return View(model);
        }
    }
}