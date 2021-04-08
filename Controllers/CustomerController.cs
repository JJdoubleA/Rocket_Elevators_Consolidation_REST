using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevators_Consolidation_REST.Models;

namespace Rocket_Elevators_Consolidation_REST.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class CustomersController : ControllerBase
  {
    private readonly RailsApp_developmentContext _context;

    public CustomersController(RailsApp_developmentContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
    {
      return await _context.Customers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customers>> GetCustomer(long id)
    {
      var Customer = await _context.Customers.FindAsync(id);

      if (Customer == null)
      {
        return NotFound();
      }

      return Customer;
    }

   

    [HttpGet("checkifcustomer")]
    public async Task<ActionResult<List<Customers>>> InactiveCustomers()
    {
      var Customers = await _context.Customers
          .Where(Customer => Customer.EmailOfCompanyContact != null)
          .ToListAsync();

      return Customers;
    }

   

    private bool CustomerExists(long id)
    {
      return _context.Customers.Any(e => e.Id == id);
    }
  }
}