using MediatR;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Interfaces;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Commands.UpdateMember
{
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, long>
    {
        private readonly IRepository<Member> memberRepository;

        public UpdateMemberCommandHandler(IRepository<Member> memberRepository)
        {
            this.memberRepository = memberRepository;
        }
        public async Task<long> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await memberRepository.GetByIdAsync(request.Id);
            if (member is null)
                throw new NotFoundException(ErrorMessages.EntityNotFound("Member"));

            var dbMembers = await memberRepository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(request.ContactNumber) && dbMembers.Any(x => x.ContactNumber == request.ContactNumber && x.Id != request.Id))
                throw new DuplicateException(ErrorMessages.DuplicateRecordError("contact number"));

            member.ModifiedOn = DateTime.Now;

            member.FirstName = request.FirstName.Trim();
            member.LastName = request.LastName.Trim();
            member.MiddleName = request.MiddleName.Trim();
            member.BirthDate = request.BirthDate?.Date;
            member.Address = request.Address;
            member.Gender = request.Gender;
            member.ContactNumber = request.ContactNumber;
            member.Category = request.Category;
            member.CivilStatus = request.CivilStatus;
            member.Age = request.Age != null && request.Age > 0 ? request.Age : null;
            member.Extension = request.Extension;
            member.Status = request.Status;
            member.City = request.City;
            member.Barangay = request.Barangay;
            member.NetworkId = request.NetworkId;

            memberRepository.Update(member);
            await memberRepository.SaveChangesAsync(cancellationToken);

            return member.Id;
        }
    }
}
