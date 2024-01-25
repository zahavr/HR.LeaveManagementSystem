using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagementSystem.UI.Models.LeaveTypes;

public class LeaveTypeViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required] 
    [Display(Name = "Default Number of days")]
    public int DefaultDays { get; set; }
}