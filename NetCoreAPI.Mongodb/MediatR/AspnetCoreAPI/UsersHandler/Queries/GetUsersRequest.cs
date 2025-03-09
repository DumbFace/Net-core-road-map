using Common.Models.BaseModels;
using HandlerRequest.AspnetCoreAPI.BadgesHandler.BadgeHandlerModel;
using HandlerRequest.AspnetCoreAPI.UsersHandler.UsersHandlerModel;
using Infrastucture.EFCore;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.EF;
namespace MediatR.AspnetCoreAPI.UsersHandler.Queries
{

    using GetUsersResponse = ApiResponseModel<IPagedList<ExtendRequestProperty>>;

    public class GetUsersRequest : IRequest<GetUsersResponse>
    {
    }

    public class ExtendRequestProperty : GetUserRequestModel
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

    public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly StackOverflowDBContext _context;

        public GetUsersHandler(StackOverflowDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetBadgeResponseModel.Badge>> GetBadges()
        {
            var badges = await _context.Badges
                .Select(badge => new GetBadgeResponseModel.Badge
                {
                    Name = badge.Name,
                })
                .ToListAsync();

            return badges;
        }

        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {

            var response = new GetUsersResponse();
            var query = _context.Users.Select(user => new ExtendRequestProperty
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
                Badges = user.Badges.Select(badge => new ExtendRequestProperty.ExtendClassBadge
                {
                    Name = badge.Name,
                    Date = badge.Date,
                    UserId = badge.UserId,
                })
            });

            response.Data = await query.ToPagedListAsync(1, 20);

            return response;
        }
    }
}
