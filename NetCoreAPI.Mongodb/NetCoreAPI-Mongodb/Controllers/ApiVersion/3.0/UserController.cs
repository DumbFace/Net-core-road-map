using Common.Models.BaseModels;
using Domain.EFCore.Entites;

//using userModel = Common.Models.BaseModels;
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
        public async Task<Common.Models.BaseModels.User> Get(int id)
        //public async Task<Domain.EFCore.Entites.User> Get(int id)
        {
            var sql = @"select [user].Id, [user].AboutMe ,  
                        (select badge.Id, badge.UserId from [Badges] badge where badge.UserId = [user].Id for JSON PATH) as 'JsonBadges'
                        from [Users] [user] 
                        where [user].Id = 3";

            var query = _context.Database.SqlQueryRaw<Common.Models.BaseModels.User>(sql);


            var user = await query.FirstOrDefaultAsync();

            return user;

        }
    }
}
