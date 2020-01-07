using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Panda.Data;
using Panda.Data.Enums;
using Panda.Data.Models;

namespace Panda.Services
{
    public class PackageService : IPackageService
    {
        private readonly PandaDbContext context;
        private readonly IUsersService usersService;
        private readonly IReceiptsService receiptsService;

        public PackageService(PandaDbContext context, IUsersService usersService, IReceiptsService receiptsService)
        {
            this.context = context;
            this.usersService = usersService;
            this.receiptsService = receiptsService;
        }

        public void Create(string description, decimal weight, string shoppingAddress, string recipientName)
        {
            var userId = usersService.GetUserId(recipientName);

            if (userId == null)
            {
                return;
            }

            var package = new Package
            {
                Description = description,
                Weight = weight,
                ShippingAddress = shoppingAddress,
                RecipientId = userId,
                Status = PackageStatus.Pending
            };

            context.Packages.Add(package);
            context.SaveChanges();
        }

        public IQueryable<Package> GetAllByStatus(PackageStatus status)
        {
            return context.Packages.Where(p => p.Status == status);
        }

        public void Deliver(string id)
        {
            var package = context.Packages.Find(id);

            package.Status = PackageStatus.Delivered;
            context.SaveChanges();

            receiptsService.CreateFromPackage((decimal)package.Weight, package.RecipientId, package.Id);
        }
    }
}
