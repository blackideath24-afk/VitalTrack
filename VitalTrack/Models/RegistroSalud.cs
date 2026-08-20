using System.ComponentModel.DataAnnotations;

namespace VitalTrack.Models
{
    public class RegistroSalud
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, completa los campos necesarios.")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Por favor, completa los campos necesarios.")]
        public double Temperatura { get; set; }

        [Required(ErrorMessage = "Por favor, completa los campos necesarios.")]
        public int PresionSistolica { get; set; }

        [Required(ErrorMessage = "Por favor, completa los campos necesarios.")]
        public int PresionDiastolica { get; set; }

        [Required(ErrorMessage = "Por favor, completa los campos necesarios.")]
        public int Agua { get; set; }

        [Required(ErrorMessage = "Por favor, completa los campos necesarios.")]
        public double Sueno { get; set; }

        [Required(ErrorMessage = "Por favor, completa los campos necesarios.")]
        public int Actividad { get; set; }
    }
}