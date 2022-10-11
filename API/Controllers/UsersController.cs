using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly DataContext _context;

        public UsersController(ILogger<UsersController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            return _context.Users is not null ? 
                await _context.Users.ToListAsync(): 
                new List<AppUser>();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            return _context.Users is not null ? 
                await _context.Users.FindAsync(id) : 
                new AppUser();
        }
    }
}