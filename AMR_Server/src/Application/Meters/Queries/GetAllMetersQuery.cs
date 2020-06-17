using AMR_Server.Application.Common.Interfaces;
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

namespace AMR_Server.Application.Meters.Queries
{
    public class GetAllMetersQuery : IRequest<IEnumerable<MeterDto>>
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string Keyword { get; set; }

        public class GetAllMetersQueryHandler : IRequestHandler<GetAllMetersQuery, IEnumerable<MeterDto>>
        {
            private readonly IAmrDbContext _context;
            private readonly IMapper _mapper;

            public GetAllMetersQueryHandler(IAmrDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<MeterDto>> Handle(GetAllMetersQuery request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.Keyword))
                {
                   return await _context.Meter
                       .Join(
                       _context.MeterVendor,
                       m => m.VendorId,
                       mv => mv.VendorId,
                      (m, mv) => new MeterDto()
                      {
                          SerialNo = m.SerialNo,
                          StreetName = m.StreetName,
                          VendorName = mv.VendorName,
                          VendorId = mv.VendorId
                      }
                       ).Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                       .ToListAsync(cancellationToken);
                }
                else
                {
                    return await _context.Meter
                        .Where(x => x.StreetName.Contains(request.Keyword) || x.Vendor.VendorName.Contains(request.Keyword))
                       .Join(
                       _context.MeterVendor,
                       m => m.VendorId,
                       mv => mv.VendorId,
                      (m, mv) => new MeterDto()
                      {
                          SerialNo = m.SerialNo,
                          StreetName = m.StreetName,
                          VendorName = mv.VendorName,
                          VendorId = mv.VendorId
                      }
                       ).Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize)
                       .ToListAsync(cancellationToken);
                }
            }
        }
    }

}
