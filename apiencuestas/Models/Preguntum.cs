using System;
using System.Collections.Generic;

namespace apiencuestas.Models;

public partial class Preguntum
{
    public int Id { get; set; }

    public string Pregunta { get; set; } = null!;

    public int IdCuestionario { get; set; }

    public virtual Cuestionario IdCuestionarioNavigation { get; set; } = null!;

    public virtual ICollection<Opcion> Opcions { get; set; } = new List<Opcion>();
    public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
}
