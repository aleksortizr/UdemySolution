using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Norhtwind.UnitOfWork;
using Northwind.Repositories;
using System;
using System.Linq;

namespace Northwind.WebApi.Controllers
{
    public class SupplierController : NorthwindController
    {
        public SupplierController(ILoggerFactory loggerFactory, IUnitOfWork unitOfWOrk) : base(loggerFactory, unitOfWOrk)
        {
        }

        [HttpGet("GetPaginatedList")]
        public IActionResult GetPaginatedList([FromQuery] int page, [FromQuery] int rows)
        {
            var suppliers = _unitOfWork.Supplier.SupplierPagedList(page, rows);
            if (suppliers == null || suppliers.Count() == 0)
                return Conflict();

            _logger.LogInformation($"{User.Identity.Name} has read suppliers list.");
            return Ok(suppliers);
        }
    }
}
