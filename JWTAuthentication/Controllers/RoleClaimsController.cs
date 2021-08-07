using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JWTAuthentication.Authentication;

namespace JWTAuthentication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleClaimsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoleClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RoleClaims
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<RoleClaimsModel>>> GetRoleClaimsModel()
        {
            return await _context.RoleClaimsModel.ToListAsync();
        }

        // GET: api/RoleClaims/5
        [HttpGet("{id}")]
        [Route("{id}")]
        public async Task<ActionResult<RoleClaimsModel>> GetRoleClaimsModel(int id)
        {
            var roleClaimsModel = await _context.RoleClaimsModel.FindAsync(id);

            if (roleClaimsModel == null)
            {
                return NotFound();
            }

            return roleClaimsModel;
        }

        // PUT: api/RoleClaims/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Route("update/{id}")]
        public async Task<IActionResult> PutRoleClaimsModel(int id, RoleClaimsModel roleClaimsModel)
        {
            if (id != roleClaimsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(roleClaimsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleClaimsModelExists(id))
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

        // POST: api/RoleClaims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<RoleClaimsModel>> PostRoleClaimsModel(RoleClaimsModel roleClaimsModel)
        {
            _context.RoleClaimsModel.Add(roleClaimsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleClaimsModel", new { id = roleClaimsModel.Id }, roleClaimsModel);
        }

        // DELETE: api/RoleClaims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleClaimsModel(int id)
        {
            var roleClaimsModel = await _context.RoleClaimsModel.FindAsync(id);
            if (roleClaimsModel == null)
            {
                return NotFound();
            }

            _context.RoleClaimsModel.Remove(roleClaimsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleClaimsModelExists(int id)
        {
            return _context.RoleClaimsModel.Any(e => e.Id == id);
        }
    }
}
