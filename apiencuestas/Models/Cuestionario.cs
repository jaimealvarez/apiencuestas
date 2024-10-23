using System;
using System.Collections.Generic;

namespace apiencuestas.Models;

public partial class Cuestionario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Preguntum> Pregunta { get; set; } = new List<Preguntum>();
}
