using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeManagementSystem;

public class Project
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProjectId { get; set; }
    
    public required int UserId { get; set; }
    
    public User? User { get; set; }

    public required string ProjectName { get; set; }

}