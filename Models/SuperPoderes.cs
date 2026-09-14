using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Scaffolding_inverso.Models;

public partial class SuperPoderes
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int HeroeId { get; set; }

    [ForeignKey("HeroeId")]
    [InverseProperty("SuperPoderes")]
    [ValidateNever]
    public virtual Heroes Heroe { get; set; } = null!;
}
