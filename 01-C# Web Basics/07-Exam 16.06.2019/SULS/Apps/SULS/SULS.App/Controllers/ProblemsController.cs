using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.BindingModels.Problem;
using SULS.App.ViewModels.Problems;
using SULS.App.ViewModels.Submissions;
using SULS.Services;

namespace SULS.App.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemService problemService;

        public ProblemsController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateBindingModel bindingModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Problems/Create");
            }

            var problemId = problemService.CreateProblem(bindingModel.Name, bindingModel.Points);

            if (problemId == null)
            {
                return Redirect("/Problems/Create");
            }

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var problem = problemService.GetById(id);

            var detailsViewModel = new ProblemDetailsViewModel
            {
                Name = problem.Name,
                MaxPoints = problem.Points,
                Submissions = problem.Submissions.Select(s => new SubmissionViewModel
                {
                    Id = s.Id,
                    AchievedResult = s.AchievedResult,
                    User = s.User.Username,
                    CreatedOn = s.CreatedOn.ToString("dd/MM/yyyy")
                })
                    .ToList()
            };

            return this.View(detailsViewModel);
        }
    }
}
