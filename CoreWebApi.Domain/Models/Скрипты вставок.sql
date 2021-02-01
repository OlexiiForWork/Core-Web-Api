USE [TestDBCirilic]
GO

select * from [dbo].[Clients]

--insert into [dbo].[Products] ([Name], [Category], [Artikul], [Price] ) VALUES ( 'Оболонь светлое 0.5','Алкоголь','AL00000001',20);
--insert into [dbo].[Products] ([Name], [Category], [Artikul], [Price] ) VALUES ( 'Черниговсое светлое 0.7','Алкоголь','AL00000002',25);
--insert into [dbo].[Products] ([Name], [Category], [Artikul], [Price] ) VALUES ( 'Оболонь тёмое 0.5','Алкоголь','AL00000003',12);
--insert into [dbo].[Products] ([Name], [Category], [Artikul], [Price] ) VALUES ( 'Черниговсое тёмое 0.5','Алкоголь','AL00000004',15.20);
--insert into [dbo].[Products] ([Name], [Category], [Artikul], [Price] ) VALUES ( 'Платье Дольче габана','Одежда','CLOS000005',85);
--insert into [dbo].[Products] ([Name], [Category], [Artikul], [Price] ) VALUES ( 'Куртка Дольче габана','Одежда','CLOS000006',140);
--insert into [dbo].[Products] ([Name], [Category], [Artikul], [Price] ) VALUES ( 'Сапоги','Обувь','OBWI000007',70);
--insert into [dbo].[Products] ([Name], [Category], [Artikul], [Price] ) VALUES ( 'Туфли','Обувь','OBWI000008',1200.25);
--insert into [dbo].[Products] ([Name], [Category], [Artikul], [Price] ) VALUES ( 'Тетрадка','Канцелярия','CANT000009',86);
--insert into [dbo].[Products] ([Name], [Category], [Artikul], [Price] ) VALUES ( 'Ручка','Канцелярия','CANT000010',12);
select * from [dbo].[Products]



DECLARE @ClientsID int =0,
@ID int =0,
@RandCount int =0;

DECLARE Clients_cursor CURSOR FOR   
SELECT ID
FROM [dbo].[Clients]
  
OPEN Clients_cursor  
  
FETCH NEXT FROM Clients_cursor   
INTO @ClientsID  
  
WHILE @@FETCH_STATUS = 0  
BEGIN  

select @RandCount=(1.0 + floor(4 * RAND(convert(varbinary, newid())))); 

DECLARE Purchases_count_cursor CURSOR FOR   
SELECT ID FROM 
(select 1 ID
union 
select 2 
union 
select 3 
union 
select 4 
union 
select 5 
union 
select 6 
union 
select 7 ) T
 where T.ID<=@RandCount
  
OPEN Purchases_count_cursor  
  
FETCH NEXT FROM Purchases_count_cursor   
INTO @ID  
  
WHILE @@FETCH_STATUS = 0  
BEGIN  

insert into [dbo].[Purchases] ([ClientsID], [Number], [Date])
select @ClientsID, 
    @ID [Number],
 DATEADD(MINUTE,-1*(1.0 + floor(4 * RAND(convert(varbinary, newid())))),DATEADD(HOUR,-1*(1.0 + floor(4 * RAND(convert(varbinary, newid())))),DATEADD(DAY,-1*(1.0 + floor(4 * RAND(convert(varbinary, newid())))) ,GETDATE())))

FETCH NEXT FROM Purchases_count_cursor   
INTO @ID  
END   
CLOSE Purchases_count_cursor;  
DEALLOCATE Purchases_count_cursor; 




FETCH NEXT FROM Clients_cursor   
INTO @ClientsID  
END   
CLOSE Clients_cursor;  
DEALLOCATE Clients_cursor;  
select * from[dbo].[Purchases]
--------------------------------------------------------------------
DECLARE @PurchasesID int =0;

DECLARE Purchases_cursor CURSOR FOR   
SELECT ID
FROM [dbo].[Purchases]
  
OPEN Purchases_cursor  
  
FETCH NEXT FROM Purchases_cursor   
INTO @PurchasesID  
  
WHILE @@FETCH_STATUS = 0  
BEGIN  

insert into [dbo].[Positions] (    [PurchasesID],    [ProductsID], 	[Сounts],	[Сost]) 
select [PurchasesID],    [ProductsID], 	[Сounts],[Сounts]*[Price]	[Сost] FROM (
select T1.Id [PurchasesID], T2.Id [ProductsID],t2.[Сounts],T2.Price FROM [dbo].[Purchases] T1
,(SELECT TOP 3 (1.0 + floor(14 * RAND(convert(varbinary, newid())))) [Сounts],* FROM [dbo].[Products] ORDER BY NEWID()) T2 
where T1.Id=@PurchasesID)as resT


FETCH NEXT FROM Purchases_cursor   
INTO @PurchasesID  
END   
CLOSE Purchases_cursor;  
DEALLOCATE Purchases_cursor;  

select *from [dbo].[Positions];


--delete from [dbo].[Positions];
--delete from[dbo].[Purchases]