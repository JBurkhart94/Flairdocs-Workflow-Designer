
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/01/2017 23:37:27
-- Generated from EDMX file: C:\Users\Mason\Documents\Classwork\Capstone\TempFlairdocs\Flairdocs-Workflow-Designer\Flairdocs-Workflow-Designer\Models\WorkflowDesigner.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [master];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Workflows'
CREATE TABLE [dbo].[Workflows] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [AuditLog_Id] int  NOT NULL
);
GO

-- Creating table 'Steps'
CREATE TABLE [dbo].[Steps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WorkflowId] int  NOT NULL
);
GO

-- Creating table 'Reviewers'
CREATE TABLE [dbo].[Reviewers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StepId] int  NOT NULL,
    [Role] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AuditLogs'
CREATE TABLE [dbo].[AuditLogs] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Transitions'
CREATE TABLE [dbo].[Transitions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Conditions] nvarchar(max)  NOT NULL,
    [WorkflowId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Workflows'
ALTER TABLE [dbo].[Workflows]
ADD CONSTRAINT [PK_Workflows]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Steps'
ALTER TABLE [dbo].[Steps]
ADD CONSTRAINT [PK_Steps]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reviewers'
ALTER TABLE [dbo].[Reviewers]
ADD CONSTRAINT [PK_Reviewers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuditLogs'
ALTER TABLE [dbo].[AuditLogs]
ADD CONSTRAINT [PK_AuditLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Transitions'
ALTER TABLE [dbo].[Transitions]
ADD CONSTRAINT [PK_Transitions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [WorkflowId] in table 'Steps'
ALTER TABLE [dbo].[Steps]
ADD CONSTRAINT [FK_WorkflowStep]
    FOREIGN KEY ([WorkflowId])
    REFERENCES [dbo].[Workflows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkflowStep'
CREATE INDEX [IX_FK_WorkflowStep]
ON [dbo].[Steps]
    ([WorkflowId]);
GO

-- Creating foreign key on [StepId] in table 'Reviewers'
ALTER TABLE [dbo].[Reviewers]
ADD CONSTRAINT [FK_StepReviewer]
    FOREIGN KEY ([StepId])
    REFERENCES [dbo].[Steps]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StepReviewer'
CREATE INDEX [IX_FK_StepReviewer]
ON [dbo].[Reviewers]
    ([StepId]);
GO

-- Creating foreign key on [AuditLog_Id] in table 'Workflows'
ALTER TABLE [dbo].[Workflows]
ADD CONSTRAINT [FK_WorkflowAuditLog]
    FOREIGN KEY ([AuditLog_Id])
    REFERENCES [dbo].[AuditLogs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkflowAuditLog'
CREATE INDEX [IX_FK_WorkflowAuditLog]
ON [dbo].[Workflows]
    ([AuditLog_Id]);
GO

-- Creating foreign key on [WorkflowId] in table 'Transitions'
ALTER TABLE [dbo].[Transitions]
ADD CONSTRAINT [FK_WorkflowTransition]
    FOREIGN KEY ([WorkflowId])
    REFERENCES [dbo].[Workflows]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkflowTransition'
CREATE INDEX [IX_FK_WorkflowTransition]
ON [dbo].[Transitions]
    ([WorkflowId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------