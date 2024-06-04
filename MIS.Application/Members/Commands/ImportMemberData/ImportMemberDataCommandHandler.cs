using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application._Enums;
using MIS.Application._Exceptions;
using MIS.Application._Helpers;
using MIS.Domain;
using MIS.Domain.Entities;

namespace MIS.Application.Members.Commands.ImportMemberData
{
    public class ImportMemberDataCommandHandler : IRequestHandler<ImportMemberDataCommand, Unit>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public ImportMemberDataCommandHandler(IAppDbContext dbContext,
                                          IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(ImportMemberDataCommand request, CancellationToken cancellationToken)
        {
            // Temporary
            var exitingRecords = dbContext.Members.ToList();
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
                    Birthdate = data.BirthDate?.Date,
                    Category = data.Category,
                    NetworkImported = data.NetworkImported,
                    Extension = data.Extension,
                    Gender = data.Gender,
                    CivilStatus = data.CivilStatus,
                    ContactNumber = data.ContactNumber,
                });

                count += 1;
            }
            
            this.dbContext.Members.AddRange(memberList);
            await this.dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
