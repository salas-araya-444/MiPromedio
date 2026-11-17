using System.Collections.ObjectModel;

namespace MiPromedio
{
    public partial class MainPage : ContentPage
    {
        // Lista con todos los cursos de la app
        // Es ObservableCollection para que la lista en pantalla se actualice sola
        ObservableCollection<Curso> cursos = new ObservableCollection<Curso>();

        public MainPage()
        {
            InitializeComponent();

            // Se conecta la lista de cursos con el CollectionView del diseño
            ListaCursos.ItemsSource = cursos;
        }

        // Se ejecuta cada vez que esta pantalla aparece, también al volver del formulario
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ActualizarPromedio();
        }

        // Se ejecuta al tocar "+ Agregar curso", abre el formulario y le pasa la lista
        private async void OnAgregarClicked(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarCursoPage(cursos));
        }

        // Calcula el promedio ponderado como lo indica el reglamento de la UCR:
        // se multiplica la nota final (redondeada) de cada curso por sus créditos,
        // se suman y ese total se divide entre la suma de todos los créditos
        private void ActualizarPromedio()
        {
            double sumaNotasPorCreditos = 0;
            int totalCreditos = 0;

            foreach (Curso curso in cursos)
            {
                sumaNotasPorCreditos = sumaNotasPorCreditos + (curso.NotaFinal * curso.Creditos);
                totalCreditos = totalCreditos + curso.Creditos;
            }

            // Si no hay cursos el promedio queda en 0 para no dividir entre 0
            double promedio = 0;
            if (totalCreditos > 0)
            {
                promedio = sumaNotasPorCreditos / totalCreditos;
            }

            // Se muestra el promedio con dos decimales
            LabelPromedio.Text = promedio.ToString("0.00");

            // Se arma el texto de resumen, "curso" en singular si solo hay uno
            string textoCursos;
            if (cursos.Count == 1)
                textoCursos = "1 curso";
            else
                textoCursos = cursos.Count + " cursos";

            LabelResumen.Text = textoCursos + " - " + totalCreditos + " créditos";
        }
    }
}