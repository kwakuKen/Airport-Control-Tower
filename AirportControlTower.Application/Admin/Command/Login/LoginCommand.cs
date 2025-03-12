using MediatR;

namespace AirportControlTower.Application.Admin.Command.Login;

public sealed record LoginCommand(string Username, string Password)
    : IRequest<LoginCommandResult>;