using AMR_Server.Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.DeviceVendors.Queries
{
    public class GetDeviceVendorDetailsByIdQuery : IRequest<QuickDeviceVendorDto>
    {
        public int VendorId { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public class GetAllMetersQueryHandler : IRequestHandler<GetDeviceVendorDetailsByIdQuery, QuickDeviceVendorDto>
        {
            private readonly IAmrDbContext _context;
            private readonly IMapper _mapper;

            public GetAllMetersQueryHandler(IAmrDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<QuickDeviceVendorDto> Handle(GetDeviceVendorDetailsByIdQuery request, CancellationToken cancellationToken)
            {
                return await _context.MeterVendor
                         .ProjectTo<QuickDeviceVendorDto>(_mapper.ConfigurationProvider)
                         .SingleOrDefaultAsync(x => x.VendorId == request.VendorId);
            }
        }
    }

}
