using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.MeterVendors.Queries
{
    public class GetVedndorDetailsByIdQuery : IRequest<MeterVendorDto>
    {
        public int VendorId { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public class GetAllMetersQueryHandler : IRequestHandler<GetVedndorDetailsByIdQuery, MeterVendorDto>
        {
            private readonly IAmrDbContext _context;
            private readonly IMapper _mapper;

            public GetAllMetersQueryHandler(IAmrDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<MeterVendorDto> Handle(GetVedndorDetailsByIdQuery request, CancellationToken cancellationToken)
            {
                return await _context.MeterVendor
                         .ProjectTo<MeterVendorDto>(_mapper.ConfigurationProvider)
                         .OrderBy(x => x.CreatedDate)
                         .SingleOrDefaultAsync(x => x.VendorId == request.VendorId);
            }
        }
    }

}
