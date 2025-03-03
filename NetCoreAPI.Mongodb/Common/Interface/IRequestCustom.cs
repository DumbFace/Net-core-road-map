using Common.Models.BaseModels;
using MediatR;

namespace Common.Interface;

public interface IRequestCustom<T> : IRequest<ApiResponseModel<T>>
{
}
