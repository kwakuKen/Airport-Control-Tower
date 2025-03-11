using MediatR;

namespace AirportControlTower.Application.Aircraft.Command.Intent;

public sealed record IntentCommand(
    string State,
    string CallSign) 
    :IRequest<int>;
