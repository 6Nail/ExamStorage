create table WayBills
(
[Id] int primary key identity(1,1),
[Type] nvarchar(MAX)null,
[Quantity] nvarchar(MAX) null,
[ProductId]int not null,
[StoreKeeper] int not null,
[Price]int not null,
[DeliveryDate] datetime not null,
[DepartDate] datetime not null,
[Provider] nvarchar(MAX) not null
)