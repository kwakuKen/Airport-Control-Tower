using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.Admin.Command.Login;

internal sealed class LoginCommandHandler(
    IAdminReadRepository adminReadRepository, 
    ILogger<LoginCommandHandler> logger)
    : IRequestHandler<LoginCommand, LoginCommandResult>
{
    public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
		try
		{
            var result = await adminReadRepository.GetUserDetails(request.Username, cancellationToken);
            if (result != null && result.VerifyPassword(request.Password))
            {
                return new LoginCommandResult(result.Username);
            }
        }
		catch (Exception ex)
		{
            logger.LogError(ex, ex.Message);
		}
        return default!;
    }
}
