
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/27/2021 04:07:14
-- Generated from EDMX file: C:\Users\firio\Desktop\Project\ProjectGameInterface\ProjectGameInterface\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GameIntDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PlayerStats]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stats] DROP CONSTRAINT [FK_PlayerStats];
GO
IF OBJECT_ID(N'[dbo].[FK_GameStats]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stats] DROP CONSTRAINT [FK_GameStats];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Players]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Players];
GO
IF OBJECT_ID(N'[dbo].[Stats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stats];
GO
IF OBJECT_ID(N'[dbo].[Games]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Games];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PlayerName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Stats'
CREATE TABLE [dbo].[Stats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Wins] int  NULL,
    [Draws] int  NULL,
    [Losses] int  NULL,
    [PlayerId] int  NOT NULL,
    [GameId] int  NOT NULL,
    [LastScore] int  NULL,
    [LastGame] datetime  NULL
);
GO

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GameType] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stats'
ALTER TABLE [dbo].[Stats]
ADD CONSTRAINT [PK_Stats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PlayerId] in table 'Stats'
ALTER TABLE [dbo].[Stats]
ADD CONSTRAINT [FK_PlayerStats]
    FOREIGN KEY ([PlayerId])
    REFERENCES [dbo].[Players]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerStats'
CREATE INDEX [IX_FK_PlayerStats]
ON [dbo].[Stats]
    ([PlayerId]);
GO

-- Creating foreign key on [GameId] in table 'Stats'
ALTER TABLE [dbo].[Stats]
ADD CONSTRAINT [FK_GameStats]
    FOREIGN KEY ([GameId])
    REFERENCES [dbo].[Games]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameStats'
CREATE INDEX [IX_FK_GameStats]
ON [dbo].[Stats]
    ([GameId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------