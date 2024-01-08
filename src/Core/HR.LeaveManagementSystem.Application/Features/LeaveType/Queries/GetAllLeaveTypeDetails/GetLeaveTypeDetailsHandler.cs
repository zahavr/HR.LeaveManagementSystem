using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetAllLeaveTypeDetails;

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
        Domain.LeaveType leaveType = await _leaveTypeRepository.GetById(request.Id);
        
        if (leaveType is null)
            throw new NotFoundException(nameof(Domain.LeaveType), request.Id);
        
        LeaveTypeDetailsDto data = _mapper.Map<LeaveTypeDetailsDto>(leaveType);

        return data;
    }
}