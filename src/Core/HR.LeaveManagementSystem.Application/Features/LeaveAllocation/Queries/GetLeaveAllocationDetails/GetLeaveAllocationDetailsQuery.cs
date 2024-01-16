using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public record GetLeaveAllocationDetailsQuery(int Id) : IRequest<LeaveAllocationDetailsDto>; 