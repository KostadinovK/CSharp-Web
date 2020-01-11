using System.Linq;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;
using SULS.App.ViewModels.Problems;
using SULS.Services;

namespace SULS.App.Controllers
{
    public class HomeController : Controller
    {
        private IProblemService problemService;

        public HomeController(IProblemService problemService)
        {
            this.problemService = problemService;
        }

        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return Redirect("/Home/Index");
        }

        public IActionResult Index()
        {
            if(User == null)
            {
                return this.View();
            }

            var problems = problemService.GetAll()
                .Select(p => new ProblemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Count = p.Submissions.Count
                });


            return this.View(problems, "IndexLoggedIn");
        }
    }
}