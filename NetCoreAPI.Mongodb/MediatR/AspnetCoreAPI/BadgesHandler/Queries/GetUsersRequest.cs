using Common.Models.BaseModels;
using HandlerRequest.AspnetCoreAPI.BadgesHandler.BadgeHandlerModel;
using Infrastucture.EFCore;
using X.PagedList;
using X.PagedList.EF;

namespace MediatR.AspnetCoreAPI.UsersHandler.Queries
{

    using GetBadgeResponse = ApiResponseModel<IPagedList<GetBadgeResponseModel.Badge>>;

    public class GetBadgeRequest : IRequest<GetBadgeResponse>
    {
    }

    public class GetBadgeRequestHandler : IRequestHandler<GetBadgeRequest, GetBadgeResponse>
    {
        private readonly StackOverflowDBContext _context;

        public GetBadgeRequestHandler(StackOverflowDBContext context)
        {
            _context = context;
        }

        public async Task<GetBadgeResponse> Handle(GetBadgeRequest request, CancellationToken cancellationToken)
        {
            var response = new GetBadgeResponse();

            response.Data = await _context.Badges.Select(badge => new GetBadgeResponseModel.Badge
            {
                Name = badge.Name,
            })
            .ToPagedListAsync(1, 10);

            return response;
        }
    }
}
