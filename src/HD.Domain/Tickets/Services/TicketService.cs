using HD.Domain.Core.Interfaces;
using HD.Domain.Core.Services;
using HD.Domain.Tickets.Entities;
using HD.Domain.Tickets.Enums;
using HD.Domain.Tickets.Interfaces;
using HD.Domain.Tickets.Validations;

namespace HD.Domain.Tickets.Services;

public class TicketService : BaseService, ITicketService
{
    private readonly ITicketRepository _repository;

    public TicketService(ITicketRepository repository, INotifier notifier) : base(notifier) => _repository = repository;

    public async Task Add(Ticket entity)
    {
        if (!ExecuteValidation(new TicketValidation(), entity)) return;

        await _repository.Add(entity);
    }

    public async Task Update(Ticket entity)
    {
        if (!ExecuteValidation(new TicketValidation(), entity)) return;

        await _repository.Update(entity);
    }

    public async Task Delete(Guid id)
    {
        var ticket = await _repository.GetById(id);

        if (ticket == null)
        {
            Notify("Ticket não existe");
            return;
        }

        if (ticket.Status != Status.Pending)
        {
            Notify("Esse ticket não pode ser removido");
            return;
        }

        await _repository.Delete(id);
    }


    public void Dispose() => _repository?.Dispose();
}
