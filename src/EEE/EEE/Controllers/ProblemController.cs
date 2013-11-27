using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using MongoDB.Bson;

using EEE.Domain.Entities;
using EEE.Domain.Repositories;
using EEE.Models;
using EEE.Domain.Services;
using EEE.Utils;
using MongoDB.Driver;


namespace EEE.Controllers
{
    public class ProblemController : BaseController
    {
        public readonly IEntityRepository<Problem> _problemRepository;

        public ProblemController(
            IEntityRepository<Problem> problemRepository,
            IEntityRepository<User> userRepository,
            IFormsAuthenticationService formsAuthenticationService)
            : base(userRepository, formsAuthenticationService)
        {
            _problemRepository = problemRepository;
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            ObjectId _id;
            if (!ObjectId.TryParse(id, out _id))
            {
                return RedirectToHome();
            }

            var problem = _problemRepository.AsQueryable().FirstOrDefault(x => x.Id == _id);
            if (problem == null)
            {
                return RedirectToHome();
            }

            return View(problem);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View(new ProblemModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(ProblemModel model)
        {
            if (!model.IsValid(model))
            {
                model.Msg = ConstHelper.FailMsg;
                return View(model);
            }

            var problemId = ObjectId.GenerateNewId();
            var problem = new Problem
            {
                Id = problemId,

                CreatedBy = CurrentUser.IdStr,
                UpdatedBy = CurrentUser.IdStr,

                Title = string.Empty,
                What = model.What.Trim(),
                When = model.When.Trim(),
                How = model.How.Trim(),
                Where = model.Where.Trim(),
                WhoEmail = CurrentUser.Email,
                WhoId = CurrentUser.IdStr,
                WhoName = string.Format("{0}{1}", CurrentUser.Name, string.IsNullOrEmpty(CurrentUser.Surname) ? string.Empty : string.Format(" {0}.", CurrentUser.Surname[0].ToString(ConstHelper.CultureTR))),

                ImageUrls = new List<string>(),
                YoutubeVideoIds = new List<string>()
            };
            var result = _problemRepository.Add(problem);
            if (result.Ok)
            {
                return Redirect(string.Format("/problem/detail/{0}", problem.IdStr));
            }

            model.Msg = ConstHelper.FailMsg;
            return View(model);
        }
    }
}