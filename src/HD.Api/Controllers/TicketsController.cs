using AutoMapper;
using HD.Api.ViewModels.Tickets;
using HD.Domain.Core.Interfaces;
using HD.Domain.Tickets.Entities;
using HD.Domain.Tickets.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HD.Api.Controllers;

[Route("api/tickets")]
public class TicketsController : MainController
{
    private readonly ITicketService _service;
    private readonly ITicketRepository _repository;
    private readonly IMapper _mapper;

    public TicketsController(ITicketService service, ITicketRepository repository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _service = service;
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<TicketViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<TicketViewModel>>(await _repository.GetAll());
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<TicketViewModel>> GetById(Guid id)
    {
        var result = await GetTicketById(id);

        if (result == null) return NotFound();

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<CreateTicketViewModel>> Create([FromBody] CreateTicketViewModel viewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _service.Add(new Ticket(viewModel.Title, viewModel.Description, viewModel.Type, viewModel.EmployeeId));

        return CustomResponse(HttpStatusCode.Created, viewModel);
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult<UpdateTicketViewModel>> Update(Guid id, [FromBody] UpdateTicketViewModel viewModel)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var entity = await _repository.GetById(id);
        if (entity == null) return NotFound();

        entity.AddSolution(viewModel.Solution);
        entity.SetStatus(viewModel.Status);
        entity.SetType(viewModel.Type);
        entity.SetPriority(viewModel.Priority);

        await _service.Update(entity);

        return CustomResponse(HttpStatusCode.NoContent);
    }


    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult<TicketViewModel>> Delete(Guid id)
    {
        await _service.Delete(id);

        return CustomResponse(HttpStatusCode.NoContent);
    }

    #region Private Methods
    private async Task<TicketViewModel> GetTicketById(Guid id) => _mapper.Map<TicketViewModel>(await _repository.GetById(id));
    #endregion
}
