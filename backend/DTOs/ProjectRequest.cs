namespace TimeManagementSystem;

public class ProjectRequest
{
    public int ProjectId { get; set; }
    
    public required int UserId { get; set; }
    
    public required string ProjectName { get; set; }

}