using MediatR;
using MIS.Application.Guests.Models;
using MIS.Application.Networks.Models;

namespace MIS.Application.Networks.Queries.GetNetworks
{
    public class GetNetworksQuery : IRequest<List<NetworkDto>>
    {

    }
}
