-- Ejecutar sobre la misma base SQLite indicada en appsettings.json.
-- Agrega la práctica sin migraciones ni cambios en las tablas anteriores.
PRAGMA foreign_keys = ON;
BEGIN TRANSACTION;

CREATE TABLE IF NOT EXISTS Ciudades (
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    Pais TEXT NOT NULL
);

CREATE UNIQUE INDEX IF NOT EXISTS UQ_Ciudades_Nombre ON Ciudades (Nombre);

CREATE TABLE IF NOT EXISTS Villanos (
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    Alias TEXT NOT NULL,
    Nivel_Amenaza INTEGER NOT NULL,
    Recompensa TEXT NOT NULL,
    FechaRegistro TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CiudadId INTEGER NOT NULL,
    CONSTRAINT CK_Villanos_NivelAmenaza CHECK (Nivel_Amenaza BETWEEN 1 AND 5),
    CONSTRAINT FK_Villanos_Ciudades FOREIGN KEY (CiudadId)
        REFERENCES Ciudades (Id) ON DELETE NO ACTION
);

CREATE INDEX IF NOT EXISTS IX_Villanos_CiudadId ON Villanos (CiudadId);

CREATE TABLE IF NOT EXISTS Cuarteles (
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    Nombre TEXT NOT NULL,
    Direccion TEXT NOT NULL,
    Capacidad INTEGER NOT NULL,
    CONSTRAINT CK_Cuarteles_Capacidad CHECK (Capacidad > 0)
);

CREATE UNIQUE INDEX IF NOT EXISTS UQ_Cuarteles_Nombre ON Cuarteles (Nombre);

CREATE TABLE IF NOT EXISTS Vehiculos (
    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    Nro_Placa TEXT NOT NULL,
    Modelo TEXT NOT NULL,
    CostoMantenimiento TEXT NOT NULL,
    FechaAlta TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CuartelId INTEGER NULL,
    CONSTRAINT FK_Vehiculos_Cuarteles FOREIGN KEY (CuartelId)
        REFERENCES Cuarteles (Id) ON DELETE SET NULL
);

CREATE UNIQUE INDEX IF NOT EXISTS UQ_Vehiculos_Placa ON Vehiculos (Nro_Placa);
CREATE INDEX IF NOT EXISTS IX_Vehiculos_CuartelId ON Vehiculos (CuartelId);

-- Los importes se guardan como TEXT, como los decimal de EF Core para SQLite.
-- HasMaxLength y HasPrecision describen el modelo; SQLite no los impone.
INSERT INTO Ciudades (Nombre, Pais)
VALUES ('La Paz', 'Bolivia'), ('Santa Cruz', 'Bolivia')
ON CONFLICT (Nombre) DO NOTHING;

-- Datos de ejemplo para las consultas de la guía.
INSERT INTO Villanos (Alias, Nivel_Amenaza, Recompensa, CiudadId)
SELECT 'Sombra Roja', 5, '15000.00', Id FROM Ciudades
WHERE Nombre = 'La Paz'
    AND NOT EXISTS (SELECT 1 FROM Villanos WHERE Alias = 'Sombra Roja' AND CiudadId = Ciudades.Id);

INSERT INTO Villanos (Alias, Nivel_Amenaza, Recompensa, CiudadId)
SELECT 'Doctor Niebla', 3, '8500.50', Id FROM Ciudades
WHERE Nombre = 'La Paz'
    AND NOT EXISTS (SELECT 1 FROM Villanos WHERE Alias = 'Doctor Niebla' AND CiudadId = Ciudades.Id);

INSERT INTO Cuarteles (Nombre, Direccion, Capacidad)
VALUES ('Base Illimani', 'Av. Arce 123, La Paz', 20),
       ('Base Oriente', 'Calle Sucre 45, Santa Cruz', 12)
ON CONFLICT (Nombre) DO NOTHING;

INSERT INTO Vehiculos (Nro_Placa, Modelo, CostoMantenimiento, CuartelId)
SELECT '2345-ABC', 'Halcón X1', '8500.00', Id FROM Cuarteles WHERE Nombre = 'Base Illimani'
ON CONFLICT (Nro_Placa) DO NOTHING;

INSERT INTO Vehiculos (Nro_Placa, Modelo, CostoMantenimiento, CuartelId)
SELECT '9876-XYZ', 'Rayo T2', '4200.75', Id FROM Cuarteles WHERE Nombre = 'Base Illimani'
ON CONFLICT (Nro_Placa) DO NOTHING;

INSERT INTO Vehiculos (Nro_Placa, Modelo, CostoMantenimiento, CuartelId)
VALUES ('1111-DEF', 'Tortuga M3', '1500.00', NULL)
ON CONFLICT (Nro_Placa) DO NOTHING;

COMMIT;
