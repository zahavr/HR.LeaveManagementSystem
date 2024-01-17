﻿using MediatR;

namespace HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public record CancelLeaveRequestCommand(int Id) : IRequest<Unit>;