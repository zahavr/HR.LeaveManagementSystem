using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using MediatR;
namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public GetLeaveTypesQueryHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    
    public async Task<List<LeaveTypeDto>> Handle(
        GetLeaveTypesQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Domain.LeaveType> leaveTypes = await _leaveTypeRepository.GetAllAsync();

        List<LeaveTypeDto> data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        return data;
    }
}