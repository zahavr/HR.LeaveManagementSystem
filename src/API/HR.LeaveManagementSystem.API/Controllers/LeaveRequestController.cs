using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaveRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestController(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeaveRequestListDto>>> Get()
    {
        List<LeaveRequestListDto> leaverRequests = await _mediator.Send(new GetLeaveRequestListQuery());
        return leaverRequests;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
    {
        LeaveRequestDetailsDto leaveRequestDetails = await _mediator.Send(new GetLeaveRequestDetailsQuery(id));
        return Ok(leaveRequestDetails);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Post([FromBody] CreateLeaveRequestCommand createLeaveRequestCommand)
    {
        int result = await _mediator.Send(createLeaveRequestCommand);
        return Created(new Uri($"{Request.GetDisplayUrl()}/api/LeaveTypes/{result}"), result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Put([FromBody] UpdateLeaveRequestCommand leaveTypeCommand)
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
        await _mediator.Send(new DeleteLeaveRequestCommand(id));
        return NoContent();
    }

    [HttpPut]
    [Route("cancel-request")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequestCommand)
    {
        await _mediator.Send(cancelLeaveRequestCommand);
        return NoContent();
    }
    
    [HttpPut]
    [Route("update-approval")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand changeLeaveRequestApprovalCommand)
    {
        await _mediator.Send(changeLeaveRequestApprovalCommand);
        return NoContent();
    }
}