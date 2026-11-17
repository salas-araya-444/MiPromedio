namespace MiPromedio
{
    // Esta clase representa un curso del semestre
    public class Curso
    {
        // Datos que el usuario ingresa en el formulario
        public string Nombre { get; set; } = "";
        public int Creditos { get; set; }
        public double Nota { get; set; }

        // Nota final redondeada como lo indica el reglamento de la UCR (artículo 25)
        // Se redondea al múltiplo de 5 más cercano, y si queda justo en medio sube
        // Ejemplos: 62 -> 60, 62.5 -> 65, 63 -> 65, 67.5 -> 70
        public double NotaFinal
        {
            get
            {
                return Math.Floor((Nota + 2.5) / 5) * 5;
            }
        }

        // Estado del curso según la nota final redondeada
        // 70 o más = Aprobado, 60 o 65 = Ampliación, menos de 60 = Reprobado
        public string Estado
        {
            get
            {
                if (NotaFinal >= 70)
                    return "Aprobado";
                else if (NotaFinal >= 60)
                    return "Ampliación";
                else
                    return "Reprobado";
            }
        }

        // Color que se usa en la lista según el estado
        public Color ColorEstado
        {
            get
            {
                if (NotaFinal >= 70)
                    return Color.FromArgb("#2E9E5B"); // verde
                else if (NotaFinal >= 60)
                    return Color.FromArgb("#E08A1E"); // naranja
                else
                    return Color.FromArgb("#D64545"); // rojo
            }
        }
    }
}