CREATE TABLE [dbo].[Username] (

    [ID]              INT NOT NULL IDENTITY,
    [Username]        NCHAR (30)    NULL,
    [Email]           NCHAR (20)    NULL,
    [Address]         NCHAR (50)    NULL,
    [Blood]           NCHAR (3)     NULL,
    [Salary]          INT           NULL,
    [Date_of_birth]   CHAR (10)     NULL,
    [Date_of_joining] CHAR (10)     NULL,
    [Password]        NCHAR (30)    NULL,
    [Section]         NCHAR (10)    NULL,
    [Sex]             NCHAR (6)     NULL, 
    [Phone] NCHAR(10) NULL, 
    CONSTRAINT [PK_Username] PRIMARY KEY ([ID]) 
);

