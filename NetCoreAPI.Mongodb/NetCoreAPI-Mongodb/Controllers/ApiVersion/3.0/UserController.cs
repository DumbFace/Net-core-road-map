using Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Infrastucture.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAPI_Mongodb.Controllers.BaseController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreAPI_Mongodb.Controllers.ApiVersion._3._0
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class UserController : BaseController_v3
    {
        //public IUnitOfWork<StackOverflowDBContext> _unitOfWork;
        private readonly StackOverflowDBContext _context;

        public UserController(
            IUnitOfWork<StackOverflowDBContext> unitOfWork, StackOverflowDBContext context)
        {
            _context = context;
            //_unitOfWork = unitOfWork;
        }

        // GET: api/<UserController>
        //[HttpGet]
        //public  async  Task<User> Get()
        //{


        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            var user = await _context.Users.Where(user => user.Id == id)
                .Include(user => user.Badge)    
                .FirstOrDefaultAsync();
            return user;
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
