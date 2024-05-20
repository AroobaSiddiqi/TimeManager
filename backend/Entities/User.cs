using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeManagementSystem;
public class User
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int UserId { get; set; }


    public required string Username { get; set; }


    public required string PasswordHash { get; set; }

    // You can add more properties here as needed, such as email, name, etc.
}