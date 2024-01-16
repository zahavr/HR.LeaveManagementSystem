using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Models;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public GetLeaveTypeDetailsHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }
    
    public async Task<LeaveTypeDetailsDto> Handle(
        GetLeaveTypeDetailsQuery request,
        CancellationToken cancellationToken)
    {
        Domain.LeaveType? leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);
        
        if (leaveType is null)
            throw new NotFoundException(nameof(Domain.LeaveType), request.Id);
        
        LeaveTypeDetailsDto data = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

        return data;
    }
}