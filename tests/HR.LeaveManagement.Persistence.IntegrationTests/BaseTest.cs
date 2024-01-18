﻿using HR.LeaveManagementSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class BaseTest
{
    protected readonly HrDatabaseContext _hrDatabaseContext;

    public BaseTest()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase("HrLeaveManagementSystemDatabase").Options;
        
        _hrDatabaseContext = new HrDatabaseContext(dbOptions);
    }
}