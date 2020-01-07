using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Panda.Services;
using Panda.Web.ViewModels.Receipts;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;

namespace Panda.Web.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IReceiptsService receiptsService;

        public ReceiptsController(IReceiptsService receiptsService)
        {
            this.receiptsService = receiptsService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var receipts = receiptsService
                .GetAll()
                .Select(r => new ReceiptViewModel
                {
                    Id = r.Id,
                    Fee = r.Fee,
                    IssuedOn = r.IssuedOn,
                    Recipient = r.Recipient.Username
                })
                .ToList();

            return this.View(receipts);
        }
    }
}
