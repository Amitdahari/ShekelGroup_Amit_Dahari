using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShekelGroup_Amit_Dahari.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShekelGroup_Amit_Dahari.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly Context _context;
        public GroupController(Context context)
        {
            _context = context;
        }

        [HttpGet("{groupCode}")]
        public ActionResult<List<GroupCustomerModel>> GetGroup(int groupCode)
        {
            var customers = _context.FactoriesToCustomers
             .Where(ftc => ftc.GroupCode == groupCode)
             .Join(_context.Customers,
                ftc => ftc.CustomerId,
                c => c.CustomerId,
                (ftc, c) => new { ftc, c })
             .Join(_context.Groups, join => join.ftc.GroupCode, g => g.GroupCode, (join, g) => new GroupCustomerModel
             {
                 GroupCode = g.GroupCode,
                 GroupName = g.GroupName,
                 CustomerId = join.c.CustomerId,
                 CustomerName = join.c.CustomerName
             }).ToList();

            if (!customers.Any())
            {
                return NotFound();
            }

            return Ok(customers);
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody]Customer customer, int groupCode, int factoryCode)
        {
            //var group = _context.CustomerGroup.FirstOrDefault(g => g.GroupCode == groupCode);

            // var factory = _context.Factories.FirstOrDefault(f => f.GroupCode == groupCode && f.FactoryCode == factoryCode);
            var group = _context.Groups.FirstOrDefault(g => g.GroupCode == groupCode);
            var factory = _context.Factories.FirstOrDefault(f => f.GroupCode == groupCode && f.FactoryCode == factoryCode);
            var factoriesToCustomer = _context.FactoriesToCustomers.FirstOrDefault(ftc => ftc.GroupCode == groupCode && ftc.FactoryCode == factoryCode);
            if (group != null && factory != null)
            {
                factoriesToCustomer.FactoryCode = factoryCode;
                factoriesToCustomer.GroupCode = groupCode;
                factoriesToCustomer.CustomerId = customer.CustomerId;
                _context.FactoriesToCustomers.Add(factoriesToCustomer);
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Group or Factory not found");
            }
        }
    }
}
