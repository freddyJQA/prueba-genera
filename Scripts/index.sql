-- Índice para mejora de rendimiento sobre tabla Marcacion
CREATE INDEX IX_Marcacion_TrabajadorId_FechaHora
ON Marcacion (TrabajadorId, FechaHora);

-- Índice para evitar duplicados sobre la tabla Marcacion
CREATE UNIQUE INDEX UX_Marcacion_Trabajador_FechaHora_Tipo
ON Marcacion (TrabajadorId, FechaHora, Tipo)
WITH (IGNORE_DUP_KEY = ON);