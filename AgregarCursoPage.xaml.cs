using System.Collections.ObjectModel;
using System.Globalization;

namespace MiPromedio
{
    public partial class AgregarCursoPage : ContentPage
    {
        // Lista de cursos que viene desde la pantalla principal
        ObservableCollection<Curso> cursos;

        // Al abrir esta pantalla se recibe la lista donde se va a guardar el curso
        public AgregarCursoPage(ObservableCollection<Curso> listaCursos)
        {
            InitializeComponent();
            cursos = listaCursos;
        }

        // Se ejecuta al tocar el botón "Guardar curso"
        private async void OnGuardarClicked(object? sender, EventArgs e)
        {
            // Se lee lo que el usuario escribió en cada campo
            string nombre = EntryNombre.Text ?? "";
            string textoCreditos = EntryCreditos.Text ?? "";
            string textoNota = EntryNota.Text ?? "";

            // Validación 1: el nombre no puede ir vacío
            if (nombre.Trim() == "")
            {
                MostrarError("Debe escribir el nombre del curso...");
                return;
            }

            // Validación 2: los créditos deben ser un número entero mayor a 0
            int creditos;
            if (int.TryParse(textoCreditos, out creditos) == false || creditos <= 0)
            {
                MostrarError("Los créditos deben ser un número entero mayor a 0...");
                return;
            }

            // Validación 3: la nota debe ser un número entre 0 y 100
            // Se cambia la coma por punto para que acepte 85,5 o 85.5
            double nota;
            textoNota = textoNota.Replace(",", ".");
            if (double.TryParse(textoNota, NumberStyles.Float, CultureInfo.InvariantCulture, out nota) == false || nota < 0 || nota > 100)
            {
                MostrarError("La nota debe ser un número entre 0 y 100...");
                return;
            }

            // Si todo está bien se oculta el mensaje de error
            LabelError.IsVisible = false;

            // Se crea el curso con los datos del formulario y se agrega a la lista
            Curso nuevo = new Curso();
            nuevo.Nombre = nombre.Trim();
            nuevo.Creditos = creditos;
            nuevo.Nota = nota;
            cursos.Add(nuevo);

            // Mensaje de confirmación y se regresa a la pantalla principal
            await DisplayAlertAsync("Curso agregado!", "El curso " + nuevo.Nombre + " se guardó correctamente!", "Aceptar");
            await Navigation.PopAsync();
        }

        // Se ejecuta al tocar "Cancelar", regresa sin guardar nada
        private async void OnCancelarClicked(object? sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        // Muestra el mensaje de error en rojo debajo del formulario
        private void MostrarError(string mensaje)
        {
            LabelError.Text = mensaje;
            LabelError.IsVisible = true;
        }
    }
}