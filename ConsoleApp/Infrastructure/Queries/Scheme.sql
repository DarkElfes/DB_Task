create table Locations
(
    Id      int identity
        constraint PK_Locations
            primary key,
    City    nvarchar(50)  not null,
    Address nvarchar(100) not null
)
go

create table ATMs
(
    Id         int identity
        constraint PK_ATMs
            primary key,
    WorkStatus nvarchar(15)   not null,
    Balance    decimal(18, 2) not null,
    LocationId int            not null
        constraint FK_ATMs_Locations_LocationId
            references Locations
            on delete cascade
)
go

create index IX_ATMs_LocationId
    on ATMs (LocationId)
go

create table Merchants
(
    Id     int identity
        constraint PK_Merchants
            primary key,
    Name   nvarchar(50) not null,
    Number nvarchar(6)  not null
)
go

create table OnlineServices
(
    Id         int identity
        constraint PK_OnlineServices
            primary key,
    Name       nvarchar(50) not null,
    MerchantId int          not null
        constraint FK_OnlineServices_Merchants_MerchantId
            references Merchants
            on delete cascade
)
go

create index IX_OnlineServices_MerchantId
    on OnlineServices (MerchantId)
go

create table Users
(
    Id          int identity
        constraint PK_Users
            primary key,
    FirstName   nvarchar(50)  not null,
    LastName    nvarchar(50)  not null,
    PhoneNumber nvarchar(15)  not null,
    Email       nvarchar(max) not null,
    Password    nvarchar(50)  not null,
    DateOfBirth date          not null
)
go

create table Cards
(
    Id             int identity
        constraint PK_Cards
            primary key,
    Balance        decimal(18, 2) not null,
    Number         nvarchar(16)   not null,
    CVV            nvarchar(3)    not null,
    Pin            nvarchar(4)    not null,
    IsBlocked      bit            not null,
    ExpirationDate date           not null,
    Currency       nvarchar(3)    not null,
    UserId         int            not null
        constraint FK_Cards_Users_UserId
            references Users
            on delete cascade
)
go

create index IX_Cards_UserId
    on Cards (UserId)
go

create table Transactions
(
    Id                   int identity
        constraint PK_Transactions
            primary key,
    Amount               decimal(18, 2) not null,
    TimeStamp            datetime2      not null,
    Status               nvarchar(9)    not null,
    CardId               int            not null
        constraint FK_Transactions_Cards_CardId
            references Cards,
    CardBalance          decimal(18, 2) not null,
    TransactionType      nvarchar(13)   not null,
    ATMId                int
        constraint FK_Transactions_ATMs_ATMId
            references ATMs
            on delete cascade,
    ATMTransactionType   nvarchar(8),
    OnlineServiceId      int
        constraint FK_Transactions_OnlineServices_OnlineServiceId
            references OnlineServices
            on delete cascade,
    ServiceReceiptNumber nvarchar(8),
    RecipientCardId      int
        constraint FK_Transactions_Cards_RecipientCardId
            references Cards,
    RecipientCardBalance decimal(18, 2)
)
go

create index IX_Transactions_ATMId
    on Transactions (ATMId)
go

create index IX_Transactions_CardId
    on Transactions (CardId)
go

create index IX_Transactions_OnlineServiceId
    on Transactions (OnlineServiceId)
go

create index IX_Transactions_RecipientCardId
    on Transactions (RecipientCardId)
go

create table __EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32)  not null
)
go

