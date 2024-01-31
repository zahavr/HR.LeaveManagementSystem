using AutoMapper;
using HR.LeaveManagementSystem.UI.Contracts;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using HR.LeaveManagementSystem.UI.Services.Base;

namespace HR.LeaveManagementSystem.UI.Services;

public class LeaveTypeService : BaseHttpService, ILeaveTypeService
{
    private readonly IMapper _mapper;

    public LeaveTypeService(
        IClient client,
        IMapper mapper,
        ILocalStorageService localStorageService)
        : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeViewModel>> GetLeaveTypes()
    {
        await AddBearerToken();
        ICollection<LeaveTypeDto> leaveTypes = await _client.LeaveTypesAllAsync();
        return _mapper.Map<List<LeaveTypeViewModel>>(leaveTypes);
    }

    public async Task<LeaveTypeViewModel> GetLeaveTypeDetails(int id)
    {
        await AddBearerToken();
        LeaveTypeDto leaveType = await _client.LeaveTypesGETAsync(id);
        return _mapper.Map<LeaveTypeViewModel>(leaveType);
    }

    public async Task<Response<int>> CreateLeaveTypes(LeaveTypeViewModel leaveTypeViewModel)
    {
        try
        {
            await AddBearerToken();
            CreateLeaveTypeCommand createLeaveTypeCommand = _mapper.Map<CreateLeaveTypeCommand>(leaveTypeViewModel);
            int id = await _client.LeaveTypesPOSTAsync(createLeaveTypeCommand);
            return new Response<int>
            {
                Success = true,
                Data = id
            };
        }
        catch (ApiException e)
        {
            return ConvertApiException<int>(e);
        }
    }

    public async Task<Response<Guid>> UpdateLeaveType(LeaveTypeViewModel leaveTypeViewModel)
    {
        try
        {
            await AddBearerToken();
            UpdateLeaveTypeCommand updateLeaveTypeCommand = _mapper.Map<UpdateLeaveTypeCommand>(leaveTypeViewModel);
            await _client.LeaveTypesPUTAsync(updateLeaveTypeCommand);
            return new Response<Guid>
            {
                Success = true,
            };
        }
        catch (ApiException e)
        {
            return ConvertApiException<Guid>(e);
        }
    }

    public async Task<Response<Guid>> DeleteLeaveType(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.LeaveTypesDELETEAsync(id);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException e)
        {
            return ConvertApiException<Guid>(e);
        }
    }
}