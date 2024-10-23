using System.Text.Json.Serialization;

namespace apiencuestas.Models
{
    public partial class Respuesta
    {
        public Guid Id { get; set; }

        public int IdPregunta { get; set; }

        public int IdOpcion { get; set; }

        [JsonIgnore]
        public virtual Preguntum? IdPreguntaNavigation { get; set; } = null!;

        [JsonIgnore]
        public virtual Opcion? IdOpcionNavigation { get; set; } = null!;
    }
}
