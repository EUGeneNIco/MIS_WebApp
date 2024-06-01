using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Domain;

namespace MIS.Application.Members.Commands.UpdateMember
{
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, long>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateMemberCommandHandler(IAppDbContext dbContext,
                                          IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<long> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await dbContext.Members.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (member is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Member"));

            if (!string.IsNullOrWhiteSpace(request.ContactNumber) && dbContext.Members.Any(x => x.ContactNumber == request.ContactNumber && !x.IsDeleted && x.Id != request.Id))
                throw new DuplicateException(ErrorMessages.DuplicateRecordError("contact number"));

            member.ModifiedOn = DateTime.Now;

            member.FirstName = request.FirstName.Trim();
            member.LastName = request.LastName.Trim();
            member.MiddleName = request.MiddleName.Trim();
            member.Birthdate = request.BirthDate?.Date;
            member.Address = request.Address;
            member.Gender = request.Gender;
            member.ContactNumber = request.ContactNumber;
            member.CivilStatus = request.CivilStatus;
            member.Age = request.Age != null && request.Age > 0 ? request.Age : null;
            member.Extension = request.Extension;
            member.NetworkId = request.NetworkId;

            await dbContext.SaveChangesAsync(cancellationToken);

            return member.Id;
        }
    }
}
