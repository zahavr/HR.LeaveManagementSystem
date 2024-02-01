using System.ComponentModel.DataAnnotations;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;

namespace HR.LeaveManagementSystem.UI.Models.LeaveAllocations
{
    public class LeaveAllocationViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Number Of Days")]
        public int NumberOfDays { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public int Period { get; set; }

        public int LeaveTypeId { get; set; }
        public LeaveTypeViewModel LeaveType { get; set; } = new();
    }
}
