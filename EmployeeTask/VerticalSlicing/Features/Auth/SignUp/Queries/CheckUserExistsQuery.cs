using MediatR;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;

namespace EmployeeTask.VerticalSlicing.Features.Auth.SignUp.Queries
{
    public record CheckUserExistsQuery(string FullName, string Email) : IRequest<Result<bool>>;

    public class CheckUserExistsQueryHandler : BaseRequestHandler<CheckUserExistsQuery, Result<bool>>
    {
        public CheckUserExistsQueryHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public override async Task<Result<bool>> Handle(CheckUserExistsQuery request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Repository<User>()
                            .GetAsync(u => u.Email == request.Email || u.FullName == request.FullName);

            return Result.Success(existingUser.Any());
        }
    }
}
