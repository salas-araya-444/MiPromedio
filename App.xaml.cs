using Microsoft.Extensions.DependencyInjection;

namespace MiPromedio
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // La app siempre se muestra en modo claro, aunque el celular esté en modo oscuro
            UserAppTheme = AppTheme.Light;
        }

        // Crea la ventana principal de la app y carga el AppShell (la navegación)
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}