using Common.Models.BaseModels;
using Domain.Enum;
using Grpc.Core;
using GrpcUser;
using HandlerRequest.AspnetCoreAPI.UsersHandler.UsersHandlerModel;
using Infrastucture.EFCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using X.PagedList;
using X.PagedList.EF;
namespace NetCoreAPI_Mongodb.rRPCBase
{
    using GoogleFromDateTime = Google.Protobuf.WellKnownTypes.Timestamp;
    public class UserService : GrpcServiceUser.GrpcServiceUserBase
    {
        private readonly ILogger<UserService> _logger;
        private readonly StackOverflowDBContext _context;

        public UserService(ILogger<UserService> logger, StackOverflowDBContext context)
        {
            _logger = logger;
            _context = context;
        }


        #region Get Users

        public class GetUsersResponseModel : GetUserRequestModel
        {
            public string EmailHarsh { get; set; }

            public DateTime LastAccessDate { get; set; }

            public string Location { get; set; }

            public int Reputation { get; set; }

            public string WebsiteUrl { get; set; }

            public int? AccountId { get; set; }

            public IEnumerable<ExtendClassBadge> Badges { get; set; }

            public class ExtendClassBadge : GetUserRequestModel.Badge
            {
                public int UserId { get; set; }

                public DateTime Date { get; set; }
            }

        }

        public override async Task<GetUsersResponse> GetUsers(GrpcUser.Empty request, ServerCallContext context)
        {
            var response = new GetUsersResponse();
            IPagedList<GetUsersResponseModel> users = await _context.Users
                .Select(user => new GetUsersResponseModel
                {
                    AboutMe = user.AboutMe,
                    Age = user.Age,
                    CreationDate = user.CreationDate,
                    DisplayName = user.DisplayName,
                    DownVotes = user.DownVotes,
                    UpVotes = user.UpVotes,
                    Views = user.Views,
                    EmailHarsh = user.EmailHash,
                    LastAccessDate = user.LastAccessDate,
                    AccountId = user.AccountId,
                    Location = user.Location,
                    Reputation = user.Reputation,
                    WebsiteUrl = user.WebsiteUrl,
                    Badges = user.Badges.Select(badge => new GetUsersResponseModel.ExtendClassBadge
                    {
                        Name = badge.Name,
                        Date = badge.Date,
                        UserId = badge.UserId,

                    })
                })
                .ToPagedListAsync(1, 200);

            foreach (var user in users)
            {
                user.CreationDate = DateTime.SpecifyKind(user.CreationDate, DateTimeKind.Utc);
                user.LastAccessDate = DateTime.SpecifyKind(user.LastAccessDate, DateTimeKind.Utc);

                GrpcUser.UserModel userModel = new()
                {
                    DisplayName = user.DisplayName,
                    AboutMe = user.AboutMe,
                    Age = user.Age,
                    CreationDate = GoogleFromDateTime.FromDateTime(user.CreationDate),
                    DownVotes = user.DownVotes,
                    UpVotes = user.UpVotes,
                    Views = user.Views,
                    EmailHarsh = user.EmailHarsh,
                    LastAccessDate = GoogleFromDateTime.FromDateTime(user.LastAccessDate),
                    AccountId = user.AccountId,
                    Location = user.Location,
                    Reputation = user.Reputation,
                    WebsiteUrl = user.WebsiteUrl,
                };

                if (user.Badges is not null && user.Badges.Any())
                {
                    foreach (var badge in user.Badges)
                    {

                        GoogleFromDateTime timeStampDate = GoogleFromDateTime.FromDateTime(DateTime.SpecifyKind(badge.Date, DateTimeKind.Utc));

                        GrpcBadge.BadgeModel badgeModel = new()
                        {
                            Date = timeStampDate,
                            Name = badge.Name,
                            UserId = badge.UserId,
                        };

                        userModel.Badges.Add(badgeModel);
                    }
                }

                response.Users.Add(userModel);
            }

            return response;
        }

        #endregion


        #region Get User 

        public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var user = (GetUserRequestModel)null;
            try
            {
                user = await _context.Users.Where(user => user.Id == request.UserId).Select(user => new GetUserRequestModel
                {
                    DisplayName = user.DisplayName,
                    DownVotes = user.DownVotes,
                    CreationDate = user.CreationDate,
                    AboutMe = user.AboutMe,
                    Age = user.Age,
                    UpVotes = user.UpVotes,
                    Views = user.Views,
                    Badges = user.Badges.Select(badge => new GetUserRequestModel.Badge
                    {
                        Name = badge.Name,
                    })
                }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new RpcException(new Status(StatusCode.Internal, "Internal server error"));
            }

            if (user == null)
            {
                //var test = user.CreationDate.AddDays(1);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "User Not Found"));
            }

            var apiResponseModel = new ApiResponseModel<GetUserRequestModel>()
            {
                Data = user,
                StatusAsEnum = StatusResponseEnum.Success,
            };

            var response = new GetUserResponse()
            {
                Response = JsonConvert.SerializeObject(apiResponseModel)
            };

            return response;
        }
        #endregion
    }
}
