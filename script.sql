IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Candidatos] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Apellido] nvarchar(max) NULL,
    [FechaNacimiento] datetime2 NOT NULL,
    [Email] nvarchar(max) NULL,
    [Telefono] bigint NOT NULL,
    CONSTRAINT [PK_Candidatos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Empleos] (
    [Id] int NOT NULL IDENTITY,
    [NombreEmpresa] nvarchar(max) NULL,
    [Desde] datetime2 NOT NULL,
    [Hasta] datetime2 NOT NULL,
    [CandidatoId] int NOT NULL,
    CONSTRAINT [PK_Empleos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Empleos_Candidatos_CandidatoId] FOREIGN KEY ([CandidatoId]) REFERENCES [Candidatos] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Empleos_CandidatoId] ON [Empleos] ([CandidatoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221221223523_InitialCreate', N'7.0.1');
GO

COMMIT;
GO

