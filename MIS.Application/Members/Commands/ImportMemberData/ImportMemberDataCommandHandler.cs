using AutoMapper;
using MediatR;
using MIS.Application._Helpers;
using MIS.Application._Interfaces;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Commands.ImportMemberData
{
    public class ImportMemberDataCommandHandler : IRequestHandler<ImportMemberDataCommand, Unit>
    {
        private readonly IAppDbContext dbContext;
        private readonly IRepository<Member> memberRepository;
        private readonly IMapper mapper;

        public ImportMemberDataCommandHandler(IAppDbContext dbContext, IRepository<Member> memberRepository,
                                          IMapper mapper)
        {
            this.dbContext = dbContext;
            this.memberRepository = memberRepository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(ImportMemberDataCommand request, CancellationToken cancellationToken)
        {
            // Temporary
            var exitingRecords = await memberRepository.GetAllAsync();
            if (exitingRecords.Any())
            {
                this.dbContext.Members.RemoveRange(exitingRecords);
                await this.dbContext.SaveChangesAsync(cancellationToken);
            }

            var memberList = new List<Member>();

            var count = 1;
            foreach (var data in request.ImportedData)
            {
                memberList.Add(new Member
                {
                    MemberCode = CodeHelper.GenerateMemberCode(count),
                    FirstName = data.FirstName,
                    MiddleName = data.MiddleName,
                    LastName = data.LastName,
                    Address = data.Address,
                    Age = data.Age,
                    BirthDate = data.BirthDate?.Date,
                    Category = data.Category,
                    NetworkImported = data.NetworkImported == "YAN" ? "Y-AM" : data.NetworkImported,
                    Extension = data.Extension,
                    Gender = data.Gender,
                    CivilStatus = data.CivilStatus,
                    ContactNumber = data.ContactNumber,
                    City = data.City,
                    Barangay = data.Barangay,
                    Status = !string.IsNullOrEmpty(data.Status) ? data.Status : "Active",
                    ImportDate = DateTime.Now.Date
                });

                count += 1;
            }
            
            this.dbContext.Members.AddRange(memberList);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
