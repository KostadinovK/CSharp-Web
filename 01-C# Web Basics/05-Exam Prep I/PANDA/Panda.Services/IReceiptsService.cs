using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Panda.Data.Models;

namespace Panda.Services
{
    public interface IReceiptsService
    {
        IQueryable<Receipt> GetAll();

        void CreateFromPackage(decimal weight, string recipientId, string packageId);
    }
}
