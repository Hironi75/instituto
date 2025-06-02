CREATE DATABASE instituto_academico;
USE instituto_academico;

-- Tabla de usuarios base (Administrador, Docente)
CREATE TABLE Usuario (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    Correo VARCHAR(100),
    Telefono VARCHAR(20),
    Tipo ENUM('Administrador', 'Docente'),
    Password VARCHAR(100)
);

-- Tabla de semestres
CREATE TABLE Semestre (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(50),
    Anio INT,
    FechaInicio DATE,
    FechaFin DATE
);

-- Tabla de materias
CREATE TABLE Materia (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Codigo VARCHAR(20),
    Nombre VARCHAR(100),
    Nivel INT,
    CargaHoraria INT
);

-- Asignación de Docente a Materia
CREATE TABLE Asignacion (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DocenteId INT,
    MateriaId INT,
    FOREIGN KEY (DocenteId) REFERENCES Usuario(Id),
    FOREIGN KEY (MateriaId) REFERENCES Materia(Id)
);

-- Tabla de estudiantes
CREATE TABLE Estudiante (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CI VARCHAR(20),
    NombreCompleto VARCHAR(100),
    Correo VARCHAR(100),
    Telefono VARCHAR(20),
    Direccion TEXT,
    Carrera VARCHAR(100)
);

-- Inscripción de estudiantes a materias
CREATE TABLE Inscripcion (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EstudianteId INT,
    MateriaId INT,
    SemestreId INT,
    FechaInscripcion DATE,
    FOREIGN KEY (EstudianteId) REFERENCES Estudiante(Id),
    FOREIGN KEY (MateriaId) REFERENCES Materia(Id),
    FOREIGN KEY (SemestreId) REFERENCES Semestre(Id)
);

-- Registro de notas
CREATE TABLE Nota (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    InscripcionId INT,
    Parcial1 DECIMAL(5,2),
    Parcial2 DECIMAL(5,2),
    ExamenFinal DECIMAL(5,2),
    NotaFinal DECIMAL(5,2),
    FOREIGN KEY (InscripcionId) REFERENCES Inscripcion(Id)
);

-- Registro de asistencia
CREATE TABLE Asistencia (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    InscripcionId INT,
    Fecha DATE,
    Asistio BOOLEAN,
    FOREIGN KEY (InscripcionId) REFERENCES Inscripcion(Id)
);

-- Certificados generados
CREATE TABLE Certificado (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EstudianteId INT,
    SemestreId INT,
    FechaGeneracion DATE,
    Documento TEXT,
    FOREIGN KEY (EstudianteId) REFERENCES Estudiante(Id),
    FOREIGN KEY (SemestreId) REFERENCES Semestre(Id)
);