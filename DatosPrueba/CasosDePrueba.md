# Casos de prueba - MiPromedio

Pruebas para revisar que la app funcione bien. Cada prueba dice qué hacer y qué debe pasar.

## Pantalla principal

| # | Prueba | Pasos | Resultado esperado |
|---|---|---|---|
| 1 | App sin cursos | Abrir la app | Promedio en 0,00, "0 cursos - 0 créditos" y el mensaje "Aún no hay cursos registrados..." |
| 2 | Ir al formulario | Tocar "+ Agregar curso" | Se abre la pantalla "Agregar curso" |
| 3 | Cancelar | En el formulario tocar "Cancelar" | Regresa a la pantalla principal sin agregar nada |
| 4 | Botón de regresar | En el formulario tocar la flecha de arriba o el botón atrás del celular | Regresa a la pantalla principal sin agregar nada |

## Validaciones del formulario

| # | Prueba | Nombre | Créditos | Nota | Resultado esperado |
|---|---|---|---|---|---|
| 5 | Todo vacío | (vacío) | (vacío) | (vacío) | Mensaje en rojo: "Debe escribir el nombre del curso..." |
| 6 | Nombre con solo espacios | (espacios) | 4 | 80 | Mensaje en rojo: "Debe escribir el nombre del curso..." |
| 7 | Créditos vacíos | Química | (vacío) | 80 | Mensaje en rojo: "Los créditos deben ser un número entero mayor a 0..." |
| 8 | Créditos en 0 | Química | 0 | 80 | Mensaje en rojo: "Los créditos deben ser un número entero mayor a 0..." |
| 9 | Créditos con decimal | Química | 2,5 | 80 | Mensaje en rojo: "Los créditos deben ser un número entero mayor a 0..." |
| 10 | Nota vacía | Química | 4 | (vacío) | Mensaje en rojo: "La nota debe ser un número entre 0 y 100..." |
| 11 | Nota mayor a 100 | Química | 4 | 150 | Mensaje en rojo: "La nota debe ser un número entre 0 y 100..." |
| 12 | Nota negativa | Química | 4 | -5 | Mensaje en rojo: "La nota debe ser un número entre 0 y 100..." |

En todos estos casos el curso **no se guarda** y la pantalla se queda en el formulario.

## Guardar cursos

| # | Prueba | Nombre | Créditos | Nota | Resultado esperado |
|---|---|---|---|---|---|
| 13 | Curso válido | Química | 4 | 80 | Sale "¡Curso agregado!", regresa a la pantalla principal y el curso aparece en la lista |
| 14 | Nota con coma | Física | 4 | 76,5 | Se guarda bien. Muestra "Nota obtenida: 76,5" y nota final 75 |
| 15 | Nota con punto | Física | 4 | 76.5 | Se guarda igual que con coma |
| 16 | Nota en 0 | Curso X | 4 | 0 | Se guarda. Nota final 0, Reprobado (rojo) |
| 17 | Nota en 100 | Curso Y | 4 | 100 | Se guarda. Nota final 100, Aprobado (verde) |

## Redondeo y estados

| # | Nota escrita | Nota final que debe mostrar | Estado |
|---|---|---|---|
| 18 | 67,5 | 70 | Aprobado (verde) |
| 19 | 67,4 | 65 | Ampliación (naranja) |
| 20 | 62,5 | 65 | Ampliación (naranja) |
| 21 | 62 | 60 | Ampliación (naranja) |
| 22 | 57,5 | 60 | Ampliación (naranja) |
| 23 | 57,4 | 55 | Reprobado (rojo) |

## Promedio ponderado

| # | Prueba | Resultado esperado |
|---|---|---|
| 24 | Agregar los 4 cursos de `CursosPrueba.txt` | 72,50 y "4 cursos - 16 créditos" |
| 25 | Agregar un solo curso | El texto dice "1 curso" en singular |
| 26 | Cursos con créditos diferentes (prueba extra de `CursosPrueba.txt`) | 80,00 y "2 cursos - 6 créditos" |
| 27 | Cerrar y volver a abrir la app | La lista queda vacía otra vez (los datos no se guardan, ver Limitaciones en el README) |
