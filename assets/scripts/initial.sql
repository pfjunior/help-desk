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

CREATE TABLE [Departments] (
    [Id] uniqueidentifier NOT NULL,
    [Code] varchar(6) NOT NULL,
    [Name] varchar(100) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Teste] varchar(150) NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Tickets] (
    [Id] uniqueidentifier NOT NULL,
    [Title] varchar(100) NOT NULL,
    [Description] varchar(500) NOT NULL,
    [Solution] varchar(500) NOT NULL,
    [Type] int NOT NULL,
    [Status] int NOT NULL,
    [Priority] int NOT NULL,
    [CreatedIn] datetime2 NOT NULL,
    [CompletedIn] datetime2 NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Registration] varchar(8) NOT NULL,
    [FirstName] varchar(30) NOT NULL,
    [LastName] varchar(30) NOT NULL,
    [Extension] varchar(150) NULL,
    [DepartmentId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Users_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id])
);
GO

CREATE TABLE [Comments] (
    [TicketId] uniqueidentifier NOT NULL,
    [Id] int NOT NULL IDENTITY,
    [Description] varchar(500) NOT NULL,
    [CreatedIn] datetime2 NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([TicketId], [Id]),
    CONSTRAINT [FK_Comments_Tickets_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Tickets] ([Id])
);
GO

CREATE INDEX [IX_Users_DepartmentId] ON [Users] ([DepartmentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240316134747_Initial', N'8.0.3');
GO

COMMIT;
GO

