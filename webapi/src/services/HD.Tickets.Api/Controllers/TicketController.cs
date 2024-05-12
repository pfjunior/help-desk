using HD.Core.Mediator;
using HD.Tickets.Api.Application.Commands;
using HD.Tickets.Domain.Interfaces;
using HD.WebApi.Core.Controllers;
using HD.WebApi.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HD.Tickets.Api.Controllers;

[Authorize]
[Route("api/ticket")]
public class TicketController : MainController
{
    private readonly IMediatorHandler _mediator;
    private readonly IAspNetUser _aspnetUser;
    private readonly ITicketRepository _ticketRepository;

    public TicketController(IMediatorHandler mediator, IAspNetUser aspnetUser, ITicketRepository ticketRepository)
    {
        _mediator = mediator;
        _aspnetUser = aspnetUser;
        _ticketRepository = ticketRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll()
    {
        return CustomResponse(await _ticketRepository.GetAllAsync());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateTicketCommand command)
    {
        command.UserId = _aspnetUser.GetUserId();
        return CustomResponse(await _mediator.SendCommand(command), HttpStatusCode.Created);
    }
}
