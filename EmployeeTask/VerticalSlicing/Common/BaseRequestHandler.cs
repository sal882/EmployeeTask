using MediatR;
using EmployeeTask.VerticalSlicing.Data.Repository.Interface;
using EmployeeTask.VerticalSlicing.Common.AuditService;

namespace EmployeeTask.VerticalSlicing.Common
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IMediator _mediator;
        protected readonly UserState _userState;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly EmailSenderHelper _emailSenderHelper;
        protected readonly IConfiguration _configuration;
        protected readonly IAuditService _auditService;
        public BaseRequestHandler(RequestParameters requestParameters)
        {
            _mediator = requestParameters.Mediator;
            _userState = requestParameters.UserState;
            _unitOfWork = requestParameters.UnitOfWork;
            _emailSenderHelper = requestParameters.EmailSenderHelper;
            _configuration = requestParameters.Configuration;
            _auditService = requestParameters.AuditService;

        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    }
}
