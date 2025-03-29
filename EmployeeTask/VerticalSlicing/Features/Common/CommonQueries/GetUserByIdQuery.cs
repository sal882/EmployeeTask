using MediatR;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.UserProfile;

namespace EmployeeTask.VerticalSlicing.Features.Common.CommonQueries
{
    public record GetUserByIdQuery(int UserId) : IRequest<Result<User>>;
    public class GetUserByIdQueryHandler : BaseRequestHandler<GetUserByIdQuery, Result<User>>
    {
        public GetUserByIdQueryHandler(RequestParameters requestParameters) : base(requestParameters)
        {
        }

        public override async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository<User>().FirstAsync(u => u.Id == request.UserId && !u.IsDeleted);

            if (user == null)
            {
                return Result.Failure<User>(UserErrors.UserNotFound);
            }

            return Result.Success(user);
        }
    }
}
