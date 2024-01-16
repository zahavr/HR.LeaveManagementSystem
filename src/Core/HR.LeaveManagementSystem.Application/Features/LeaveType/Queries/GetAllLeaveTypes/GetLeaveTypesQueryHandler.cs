using AutoMapper;
using HR.LeaveManagementSystem.Application.Contracts.Logging;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Models;
using MediatR;
namespace HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetLeaveTypesQueryHandler(
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<GetLeaveTypesQueryHandler> logger,
        IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _leaveTypeRepository = leaveTypeRepository;
    }
    
    public async Task<List<LeaveTypeDto>> Handle(
        GetLeaveTypesQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Domain.LeaveType> leaveTypes = await _leaveTypeRepository.GetAllAsync();

        List<LeaveTypeDto> data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        _logger.LogInformation("Leave types were retrieved successfully");
        return data;
    }
}