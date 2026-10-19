namespace RobotApi.Models
{
    public class RobotEntity
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public int Energia { get; set; }

        public int NivelHabilidad { get; set; }

        public string Tipo { get; set; } = string.Empty;
    }
}
