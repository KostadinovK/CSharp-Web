using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.BindingModels.Submission;
using SULS.App.ViewModels.Submissions;
using SULS.Services;

namespace SULS.App.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly IProblemService problemService;
        private readonly ISubmissionService submissionService;

        public SubmissionsController(IProblemService problemService, ISubmissionService submissionService)
        {
            this.problemService = problemService;
            this.submissionService = submissionService;
        }

        [Authorize]
        public IActionResult Create(string id)
        {
            var problem = problemService.GetById(id);

            var viewModel = new SubmissionCreateModel
            {
                ProblemId = problem.Id,
                Name = problem.Name
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(SubmissionCreateBindingModel bindingModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/");
            }

            submissionService.CreateSubmissionToProblem(bindingModel.Code, bindingModel.ProblemId, User.Id);

            return Redirect("/");
        }
    }
}
