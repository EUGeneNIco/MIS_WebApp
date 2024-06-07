using MediatR;
using MIS.Application.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Application.Services.Queries
{
    public class GetServicesQuery : IRequest<List<ServiceDto>>
    {
    }
}
