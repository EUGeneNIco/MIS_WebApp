using AutoMapper;
using MIS.Application._Mappings;
using MIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MIS.Application.Members.Queries.GetGuestGrid
{
    public class GuestGridItem : IHaveCustomMapping
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public long NetworkId { get; set; }
        public string Network { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Guest, GuestGridItem>()
                .ForMember(dm => dm.Name, mo => mo.MapFrom(s => $"{s.LastName}, {s.FirstName} {s.MiddleName}"))
                .ForMember(dm => dm.Network, mo => mo.MapFrom(s => s.Network.Name));
        }
    }


}
