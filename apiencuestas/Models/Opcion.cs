using System;
using System.Collections.Generic;

namespace apiencuestas.Models;

public partial class Opcion
{
    public int Id { get; set; }

    public int IdPregunta { get; set; }

    public string Opcion1 { get; set; } = null!;

    public int Orden { get; set; }

    public bool Correcta { get; set; }

    public virtual Preguntum IdPreguntaNavigation { get; set; } = null!;

    public virtual ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
}
