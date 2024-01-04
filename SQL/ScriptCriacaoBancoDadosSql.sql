Create Database TesteTecnicoDevNet;
USE TesteTecnicoDevNet;

BEGIN TRANSACTION;
GO

CREATE TABLE [Cliente] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NOT NULL,
    [CPF] bigint NOT NULL,
    [UF] nvarchar(max) NOT NULL,
    [Celular] bigint NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Financiamentos] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NOT NULL,
    [CPF] bigint NOT NULL,
    [TipoCredito] int NOT NULL,
    [ValorTotal] decimal(18,2) NOT NULL,
    [DataUltimoVencimento] datetime2 NOT NULL,
    CONSTRAINT [PK_Financiamentos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Financiamentos_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Parcelas] (
    [Id] int NOT NULL IDENTITY,
    [FinanciamentoId] int NOT NULL,
    [NumeroParcela] int NOT NULL,
    [ValorParcela] decimal(18,2) NOT NULL,
    [DataVencimento] datetime2 NOT NULL,
    [DataPagamento] datetime2 NULL,
    CONSTRAINT [PK_Parcelas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Parcelas_Financiamentos_FinanciamentoId] FOREIGN KEY ([FinanciamentoId]) REFERENCES [Financiamentos] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Financiamentos_ClienteId] ON [Financiamentos] ([ClienteId]);
GO

CREATE INDEX [IX_Parcelas_FinanciamentoId] ON [Parcelas] ([FinanciamentoId]);
GO


COMMIT;
GO

--Script para Criação da Massa de dados

declare @NomeCliente Varchar(100)
declare @UF Varchar(2) 
declare @IdCliente INT
declare @cpf Varchar(11)

declare @IdFinanciamento INT
declare @TipoCredito INT
declare @valorFinanciamento decimal 
declare @qtdParcelas int 
declare @Parcela int 
declare @valorParcela decimal 
declare @dataVencimento Varchar(30)

------------------------------------------------------------------------------------
--Informe aqui os dados para realização do emprestimo
set @NomeCliente			= 'Cliente1 ...'		-- Informe o Nome do cliente
set @cpf					= '28713899007'			-- Informe o CPF 
set @UF						= 'MG'					-- Informe o Estado do cliente
set @valorFinanciamento		= 21740.00				-- Informe o Valor do Financiamento
set @qtdParcelas			= 3						-- Informe a Quantidade de parcelas
set @TipoCredito			= 1						-- Informe o Tipo de Credito  Legenda 1= Crédito Direto / 2 = Crédito Consignado / 3 = Crédito Pessoa Jurídica / 4 = Crédito Pessoa Física /  5= Crédito Imobiliário 
set @dataVencimento         = DATEADD(DAY, +30, GETDATE())
set @Parcela = 1								
set @valorParcela = (@valorFinanciamento) / (@qtdParcelas)

BEGIN
	--cadastro do cliente
	insert into Cliente values (@NomeCliente, @cpf, @UF,'11946542141')
	set @IdCliente = @@IDENTITY

	--Cadastro Financiamento
	insert into Financiamentos values (@IdCliente, @cpf , @TipoCredito ,@valorFinanciamento, @dataVencimento)
	set @IdFinanciamento = @@IDENTITY

	-- cadastro das parcelas
	while(@Parcela <= @qtdParcelas)
	BEGIN
		insert into Parcelas values (@IdFinanciamento,@Parcela,@valorParcela, @dataVencimento ,null)

		--atualizando no financiamento a data do ultimo vencimento
		IF (@Parcela = @qtdParcelas)
		BEGIN
			update Financiamentos set DataUltimoVencimento = @dataVencimento where Id = @IdFinanciamento	
		END
		
		SET @dataVencimento = DATEADD(DAY, +30, @dataVencimento)
		SET @Parcela = @Parcela +1

	END
END

 
------------------------------------------------------------------------------------------------
-- Retorna Clientes que ja pagaram 60% das parcelas no estado de SP

SELECT 
	c.Nome, 
	c.CPF
FROM 
	CLIENTE c
	INNER JOIN Financiamentos 	f ON f.ClienteId = c.Id
	LEFT JOIN Parcelas 			p ON f.Id = p.FinanciamentoId
WHERE c.UF = 'SP'
	GROUP BY c.Nome, c.CPF
	HAVING COUNT(p.FinanciamentoId) > 0 
	AND SUM(CASE WHEN p.DataPagamento IS NOT NULL THEN 1 ELSE 0 END) / COUNT(p.FinanciamentoId) > 0.6;

--Retorno dos dados

--|	Nome	        CPF
--|	Cliente1 ...	28713899007
--|	Cliente7 ...	44321941039
---------------------------------------------
--Listar os primeiros quatro clientes que possuem alguma parcela com mais de cinco dia sem atraso.

SELECT 
	top 4 
	c.Nome, 
	c.CPF
FROM 
	CLIENTE c
	INNER JOIN Financiamentos 		f ON f.ClienteId = c.Id
	INNER JOIN Parcelas 			ON  f.Id = p.FinanciamentoId
WHERE p.DataVencimento > GETDATE() 
	AND p.DataPagamento IS NULL
	GROUP BY c.Nome, c.CPF
	--Retorno dos dados

--|	Nome	        CPF
--|	Cliente2 ...	44321941032
--|	Cliente3 ...	44321941034
--|	Cliente4 ...	44321941035
--|	Cliente5 ...	44321941036

