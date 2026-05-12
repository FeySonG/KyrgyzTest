using System.ComponentModel.DataAnnotations;

namespace KyrgyzTest.Application.Contracts.OldDbs.Regulations;

public class RegulationDto
{
    public int Id { get; set; }

    public string? Name { get; set; }
    
    public string? ShRegulation { get; set; }
}