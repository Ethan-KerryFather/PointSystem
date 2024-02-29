using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using PointSystem.Model;
using PointSystem.Model.Context;

namespace PointSystem.Controllers
{
    public class AddPoints
    {
        public int UserId { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly UsersContext _context;

        public PointsController(UsersContext context)
        {
            _context = context;
        }

        // GET: api/Points
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Points>>> GetPoints()
        {
            return await _context.Points.ToListAsync();
        }

        // GET: api/Points/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Points>> GetPoints(int id)
        {
            var points = await _context.Points.FindAsync(id);

            if (points == null)
            {
                return NotFound();
            }

            return points;
        }

        // PUT: api/Points/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoints(int id, Points points)
        {
            if (id != points.WalletId)
            {
                return BadRequest();
            }

            _context.Entry(points).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointsExists(id))
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

        // POST: api/Points
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Points>> PostPoints(Points points)
        {
            _context.Points.Add(points);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoints", new { id = points.WalletId }, points);
        }

        // POST : /makeWallet/4
        [HttpPost("/makeWallet/{userId}")]
        public async Task<ActionResult<Points>> MakeWallet(int userId)
        {
            var result = await _context.Points.AddAsync(new Points { 
                UserId = userId,
                
            });

            
            _context.SaveChanges();


            return CreatedAtAction(nameof(GetPoints), new {id = userId }, result.Entity);
        }


        // POST : /Points/Add/4
        [HttpPost("/AddPoints/{id}")]
        public async Task<ActionResult<Points>> PostPoints(int id)
        {
            var points = await _context.Points.FirstOrDefaultAsync(x => x.UserId == id);
            
            if(points == null)
            {
                return NotFound();
            }

            points.Balance += 100;

            try
            {
                await _context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(points);
       
        }

        // DELETE: api/Points/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoints(int id)
        {
            var points = await _context.Points.FindAsync(id);
            if (points == null)
            {
                return NotFound();
            }

            _context.Points.Remove(points);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PointsExists(int id)
        {
            return _context.Points.Any(e => e.WalletId == id);
        }
    }
}
