using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StackExchange.Redis;

namespace JWTAuthentication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        RoleManager<IdentityRole> _RMObj; 
        public RolesController(ApplicationDbContext context, RoleManager<IdentityRole> RMObj)
        {
            _context = context;
            _RMObj = RMObj;
            
        }

        // GET: api/Roles
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
        {   
            return await _RMObj.Roles.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        [Route("{id}")]
        public async Task<ActionResult<IdentityRole>> GetRole(string id)
        {
            var roleModel = await _RMObj.FindByIdAsync(id);

            if (roleModel == null)
            {
                return NotFound();
            }

            return roleModel;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Route("update/{id}")]
        public async Task<IActionResult> PutRole(string id, IdentityRole RoleMdl)
        {
            if (id != RoleMdl.Id)
            {
                return BadRequest();
            }
            IdentityRole thisRole = await _RMObj.FindByIdAsync(RoleMdl.Id);
            thisRole.Name = RoleMdl.Name;
            await _RMObj.UpdateAsync(thisRole);
            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<IdentityRole>> PostRole(IdentityRole roleModel)
        {
            await _RMObj.CreateAsync(roleModel);
            return CreatedAtAction("GetRole", new { id = roleModel.Id }, roleModel);
        }

        
    }
}
