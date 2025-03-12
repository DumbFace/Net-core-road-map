using Common.Enum;
using Domain.EFCore.Entites;
using Infrastucture.EFCore;
using MediatR;
using MediatR.AspnetCoreAPI.UsersHandler.Queries;
using Microsoft.AspNetCore.Mvc;
using NetCoreAPI_Mongodb.AuthorizeFilter;
using NetCoreAPI_Mongodb.Controllers.BaseController;
using Serilog;
using System.Reflection.Metadata.Ecma335;

namespace NetCoreAPI_Mongodb.Controllers.ApiVersion.v4
{
    //[Authorize]
    public class UsersController : BaseController_v4
    {
        private readonly ILogger<UsersController> _logger;

        private readonly StackOverflowDBContext _context;

        public UsersController(IMediator mediator, ILogger<UsersController> logger, StackOverflowDBContext context) : base(mediator)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        //[CustomAuthorize([UserPermissionsEnum.READ])]
        //public async Task<ApiResponseModel<IEnumerable<GetUserRequestModel>>> GetUsers()
        public async Task<IActionResult> GetUsers()
        {
            var response = await _mediator.Send(new GetUsersRequest());
            return Ok(response);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            //ExceptionRegionEncoder.
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            //var test = user.Badges.FirstOrDefault();


            return Ok();
        }

        //// PUT: api/Users/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Users
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}

        //// DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
