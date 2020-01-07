using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Panda.Data.Enums;
using Panda.Services;
using Panda.Web.ViewModels.Packages;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;

namespace Panda.Web.Controllers
{
    public class PackagesController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IPackageService packageService;

        public PackagesController(IUsersService usersService, IPackageService packageService)
        {
            this.usersService = usersService;
            this.packageService = packageService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var usernames = usersService.GetUsernames();

            return this.View(usernames);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(PackageInputModel package)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Packages/Create");
            }

            packageService.Create(package.Description, package.Weight, package.ShippingAddress, package.RecipientName);

            return Redirect("/Packages/Pending");
        }

        [Authorize]
        public IActionResult Pending()
        {
            var packages = packageService.GetAllByStatus(PackageStatus.Pending)
                .Select(p => new PackageViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Weight = p.Weight,
                    ShippingAddress = p.ShippingAddress,
                    RecipientName = p.Recipient.Username
                })
                .ToList();

            return this.View(new PackagesListViewModel { Packages = packages });
        }

        [Authorize]
        public IActionResult Delivered()
        {
            var packages = packageService.GetAllByStatus(PackageStatus.Delivered)
                .Select(p => new PackageViewModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Weight = p.Weight,
                    ShippingAddress = p.ShippingAddress,
                    RecipientName = p.Recipient.Username
                })
                .ToList();

            return this.View(new PackagesListViewModel { Packages = packages });
        }

        [Authorize]
        public IActionResult Deliver(string id)
        {
            packageService.Deliver(id);
            return this.Redirect("/Packages/Delivered");
        }
    }
}
