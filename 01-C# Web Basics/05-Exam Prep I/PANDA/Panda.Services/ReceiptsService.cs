using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Panda.Data;
using Panda.Data.Models;

namespace Panda.Services
{
    public class ReceiptsService : IReceiptsService
    {
        private readonly PandaDbContext context;

        public ReceiptsService(PandaDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Receipt> GetAll()
        {
            return context.Receipts.AsQueryable();
        }

        public void CreateFromPackage(decimal weight, string recipientId, string packageId)
        {
            var receipt = new Receipt
            {
                PackageId = packageId,
                RecipientId = recipientId,
                Fee = weight * 2.67M,
                IssuedOn = DateTime.UtcNow,
            };

            this.context.Receipts.Add(receipt);
            this.context.SaveChanges();
        }
    }
}
