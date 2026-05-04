using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLE_BackEnd.Models;

public class ContainerAddress
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Address { get; set; } = string.Empty;

    [Required] 
    public int ContainerId { get; set; }
    
    [ForeignKey("ContainerId")]
    public virtual Container? Container { get; set; }
}