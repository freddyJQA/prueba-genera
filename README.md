# Prueba Técnica - Genera

## T-SQL
1. Se adjunta script `Scripts/[dbo].[sp_calcula_horas_dia].sql` con las correcciones según requerimientos. Cada corrección está documentada dentro del script.
2. Se adjunta script `Scripts/index.sql`, el cual contiene 2 índices sobre la tabla `Marcacion`:
   * Se crea un UNIQUE INDEX para evitar registros duplicados. Se configuró `IGNORE_DUP_KEY = ON` para ignorar registros duplicados durante la carga.
   * Se crea un Nonclustered INDEX para mejorar el rendimiento de la consulta.

## C# .NET 8
1. Se presenta la solución para importar los registros a partir de un archivo CSV.
2. La cadena de conexión debe configurarse en `appsettings.json`.
4. El archivo `marcaciones.csv` debe ir en la carpeta `Data`, ubicada dentro del proyecto. La ruta para dicho archivo está configurada en `appsettings.json`, específicamente en la sección `CsvSettings`.
5. El archivo `rechazos.csv` se genera automáticamente en la carpeta `output` del directorio de ejecución de la aplicación. La ruta es:

```text
bin/
└── Debug/
    └── net8.0/
        └── output/
            └── rechazos.csv
```

### Decisiones técnicas

- Se utilizó Dapper para el acceso a datos.
- Se utilizó CsvHelper para la lectura del archivo CSV.
- Las inserciones se realizan mediante una transacción para mantener la integridad del lote.

## Uso de IA
Se utilizó IA `(Claude)` y `(ChatGPT)` como herramientas de apoyo durante el desarrollo para resolver dudas puntuales, revisar alternativas de implementación y mejorar la documentación.

Ejemplo: se utilizó para analizar alternativas de índices para el Stored Procedure y comparar su impacto mediante las herramientas de diagnóstico de SQL Server.
