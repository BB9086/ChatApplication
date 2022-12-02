USE [Chat]
GO

/****** Object:  StoredProcedure [dbo].[spInsertMessage]    Script Date: 10/28/2020 8:37:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[spInsertMessage]
@Sender nvarchar(100),
@Receiver nvarchar(100),
@Message nvarchar(200),
@Datum datetime=null
as
begin 
Insert into tblMessenger values (@Sender,@Receiver, @Message, GETDATE())
end
GO


