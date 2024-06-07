using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MIS.Application.Services.Models;
using MIS.Domain;

namespace MIS.Application.Services.Queries
{
    public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, List<ServiceDto>>
    {
        private readonly IAppDbContext dbContext;
        private readonly IMapper mapper;

        public GetServicesQueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<ServiceDto>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Services.Where(x => !x.IsDeleted && x.IsActive);

            return this.mapper.Map<List<ServiceDto>>(await query.ToListAsync(cancellationToken));
        }
    }
}
