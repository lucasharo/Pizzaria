CREATE TABLE [dbo].[Endereco] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [CEP]         VARCHAR (8)   NOT NULL,
    [Logradouro]  VARCHAR (255) NOT NULL,
    [Numero]      VARCHAR (10)  NOT NULL,
    [Complemento] VARCHAR (10)  NULL,
    [Bairro]      VARCHAR (100) NULL,
    [Cidade]      VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Pizza] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [Valor]    DECIMAL (19, 2) NOT NULL,
    [IdPedido] INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Pizza_Pedido] FOREIGN KEY ([IdPedido]) REFERENCES [dbo].[Pedido] ([Id])
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

CREATE TABLE [dbo].[Sabor] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [Descricao] VARCHAR (100)   NOT NULL,
    [Preco]     DECIMAL (19, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


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

INSERT INTO Sabor VALUES('Calabresa', 35), ('Frango com catupiry', 40);