CREATE TABLE [dbo].[Login] (
    [ID]        INT        NOT NULL,
    [Loginname] CHAR (30)  NULL,
    [Email]     NCHAR (20) NOT NULL,
    [Password]  NCHAR (30) NULL,
    CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED ([Email] ASC),
    FOREIGN KEY ([ID]) REFERENCES [dbo].[Username] ([ID]) ON UPDATE CASCADE ON DELETE CASCADE 
);

