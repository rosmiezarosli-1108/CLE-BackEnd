using System.ComponentModel.DataAnnotations;

namespace CLE_BackEnd.DTOs.ContainerAddress;

public class ContainerAddressDto
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public int ContainerId { get; set; }
}

public class ContainerAddressCreateDto
{
    [Required]
    public string Address { get; set; } = string.Empty;

    [Required]
    public int ContainerId { get; set; }
}

public class ContainerAddressUpdateDto : ContainerAddressCreateDto
{
    [Required]
    public int Id { get; set; }
}