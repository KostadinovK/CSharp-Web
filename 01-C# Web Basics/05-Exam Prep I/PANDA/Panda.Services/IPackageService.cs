using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Panda.Data.Enums;
using Panda.Data.Models;

namespace Panda.Services
{
    public interface IPackageService
    {
        void Create(string description, decimal weight, string shoppingAddress, string recipientName);

        IQueryable<Package> GetAllByStatus(PackageStatus status);

        void Deliver(string id);
    }
}
