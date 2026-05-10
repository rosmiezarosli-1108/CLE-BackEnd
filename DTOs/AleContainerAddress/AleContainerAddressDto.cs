using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.AleContainerAddress;

public class AleContainerAddressDto
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public int ContainerId { get; set; }
}

public class AleContainerAddressCreateDto
{
    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public int ContainerId { get; set; }
}

public class AleContainerAddressUpdateDto : AleContainerAddressCreateDto
{
    [Required] public int Id { get; set; }
}