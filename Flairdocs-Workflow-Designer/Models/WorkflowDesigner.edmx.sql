
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/05/2017 14:50:45
-- Generated from EDMX file: C:\Users\Mason\Documents\Classwork\Capstone\TempFlairdocs\Flairdocs-Workflow-Designer\Flairdocs-Workflow-Designer\Models\WorkflowDesigner.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [osuflairdocs];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_WorkflowStep]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Steps] DROP CONSTRAINT [FK_WorkflowStep];
GO
IF OBJECT_ID(N'[dbo].[FK_WorkflowAuditLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Workflows] DROP CONSTRAINT [FK_WorkflowAuditLog];
GO
IF OBJECT_ID(N'[dbo].[FK_WorkflowTransition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transitions] DROP CONSTRAINT [FK_WorkflowTransition];
GO
IF OBJECT_ID(N'[dbo].[FK_ReviewerAttribute]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attributes] DROP CONSTRAINT [FK_ReviewerAttribute];
GO
IF OBJECT_ID(N'[dbo].[FK_StepReviewer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reviewers] DROP CONSTRAINT [FK_StepReviewer];
GO
IF OBJECT_ID(N'[dbo].[FK_AuditLogAudit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Audits] DROP CONSTRAINT [FK_AuditLogAudit];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Workflows]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Workflows];
GO
IF OBJECT_ID(N'[dbo].[Steps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Steps];
GO
IF OBJECT_ID(N'[dbo].[Reviewers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reviewers];
GO
IF OBJECT_ID(N'[dbo].[AuditLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuditLogs];
GO
IF OBJECT_ID(N'[dbo].[Transitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transitions];
GO
IF OBJECT_ID(N'[dbo].[Attributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attributes];
GO
IF OBJECT_ID(N'[dbo].[Audits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Audits];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Workflows'
CREATE TABLE [dbo].[Workflows] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Creation_Date] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [AuditLog_Id] int  NOT NULL
);
GO

-- Creating table 'Steps'
CREATE TABLE [dbo].[Steps] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WorkflowId] int  NOT NULL,
    [Creation_Date] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Reviewers'
CREATE TABLE [dbo].[Reviewers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Role] nvarchar(max)  NOT NULL,
    [Creation_Date] nvarchar(max)  NOT NULL,
    [StepId] int  NOT NULL
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

-- Creating table 'Attributes'
CREATE TABLE [dbo].[Attributes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ReviewerId] int  NOT NULL,
    [Creation_Date] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Audits'
CREATE TABLE [dbo].[Audits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AuditLogId] int  NOT NULL,
    [Entry] nvarchar(max)  NOT NULL
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

-- Creating primary key on [Id] in table 'Attributes'
ALTER TABLE [dbo].[Attributes]
ADD CONSTRAINT [PK_Attributes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Audits'
ALTER TABLE [dbo].[Audits]
ADD CONSTRAINT [PK_Audits]
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

-- Creating foreign key on [ReviewerId] in table 'Attributes'
ALTER TABLE [dbo].[Attributes]
ADD CONSTRAINT [FK_ReviewerAttribute]
    FOREIGN KEY ([ReviewerId])
    REFERENCES [dbo].[Reviewers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReviewerAttribute'
CREATE INDEX [IX_FK_ReviewerAttribute]
ON [dbo].[Attributes]
    ([ReviewerId]);
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

-- Creating foreign key on [AuditLogId] in table 'Audits'
ALTER TABLE [dbo].[Audits]
ADD CONSTRAINT [FK_AuditLogAudit]
    FOREIGN KEY ([AuditLogId])
    REFERENCES [dbo].[AuditLogs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuditLogAudit'
CREATE INDEX [IX_FK_AuditLogAudit]
ON [dbo].[Audits]
    ([AuditLogId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------