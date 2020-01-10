using System.Linq;
using Musaca.Services;
using Musaca.Web.BindingModels.User;
using Musaca.Web.ViewModels.Order;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;

namespace Musaca.Web.Controllers
{
    public class UsersController : Controller
    {
        private IUserService userService;
        private IOrderService orderService;

        public UsersController(IUserService userService, IOrderService orderService)
        {
            this.userService = userService;
            this.orderService = orderService;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterBindingModel input)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Users/Register");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return Redirect("/Users/Register");
            }

            var userId = userService.CreateUser(input.Username, input.Password, input.Email);

            if (userId == null)
            {
                return Redirect("/Users/Register");
            }

            orderService.CreateOrder(userId);

            return this.Redirect("/Users/Login");
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginBindingModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Users/Login");
            }

            var user = userService.GetUserOrNull(input.Username, input.Password);

            if (user == null)
            {
                return this.Redirect("/Users/Login");
            }

            SignIn(user.Id, user.Username, user.Email);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Logout()
        {
            SignOut();

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Profile()
        {
            var orders = orderService.GetAllCompletedOrdersForUser(User.Id)
                .Select(o => new OrderProfileViewModel
                {
                    Id = o.Id,
                    Total = $"${o.Products.Sum(p => p.Product.Price):f2}",
                    IssuedOn = o.IssuedOn.ToString("dd/MM/yyyy"),
                    Cashier = o.Cashier.Username
                })
                .ToList();

            return this.View(orders);
        }
    }
}
