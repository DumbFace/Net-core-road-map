using Common.Models.BaseModels;
using HandlerRequest.AspnetCoreAPI.BadgesHandler.BadgeHandlerModel;
using HandlerRequest.AspnetCoreAPI.UsersHandler.UsersHandlerModel;
using Infrastucture.EFCore;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.EF;
using BadgeRequest = HandlerRequest.AspnetCoreAPI.UsersHandler.UsersHandlerModel.Badge;
namespace MediatR.AspnetCoreAPI.UsersHandler.Queries
{

    using GetUsersResponse = ApiResponseModel<IPagedList<GetUserRequestModel>>;

    public class GetUsersRequest : IRequest<GetUsersResponse>
    {
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
            var query = _context.Users.Select(user => new GetUserRequestModel
            {
                DisplayName = user.DisplayName,
                DownVotes = user.DownVotes,
                CreationDate = user.CreationDate,
                AboutMe = user.AboutMe,
                Age = user.Age,
                UpVotes = user.UpVotes,
                Views = user.Views,
                Badges = user.Badges.Select(badge => new BadgeRequest
                {
                    Name = badge.Name,
                })
            });

            response.Data = await query.ToPagedListAsync(1, 2);
            var badges = await GetBadges();


            return response;
        }
    }
}
