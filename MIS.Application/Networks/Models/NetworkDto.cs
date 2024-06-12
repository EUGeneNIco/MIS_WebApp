using AutoMapper;
using MIS.Application._Mappings;
using MIS.Domain.Entities;

namespace MIS.Application.Networks.Models
{
    public class NetworkDto : IHaveCustomMapping
    {
        public string Name { get; set; }
        public long Id { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Network, NetworkDto>();
        }
    }
}
