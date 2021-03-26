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
  public class InterventionsController : ControllerBase
  {
    private readonly RailsApp_developmentContext _context;

    public InterventionsController(RailsApp_developmentContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Interventions>>> GetInterventions()
    {
      return await _context.Interventions.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Interventions>> GetIntervention(long id)
    {
      var Intervention = await _context.Interventions.FindAsync(id);

      if (Intervention == null)
      {
        return NotFound();
      }

      return Intervention;
    }

    [HttpGet("{id}/status")]
    public async Task<ActionResult<string>> GetInterventionStatus(long id)
    {
      var Intervention = await _context.Interventions.FindAsync(id);

      if (Intervention == null)
      {
        return NotFound();
      }

      return Intervention.Status;
    }

    [HttpGet("Pending")]
    public async Task<ActionResult<List<Interventions>>> PendingInterventions()
    {
      var Intervention = await _context.Interventions
          .Where(Intervention => Intervention.Status == "Pending")
          .ToListAsync();

      return Intervention;
    }
     

    [HttpPut("{id}")]
    public async Task<IActionResult> ChangeInterventionStatus(long id, [FromBody] Interventions Intervention)
    {
      var findIntervention = await _context.Interventions.FindAsync(id);

      if (Intervention == null)
      {
        return BadRequest();
      }

      if (findIntervention == null)
      {
        return NotFound();
      }

      if (Intervention.Status == findIntervention.Status)
      {
        ModelState.AddModelError("Status", "Looks like you didn't change the status.");
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      findIntervention.Status = Intervention.Status;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!InterventionExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    private bool InterventionExists(long id)
    {
      return _context.Interventions.Any(e => e.Id == id);
    }
  }
}