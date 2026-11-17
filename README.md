# MiPromedio

**Universidad de Costa Rica, Sede Regional del Pacífico**
**Bachillerato en Informática Empresarial**
**Curso:** Desarrollo de Software IV
**Investigación:** Investigación y demostración de frameworks para desarrollo móvil
**Framework asignado:** .NET MAUI

**Integrantes:**

- Esteban Salas Araya (C5J444)
- Aletia Ledezma Araya (C4G420)
- Yendri Jiménez Molina (C4G264)

**II Ciclo 2026**

---

## Descripción

MiPromedio es una aplicación móvil sencilla hecha con .NET MAUI y C# para demostrar cómo funciona este framework. La app permite registrar los cursos de un semestre con sus créditos y su nota, y calcula el promedio ponderado.

Lo que hace la app:

- Muestra una lista de los cursos registrados.
- Permite agregar cursos desde un formulario con 3 campos: nombre, créditos y nota final (de 0 a 100).
- Valida los datos antes de guardarlos y muestra un mensaje de error si algo está mal.
- Redondea la nota al múltiplo de 5 más cercano, como se hace en la UCR (por ejemplo 63 queda en 65 y 62 queda en 60).
- Muestra el estado de cada curso con un color: Aprobado (verde), Ampliación (naranja) o Reprobado (rojo).
- Calcula el promedio ponderado del semestre y la cantidad de cursos y créditos.

La app tiene dos pantallas:

1. **Pantalla principal:** promedio ponderado, lista de cursos y botón para agregar.
2. **Pantalla Agregar curso:** formulario con validación.

## Requisitos

Para abrir y ejecutar el proyecto se necesita:

- **Sistema operativo:** Windows 10 u 11.
- **Visual Studio 2026 Community** (gratis) con la carga de trabajo **"Desarrollo de la interfaz de usuario de aplicaciones multiplataforma de .NET"** (.NET MAUI).
- **.NET 11 SDK:** viene incluido con Visual Studio.
- **SDK de Android:** se instala con la misma carga de trabajo. La primera vez Visual Studio pide aceptar las licencias.
- **Para probar en Android**, una de estas dos opciones:
  - Un celular Android 7.0 o superior conectado por cable USB, con la depuración USB activada.
  - Un emulador de Android creado desde Visual Studio.
- **Git**, para clonar el repositorio.

## Instalación

1. Descargar Visual Studio 2026 Community desde https://visualstudio.microsoft.com/es/downloads/
2. En el instalador marcar la carga de trabajo **"Desarrollo de la interfaz de usuario de aplicaciones multiplataforma de .NET"** y dejar marcada la opción **"configuración de Android SDK"**. Luego dar "Instalar".
3. Clonar el repositorio:

   ```bash
   git clone https://github.com/salas-araya-444/MiPromedio.git
   ```

   Se recomienda guardarlo en una ruta corta y sin tildes ni espacios, por ejemplo `C:\Inv_DS4\`. En Windows las rutas muy largas pueden dar errores al compilar para Android.

4. Abrir el archivo `MiPromedio.slnx` con Visual Studio.
5. Esperar a que abajo diga **"Listo"**, porque la primera vez Visual Studio restaura los paquetes NuGet.

## Ejecución

### Desde Visual Studio

**En Windows (la forma más rápida para probar):**

1. Arriba, al lado del botón de play, escoger **Windows Machine**.
2. Dar play (F5).

**En un celular Android:**

1. En el celular activar las Opciones de desarrollador (tocar 7 veces "Versión del SO" en Ajustes → Sobre el teléfono).
2. En Opciones de desarrollador activar **Depuración USB**. En celulares Xiaomi/POCO también hay que activar **Instalar vía USB**.
3. Conectar el celular por cable, escoger "Transferencia de archivos" y aceptar el permiso de depuración.
4. En Visual Studio escoger **Android Local Devices** y seleccionar el celular.
5. Dar play (F5). La primera vez tarda varios minutos.

**En un emulador:**

1. Escoger **Android Emulators** y crear un emulador (por ejemplo un Pixel).
2. Dar play (F5).

### Desde la terminal

Desde la carpeta del proyecto:

```bash
# Compilar
dotnet build

# Ejecutar en Windows
dotnet build -t:Run -f net11.0-windows10.0.19041.0

# Ejecutar en un celular Android conectado o en un emulador abierto
dotnet build -t:Run -f net11.0-android
```

## Estructura del proyecto

```
MiPromedio/
├── Curso.cs                     Modelo: datos de un curso, redondeo, estado y color
├── MainPage.xaml                Diseño de la pantalla principal
├── MainPage.xaml.cs             Lógica de la pantalla principal y cálculo del promedio
├── AgregarCursoPage.xaml        Diseño del formulario
├── AgregarCursoPage.xaml.cs     Validaciones y guardado del curso
├── App.xaml / App.xaml.cs       Inicio de la app (modo claro siempre)
├── AppShell.xaml                Navegación y barra superior
├── MauiProgram.cs               Punto de entrada y configuración de la app
├── MiPromedio.csproj            Configuración del proyecto (plataformas, imágenes, versión)
├── Platforms/                   Código específico de cada sistema (Android, iOS, Windows, Mac)
├── Resources/
│   ├── Images/                  Imágenes (logo de la UCR)
│   ├── Styles/Colors.xaml       Colores de la app (paleta de la UCR)
│   ├── Styles/Styles.xaml       Estilos por defecto de los controles
│   ├── AppIcon/                 Ícono de la app
│   ├── Splash/                  Pantalla de carga
│   └── Fonts/                   Tipos de letra
├── DatosPrueba/                 Datos y casos para probar la app
└── Docs/                        Explicación técnica del código
```

En la carpeta `Docs` está el archivo `ExplicacionTecnica.md`, que explica con más detalle cada archivo, los controles que se usan, los eventos y el flujo de la app.

## Datos de prueba

En la carpeta `DatosPrueba` están:

- `CursosPrueba.txt`: 4 cursos de ejemplo con los resultados que debe mostrar la app.
- `CasosDePrueba.md`: pruebas de las validaciones del formulario.

## Tecnologías usadas

- .NET MAUI (.NET 11)
- C#
- XAML
- Visual Studio 2026 Community
- Git y GitHub

## Pruebas realizadas

La app se probó en:

- Windows 11 (Windows Machine).
- Celular Xiaomi POCO X6 Pro con Android 14.

## Limitaciones

- **Los datos no se guardan:** los cursos se guardan solo en memoria. Al cerrar la app la lista queda vacía otra vez.
- **No se pueden editar ni eliminar cursos:** solo se pueden agregar.
- **No usa base de datos ni API:** todo funciona dentro del celular, sin internet.
- **iOS y Mac no se probaron:** el proyecto tiene esas plataformas, pero para compilar en iOS se necesita una Mac con Xcode.
- **El promedio es una aproximación:** usa las notas redondeadas, pero no contempla casos especiales como cursos con nota menor a 50, que en la UCR se toman como 50 para el promedio ponderado.
- **La escala de la nota es de 0 a 100:** la UCR registra las notas de 0 a 10, pero la app usa 0 a 100 porque es como los estudiantes suelen ver su nota.

## Referencias

- Documentación oficial de .NET MAUI: https://learn.microsoft.com/dotnet/maui
- Novedades de .NET MAUI en .NET 11: https://learn.microsoft.com/dotnet/maui/whats-new/dotnet-11
- Política de soporte de .NET MAUI: https://dotnet.microsoft.com/platform/support/policy/maui
- Paleta de colores de la UCR: https://ci.ucr.ac.cr/sites/default/files/2025-10/Analisis%20de%20color%20paleta%20UCR%20accesible.pdf
