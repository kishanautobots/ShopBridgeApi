USE [ShopBridge]
GO

CREATE TABLE [dbo].[shop_bridge_products](
	[id] [int] Primary Key IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[description] [varchar](500) NULL,
	[price] [float] NULL,
	[added_date_time] [datetime] NOT NULL,
	[modified_date_time] [datetime] NULL)

