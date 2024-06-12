using AutoMapper;
using MediatR;
using MIS.Application._Interfaces;
using MIS.Application.Networks.Models;
using MIS.Domain.Entities;

namespace MIS.Application.Networks.Queries.GetNetworks
{
    public class GetNetworksQueryHandler : IRequestHandler<GetNetworksQuery, List<NetworkDto>>
    {
        private readonly IRepository<Network> networkRepository;
        private readonly IMapper mapper;

        public GetNetworksQueryHandler(IRepository<Network> networkRepository, IMapper mapper)
        {
            this.networkRepository = networkRepository;
            this.mapper = mapper;
        }
        public async Task<List<NetworkDto>> Handle(GetNetworksQuery request, CancellationToken cancellationToken)
        {
            var guest = await networkRepository.GetAllAsync();
            //if (guest is null)
                //return Enumerable.Empty<NetworkDto>();

            return mapper.Map<List<NetworkDto>>(guest);
        }
    }
}
