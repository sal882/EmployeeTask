using MediatR;
using EmployeeTask.VerticalSlicing.Data.Repository.Interface;
using EmployeeTask.VerticalSlicing.Common.AuditService;

namespace EmployeeTask.VerticalSlicing.Common
{
    public class RequestParameters
    {
       public EmailSenderHelper EmailSenderHelper { get; set; }

        public IMediator Mediator { get; set; }
        public UserState UserState { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public IConfiguration Configuration { get; set; }
        public IAuditService AuditService { get; set; }

        public RequestParameters(IConfiguration configuration, IMediator mediator, UserState userState, IUnitOfWork unitOfWork , EmailSenderHelper emailSenderHelper,IAuditService auditService)
        {
            Mediator = mediator;
            UserState = userState;
            UnitOfWork = unitOfWork;
            EmailSenderHelper = emailSenderHelper;
            Configuration = configuration;
            AuditService = auditService;
        }
    }
}
