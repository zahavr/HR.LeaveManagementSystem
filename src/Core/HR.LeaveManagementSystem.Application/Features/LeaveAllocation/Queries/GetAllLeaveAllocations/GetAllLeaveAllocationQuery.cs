using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public record GetAllLeaveAllocationQuery(bool IsLoggedUser) : IRequest<List<LeaveAllocationDto>>;