using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Models;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeaveAllocationDto>>> Get(bool isLoggerUser)
    {
        List<LeaveAllocationDto> leaveAllocations = await _mediator.Send(new GetAllLeaveAllocationQuery(isLoggerUser));
        return leaveAllocations;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        LeaveAllocationDetailsDto leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailsQuery(id));
        return Ok(leaveAllocation);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationCommand createLeaveAllocationCommand)
    {
        var response = await _mediator.Send(createLeaveAllocationCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put([FromBody] UpdateLeaveAllocationCommand leaveAllocationCommand)
    {
        await _mediator.Send(leaveAllocationCommand);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveAllocationCommand(id));
        return NoContent();
    }
}