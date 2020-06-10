using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.Account.Commands.Register
{
    public class RegisterCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IAmrDbContext _context;
        private readonly IIdentityService _identityService;
        public RegisterCommandHandler(IAmrDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.Register(request.UserName, request.Password);
        }
    }
}
