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

namespace AMR_Server.Application.Meters.Queries
{
    public class GetAllMetersQuery : IRequest<IEnumerable<MeterDto>>
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

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
                //return (from m in _context.Meter
                //        join mv in _context.MeterVendor on m.VendorId equals mv.VendorId
                //        select new MeterDto()
                //        {
                //            SerialNo = m.SerialNo,
                //            StreetName = m.StreetName,
                //            VendorName = mv.VendorName,
                //            VendorId = mv.VendorId
                //        })
                //             .OrderBy(x => x.CreatedDate);
                //.ToListAsync(cancellationToken);

                var data = await _context.Meter
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
        ).ToListAsync(cancellationToken);
                return data;
            }
        }
    }

}
