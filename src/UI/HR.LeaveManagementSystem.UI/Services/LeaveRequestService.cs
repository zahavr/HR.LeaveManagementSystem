using AutoMapper;
using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveAllocations;
using HR.LeaveManagementSystem.UI.Models.LeaveRequests;
using HR.LeaveManagementSystem.UI.Services.Base;

namespace HR.LeaveManagementSystem.UI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    private readonly IMapper _mapper;

    public LeaveRequestService(
        IClient client,
        ILocalStorageService localStorageService,
        IMapper mapper)
        : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<AdminLeaveRequestViewViewModel> GetAdminLeaveRequestList()
    {
        ICollection<LeaveRequestListDto> leaveRequests = await _client.LeaveRequestAllAsync(isLoggedInUser: false);
        
        AdminLeaveRequestViewViewModel model = new()
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
            PendingRequests = leaveRequests.Count(q => q.Approved == null),
            RejectedRequests = leaveRequests.Count(q => q.Approved == false),
            LeaveRequests = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequests)
        };
        return model;
    }

    public async Task<EmployeeLeaveRequestViewModel> GetUserLeaveRequests()
    {
        var leaveRequest = await _client.LeaveRequestAllAsync(true);
        var allocations = await _client.LeaveAllocationsAllAsync(true);
        var model = new EmployeeLeaveRequestViewModel()
        {
            LeaveAllocations = _mapper.Map<List<LeaveAllocationViewModel>>(allocations),
            LeaveRequests = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequest)
        };
        return model;
    }

    public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestViewModel leaveRequest)
    {
        try
        {
            var response = new Response<Guid>();
            CreateLeaveRequestCommand createLeaveRequest = _mapper.Map<CreateLeaveRequestCommand>(leaveRequest);

            await _client.LeaveRequestPOSTAsync(createLeaveRequest);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiException<Guid>(ex);
        }
    }

    public async Task<LeaveRequestViewModel> GetLeaveRequest(int id)
    {
        LeaveRequestDetailsDto response = await _client.LeaveRequestGETAsync(id);
        return _mapper.Map<LeaveRequestViewModel>(response);
    }

    public Task DeleteLeaveRequest(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Guid>> ApproveLeaveRequest(int id, bool approved)
    {
        try
        {
            var response = new Response<Guid>();
            var request = new ChangeLeaveRequestApprovalCommand
            {
                Id = id,
                Approved = approved
            };
            await _client.UpdateApprovalAsync(request);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiException<Guid>(ex);
        };
    }

    public async Task<Response<Guid>> CancelLeaveRequest(int id)
    {
        try
        {
            var response = new Response<Guid>();
            var request = new CancelLeaveRequestCommand() { Id = id };
            await _client.CancelRequestAsync(request);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiException<Guid>(ex);
        }
    }
}