using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.UserBasicDatas.Commands
{
    public class CreateUserBasicDataCommand : IRequest<short>
    {
        public short UserId { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string AspNetUserId { get; set; }

        public class CreateUserBasicDataCommandHandler : IRequestHandler<CreateUserBasicDataCommand, short>
        {
            private readonly IAmrDbContext _context;

            public CreateUserBasicDataCommandHandler(IAmrDbContext context)
            {
                _context = context;
            }

            public async Task<short> Handle(CreateUserBasicDataCommand request, CancellationToken cancellationToken)
            {
                UserBasicData user = new UserBasicData()
                {
                    UserName = request.UserName,
                    FirstName = request.FirstName
                };
                _context.UserBasicData.Add(user);
                await _context.SaveChangesAsync(cancellationToken);
                return user.UserId;
            }
        }
    }
}
