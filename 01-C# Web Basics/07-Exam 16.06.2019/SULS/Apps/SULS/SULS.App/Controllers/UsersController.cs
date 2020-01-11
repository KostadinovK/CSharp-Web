using System.Linq;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SULS.App.BindingModels.User;
using SULS.App.ViewModels.Problems;
using SULS.Services;

namespace SULS.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IProblemService problemService;

        public UsersController(IUserService userService, IProblemService problemService)
        {
            this.userService = userService;
            this.problemService = problemService;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterBindingModel registerBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Users/Register");
            }

            if (registerBindingModel.ConfirmPassword != registerBindingModel.Password)
            {
                return Redirect("/Users/Register");
            }

            var userId = userService.CreateUser(registerBindingModel.Username, registerBindingModel.Email,
                registerBindingModel.Password);

            if (userId == null)
            {
                return Redirect("/Users/Register");
            }

           
            return Redirect("/Users/Login");
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginBindingModel bindingModel)
        {
            var user = userService.GetByUsernameAndPassword(bindingModel.Username, bindingModel.Password);

            if (user == null)
            {
                return Redirect("/Users/Login");
            }

            SignIn(user.Id, user.Username, user.Email);
            
            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Logout()
        {
            SignOut();

            return Redirect("/");
        }
    }
}