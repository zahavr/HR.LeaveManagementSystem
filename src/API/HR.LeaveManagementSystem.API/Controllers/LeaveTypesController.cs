using HR.LeaveManagementSystem.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Models;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagementSystem.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeaveTypeDto>>> Get()
    {
        List<LeaveTypeDto> leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
        return leaveTypes;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
    {
        LeaveTypeDetailsDto leaveType = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
        return Ok(leaveType);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<int>> Post([FromBody] CreateLeaveTypeCommand createLeaveTypeCommand)
    {
        int result = await _mediator.Send(createLeaveTypeCommand);
        return Created(new Uri($"{Request.GetDisplayUrl()}/api/LeaveTypes/{result}"), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put([FromBody] UpdateLeaveTypeCommand leaveTypeCommand)
    {
        await _mediator.Send(leaveTypeCommand);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveTypeCommand(id));
        return NoContent();
    }
}