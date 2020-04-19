using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Norhtwind.UnitOfWork;
using Northwind.Models;

namespace Northwind.WebApi.Controllers
{
    public class CustomerController : NorthwindController
    {
        public CustomerController(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork): base(loggerFactory, unitOfWork)
        {
        }

        [HttpGet]
        [Authorize]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unitOfWork.Customer.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(_unitOfWork.Customer.Insert(customer));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (_unitOfWork.Customer.Update(customer))
                return Ok( new { Message = "CUstomer updated successfully"});
            return Conflict();
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] Customer customer)
        {
            if (customer.Id > 0)
                return Ok(_unitOfWork.Customer.Delete(customer));
            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles="admin")]
        [Route("GetPaginatedCustomer/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedCustomer(int page, int rows)
        {
            if (!(page > 0 && rows > 0))
                return BadRequest(new { Message = "Page size or page index are wrong." });
            return Ok(_unitOfWork.Customer.CustomerPagedList(page, rows));
        }
    }
}
