using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Norhtwind.UnitOfWork;

namespace Northwind.WebApi.Controllers
{
    [Route("[controller]")]
    public class NorthwindController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly IUnitOfWork _unitOfWork;

        public NorthwindController(ILoggerFactory loggerFactory, IUnitOfWork unitOfWOrk)
        {
            _logger = loggerFactory.CreateLogger(this.GetType().Name);
            _unitOfWork = unitOfWOrk;
        }
    }
}
