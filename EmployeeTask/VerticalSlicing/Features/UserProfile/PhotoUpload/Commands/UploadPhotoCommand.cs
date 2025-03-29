using MediatR;
using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;
using EmployeeTask.VerticalSlicing.Features.UserProfile.PhotoUpload.Helper;

namespace EmployeeTask.VerticalSlicing.Features.UserProfile.PhotoUpload.Commands
{
    public record UploadPhotoCommand(IFormFile ImageFile, int UserId) : IRequest<Result<bool>>;

    public class UploadPhotoCommandHandler : BaseRequestHandler<UploadPhotoCommand, Result<bool>>
    {
        public UploadPhotoCommandHandler(RequestParameters requestParameters) : base(requestParameters) { }
        public async override Task<Result<bool>> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            if (request.ImageFile == null || request.ImageFile.Length == 0)
                return Result.Failure<bool>(UserErrors.NoFileUploaded);

            // Use the DocumentSetting helper to upload the file
            var fileName = DocumentSetting.UploadFile(request.ImageFile, "UserPhotos");

            var attachment = new Attachments
            {
                FileId = fileName,
                Extend = Path.GetExtension(request.ImageFile.FileName),
                Type = "UserPhoto",
                Path = $"/uploads/UserPhotos/{fileName}"
            };

            await _unitOfWork.Repository<Attachments>().AddAsync(attachment);
            await _unitOfWork.SaveChangesAsync();

            // Associate the attachment with the user
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
            if (user == null)
                return Result.Failure<bool>(UserErrors.UserNotFound);


            user.PhotoId = attachment.Id; 
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(true);
        }
    }
}
