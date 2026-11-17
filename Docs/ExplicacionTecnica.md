# Explicación técnica - MiPromedio

Este documento explica cómo está hecha la app: qué hace cada archivo, qué controles se usan, cómo funcionan los eventos y el recorrido de la app desde que se abre hasta que se calcula el promedio.

## 1. Cómo arranca la app

El orden en que se ejecutan los archivos al abrir la app es este:

1. **`MauiProgram.cs`**: es el punto de entrada. Crea la app con `MauiApp.CreateBuilder()`, le dice cuál es la clase principal (`App`) y registra los tipos de letra.
2. **`App.xaml.cs`**: en el constructor se pone `UserAppTheme = AppTheme.Light` para que la app siempre se vea en modo claro, aunque el celular esté en modo oscuro. Luego `CreateWindow` crea la ventana y carga el `AppShell`.
3. **`AppShell.xaml`**: maneja la navegación. Dice que la primera pantalla es `MainPage` y le da el color azul a la barra de arriba.
4. **`MainPage`**: es la pantalla que ve el usuario al abrir la app.

## 2. Archivos principales

### `Curso.cs` (el modelo)

Es una clase que representa un curso. Tiene tres datos que llena el usuario:

- `Nombre` (texto)
- `Creditos` (número entero)
- `Nota` (número con decimales, de 0 a 100)

Y tres propiedades que **no se guardan, se calculan** a partir de la nota:

- **`NotaFinal`**: la nota redondeada al múltiplo de 5 más cercano. Se calcula así:

  ```csharp
  Math.Floor((Nota + 2.5) / 5) * 5
  ```

  Se le suma 2,5 (la mitad de 5), se divide entre 5, se quitan los decimales con `Math.Floor` y se multiplica otra vez por 5. Ejemplo con 63: 65,5 / 5 = 13,1, se quitan los decimales y queda 13, y 13 x 5 = 65.

- **`Estado`**: devuelve "Aprobado" si la nota final es 70 o más, "Ampliación" si es 60 o 65, y "Reprobado" si es menos de 60.
- **`ColorEstado`**: devuelve verde, naranja o rojo según el estado. Se usa para pintar la etiqueta del estado en la lista.

### `MainPage.xaml` (diseño de la pantalla principal)

La pantalla está organizada con un `Grid` de 5 filas:

| Fila | Contenido |
|---|---|
| 0 | Logo de la UCR (`Image`) y el texto de la investigación (`Label`) |
| 1 | Tarjeta azul (`Border`) con el promedio ponderado y el resumen de cursos y créditos |
| 2 | Título "Mis cursos" y la lista de cursos (`CollectionView`) |
| 3 | Botón "+ Agregar curso" (`Button`) |
| 4 | Pie de página con los nombres de los integrantes |

La fila de la lista usa `*` en lugar de `Auto`. Eso significa que ocupa todo el espacio que sobra en la pantalla.

Arriba se usa `Shell.TitleView` para poner el título "Mi Promedio" centrado en la barra azul.

**La lista (`CollectionView`):**

- `ItemTemplate` / `DataTemplate`: es el molde de cada tarjeta. Se repite una vez por cada curso.
- Dentro del molde se usa **enlace de datos** (`Binding`). Por ejemplo `{Binding Nombre}` muestra el nombre del curso, y `{Binding ColorEstado}` pinta la etiqueta del estado con su color.
- `StringFormat` sirve para dar formato al texto. Por ejemplo `'{0} créditos'` muestra "4 créditos".
- `EmptyView`: es lo que se muestra cuando la lista está vacía ("Aún no hay cursos registrados...").

### `MainPage.xaml.cs` (lógica de la pantalla principal)

- **La lista de cursos** es un `ObservableCollection<Curso>`. Es una lista especial que avisa a la pantalla cada vez que se agrega algo, así la lista visual se actualiza sola.
- En el constructor se conecta la lista con el control visual:

  ```csharp
  ListaCursos.ItemsSource = cursos;
  ```

- **`OnAppearing`**: se ejecuta cada vez que la pantalla se muestra, también cuando se regresa del formulario. Ahí se llama a `ActualizarPromedio()`.
- **`OnAgregarClicked`**: es el evento del botón "+ Agregar curso". Abre el formulario y le pasa la lista:

  ```csharp
  await Navigation.PushAsync(new AgregarCursoPage(cursos));
  ```

- **`ActualizarPromedio`**: recorre los cursos con un `foreach`, multiplica la nota final de cada uno por sus créditos y lo suma. Al final divide entre el total de créditos. Si no hay cursos, el promedio queda en 0 para no dividir entre cero.

### `AgregarCursoPage.xaml` (diseño del formulario)

- Tres cajas de texto (`Entry`): `EntryNombre`, `EntryCreditos` y `EntryNota`. Cada una tiene un `x:Name` para poder leerla desde el código.
- `Keyboard="Numeric"` en créditos y nota, para que en el celular salga el teclado de números.
- `LabelError`: un texto en rojo que empieza oculto (`IsVisible="False"`) y se muestra cuando hay un error.
- Dos botones: "Guardar curso" (evento `OnGuardarClicked`) y "Cancelar" (evento `OnCancelarClicked`).

### `AgregarCursoPage.xaml.cs` (validaciones y guardado)

- El **constructor recibe la lista de cursos** de la pantalla principal. Así las dos pantallas trabajan con la misma lista.
- **`OnGuardarClicked`** hace las validaciones en orden. Si una falla, muestra el error y se detiene con `return`:
  1. El nombre no puede estar vacío.
  2. Los créditos tienen que ser un número entero mayor a 0. Se usa `int.TryParse`, que devuelve `false` si el texto no es un número, en vez de que la app se caiga.
  3. La nota tiene que ser un número de 0 a 100. Antes de convertirla se cambia la coma por punto (`Replace(",", ".")`), porque en Costa Rica se escribe "85,5" y C# espera "85.5".
- Si todo está bien, crea el objeto `Curso`, lo agrega a la lista con `cursos.Add(nuevo)`, muestra el mensaje "¡Curso agregado!" con `DisplayAlertAsync` y regresa con `Navigation.PopAsync()`.
- **`OnCancelarClicked`** regresa a la pantalla principal sin guardar nada.

### Otros archivos

- **`Resources/Styles/Colors.xaml`**: colores de la app. Se usaron los de la paleta oficial de la UCR (Azul `#02477B`, Azul oscuro `#0C344E`, Celeste `#00C0F3` y Celeste claro `#BAD2ED`). En el diseño se usan con `{StaticResource Primary}`, `{StaticResource AzulOscuro}`, etc. Si se cambia un color ahí, cambia en toda la app.
- **`Platforms/Android/Resources/values/colors.xml`**: color de la barra de estado del celular (donde está la hora y la batería).
- **`Resources/Images/logo_ucr.png`**: el logo. En `MiPromedio.csproj` se le puso un tamaño base (`BaseSize`) para que MAUI no genere versiones enormes de la imagen.
- **`MiPromedio.csproj`**: dice para qué plataformas se compila (Android, iOS, Mac y Windows), el nombre de la app y su versión.

## 3. Recorrido de la app (flujo)

```
Se abre la app
   |
   v
MauiProgram -> App -> AppShell -> MainPage
   |
   v
MainPage.OnAppearing -> ActualizarPromedio()  (muestra 0,00 y la lista vacía)
   |
   v
Usuario toca "+ Agregar curso"  -> evento OnAgregarClicked
   |
   v
Navigation.PushAsync -> se abre AgregarCursoPage (recibe la lista)
   |
   v
Usuario llena el formulario y toca "Guardar curso" -> evento OnGuardarClicked
   |
   |-- ¿Algún dato malo? -> se muestra LabelError en rojo y se queda en el formulario
   |
   v (todo bien)
cursos.Add(nuevo)  -> la lista de MainPage se actualiza sola (ObservableCollection)
   |
   v
"¡Curso agregado!" -> Navigation.PopAsync -> regresa a MainPage
   |
   v
MainPage.OnAppearing -> ActualizarPromedio()  (se recalcula el promedio)
```

## 4. Conceptos de .NET MAUI que se usan

| Concepto | Dónde se usa |
|---|---|
| XAML para el diseño y C# para la lógica | Cada pantalla tiene un `.xaml` y un `.xaml.cs` |
| Controles (`Label`, `Entry`, `Button`, `Image`, `Border`) | Las dos pantallas |
| Layouts (`Grid`, `VerticalStackLayout`) | Para acomodar los controles |
| Lista dinámica (`CollectionView`) | Lista de cursos |
| Enlace de datos (`Binding`) | Tarjetas de la lista |
| Eventos (`Clicked`) | Botones |
| Navegación (`PushAsync` / `PopAsync`) | Ir al formulario y regresar |
| Ciclo de vida (`OnAppearing`) | Recalcular el promedio |
| Recursos compartidos (`StaticResource`) | Colores |
| Shell | Barra superior y pantalla inicial |
