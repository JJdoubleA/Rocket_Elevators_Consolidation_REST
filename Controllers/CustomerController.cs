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

    // [HttpGet("{id}")]
    // public async Task<ActionResult<Customers>> GetCustomer(long id)
    // {
    //   var Customer = await _context.Customers.FindAsync(id);

    //   if (Customer == null)
    //   {
    //     return NotFound();
    //   }

    //   return Customer;
    // }

   

    // [HttpGet("{Email}")]
    // public  async  Task<ActionResult<bool>> InactiveCustomers(string Email)
    // {

      
    //   var Customers = await _context.Customers
    //       .Where(Customer => Customer.EmailOfCompanyContact == Email)
    //       .ToListAsync();
      
      
    //   if (!CustomerExists(Email))
    //   {
    //     return false;
    //   }
      
    //   return true;
    // }
    
    [HttpGet("{Email}")]
    public async Task<ActionResult<List<Customers>>> GetBatteryStatus(string Email)
    {
       var Customers = await _context.Customers
         .Where(Customer => Customer.EmailOfCompanyContact == Email)
        .ToListAsync();
      
        
      
      if (Customers == null)
      {
        return NotFound();
      }

      return Customers;
    }
    
    [HttpGet("{Email}/building")]
        public async Task<ActionResult<List<Buildings>>> GetBuilding(string Email, [FromBody] Customers customer )
        {
          var Customers = await _context.Customers
          .Where(Customer => Customer.EmailOfCompanyContact == Email)
          .ToListAsync();

          
          

          var building = await _context.Buildings.Where(b => b.CustomerId == customer.Id).ToListAsync();
          
            

            if (building == null)
            {
              
                return NotFound();
            }

            return building;
        }
       
       
        [HttpGet("{id}/battery")]
        public async Task<ActionResult<List<Batteries>>> GetBattery(long id)
        {
            var battery = await _context.Batteries.Where(b => b.BuildingId == id).ToListAsync();
            
            

            if (battery == null)
            {
              
                return NotFound();
            }

            return battery;
        }
        [HttpGet("{id}/column")]
        public async Task<ActionResult<List<Columns>>> GetColumn(long id)
        {
            var columns = await _context.Columns.Where(b => b.BatteryId == id).ToListAsync();
            
            

            if (columns == null)
            {
              
                return NotFound();
            }

            return columns;
        }
        [HttpGet("{id}/elevator")]
        public async Task<ActionResult<List<Elevators>>> GetElevator(long id)
        {
            var elevators = await _context.Elevators.Where(b => b.ColumnId == id).ToListAsync();
            
            

            if (elevators == null)
            {
              
                return NotFound();
            }

            return elevators;
        }
        


   

    private bool CustomerExists(string Email)
    {
      return _context.Customers.Any(e => e.EmailOfCompanyContact == Email);
    }
  }
}

