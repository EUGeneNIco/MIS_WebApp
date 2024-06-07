using AutoMapper;
using MIS.Application._Mappings;
using MIS.Domain.Entities;

namespace MIS.Application.Services.Models
{
    public class ServiceDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Service, ServiceDto>();
        }
    }
}
