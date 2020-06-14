using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AMR_Server.Application.Account.Commands.Login
{
    public class LoginCommand : IRequest<LoginCommand>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommand>
    {
        private readonly IAmrDbContext _context;
        private readonly IIdentityService _identityService;
        public LoginCommandHandler(IAmrDbContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        public async Task<LoginCommand> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //request.Token = await _identityService.Login(request.UserName, request.Password);
            //if (string.IsNullOrEmpty(request.Token))
            //{
            var data = await _context.City
  .ToListAsync(cancellationToken);
            request.Token =await _identityService.AuthenticateAD(request.UserName, request.Password);
            //}
            return request;
        }
    }
}
