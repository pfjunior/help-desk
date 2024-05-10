using FluentValidation.Results;
using HD.Core.Messages;
using HD.Tickets.Domain.Entities;
using HD.Tickets.Domain.Interfaces;
using MediatR;

namespace HD.Tickets.Api.Application.Commands;

public class TicketCommandHandler : CommandHandler, IRequestHandler<CreateTicketCommand, ValidationResult>
{
    private readonly ITicketRepository _ticketRepository;

    public TicketCommandHandler(ITicketRepository ticketRepository) => _ticketRepository = ticketRepository;

    // TODO: Transform in CA: Use Cases with CQRS
    public async Task<ValidationResult> Handle(CreateTicketCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var ticket = new Ticket(message.UserId, message.Subject, message.Description, message.Type, message.UserName, message.Email, message.Department);

        await _ticketRepository.AddAsync(ticket);

        return await PersistData(_ticketRepository.UnitOfWork);
    }
}
