using Common.Models.BaseModels;
using Infrastucture.EFCore;
using Infrastucture.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAPI_Mongodb.Controllers.BaseController;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreAPI_Mongodb.Controllers.ApiVersion.v3
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
        public async Task<Common.Models.BaseModels.UserModel> Get(int id)
        //public async Task<Domain.EFCore.Entites.User> Get(int id)
        {

            //SQL Raw
            //var sql = @"select [user].Id, [user].AboutMe ,  
            //            (select badge.Id, badge.UserId from [Badges] badge where badge.UserId = [user].Id for JSON PATH) as 'JsonBadges'
            //            from [Users] [user] 
            //            where [user].Id = 3";

            //var query = _context.Database.SqlQueryRaw<Common.Models.BaseModels.User>(sql);


            //var user = await query.FirstOrDefaultAsync();

            var query = _context.Users.Where(user => user.Id == id)
                                  .Select(user => new Common.Models.BaseModels.UserModel
                                  {
                                      Id = user.Id,
                                      AboutMe = user.AboutMe,
                                      JsonBadges = user.Badges.Select(badge => new Badge
                                      {
                                          Id = badge.Id,
                                          UserId = badge.UserId
                                      }).ToString()
                                  });

            var user = await query.FirstOrDefaultAsync();
            //var query =
            //            from user_ in _context.Users
            //            join badge in _context.Badges on user_.Id equals badge.UserId into badgeGroup
            //            from badge in badgeGroup.DefaultIfEmpty()
            //            where user_.Id == id
            //            select new Common.Models.BaseModels.User
            //            {
            //                Id = user_.Id,
            //                AboutMe = user_.AboutMe,
            //                Badges = badgeGroup.Select(e => new Common.Models.BaseModels.Badge
            //                {
            //                    Id = e.Id,
            //                    UserId = e.Id,
            //                }),
            //            };
            //var user = await query.FirstOrDefaultAsync();

            return user;

        }
    }
}
