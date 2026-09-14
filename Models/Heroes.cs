using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scaffolding_inverso.Models;

public partial class Heroes
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string? IdentidadSecreta { get; set; }

    [InverseProperty("Heroe")]
    public virtual ICollection<SuperPoderes> SuperPoderes { get; set; } = new List<SuperPoderes>();
}
