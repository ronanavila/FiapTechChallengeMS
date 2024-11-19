USE TechChallenge
GO

INSERT INTO dbo.Region([DDD], [Location])
VALUES 
(12, 'Rio Preto'),
(19, 'Campinas'),
(11, 'São Paulo');
GO


INSERT INTO dbo.Contact([Guid], [Name], [Email], [Phone], [RegionDDD])
VALUES 
(NEWID(), 'José', 'jose@jose.com.br', '111144444', 11),
(NEWID(), 'João', 'joao@joao.com.br', '444445555', 12),
(NEWID(), 'Pedro', 'pedro@pedro.com.br', '333366666', 19);
GO