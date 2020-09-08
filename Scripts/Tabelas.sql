CREATE TABLE [dbo].[Usuario] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [IdFacebook]        BIGINT        NULL,
    [Nome]              VARCHAR (255) NOT NULL,
    [Sobrenome]         VARCHAR (255) NOT NULL,
    [Email]             VARCHAR (100) NOT NULL,
    [Password]          VARCHAR (MAX) NULL,
    [Role]              VARCHAR (50)  NOT NULL,
    [Token]             VARCHAR (MAX) NULL,
    [TokenConfirmacao]  VARCHAR (MAX) NULL,
    [TokenAlterarSenha] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Endereco] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [CEP]         VARCHAR (8)   NOT NULL,
    [Logradouro]  VARCHAR (255) NOT NULL,
    [Numero]      VARCHAR (10)  NOT NULL,
    [Complemento] VARCHAR (10)  NULL,
    [Bairro]      VARCHAR (100) NULL,
    [Cidade]      VARCHAR (100) NULL,
    [IdUsuario]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Endereco_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([Id])
);

CREATE TABLE [dbo].[Pedido] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Nome]        VARCHAR (255)   NOT NULL,
    [Valor]       DECIMAL (19, 2) NOT NULL,
    [CEP]         VARCHAR (8)     NOT NULL,
    [Logradouro]  VARCHAR (255)   NOT NULL,
    [Numero]      VARCHAR (10)    NOT NULL,
    [Complemento] VARCHAR (10)    NULL,
    [Bairro]      VARCHAR (100)   NULL,
    [Cidade]      VARCHAR (100)   NULL,
    [IdUsuario]   INT             NULL,
    [DataCriacao] DATETIME        NOT NULL DEFAULT GETDATE(), 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Pedido_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([Id])
);

CREATE TABLE [dbo].[Pizza] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [Valor]    DECIMAL (19, 2) NOT NULL,
    [IdPedido] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Pizza_Pedido] FOREIGN KEY ([IdPedido]) REFERENCES [dbo].[Pedido] ([Id])
);

CREATE TABLE [dbo].[Sabor] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [Descricao] VARCHAR (100)   NOT NULL,
    [Preco]     DECIMAL (19, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Pizza_Sabor] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [IdPizza] INT NOT NULL,
    [IdSabor] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Pizza_Sabor_Pizza] FOREIGN KEY ([IdPizza]) REFERENCES [dbo].[Pizza] ([Id]),
    CONSTRAINT [FK_Pizza_Sabor_Sabor] FOREIGN KEY ([IdSabor]) REFERENCES [dbo].[Sabor] ([Id])
);

INSERT INTO Sabor VALUES
    ('3 Queijos', 50),
    ('Frango com requeijão', 59.99),
    ('Mussarela', 42.50),
    ('Calabresa', 42.50),
    ('Pepperoni', 55),
    ('Portuguesa', 45), 
    ('Veggie', 59.99);