USE [Chat]
GO

/****** Object:  StoredProcedure [dbo].[spAuthenticateUser]    Script Date: 10/28/2020 8:38:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[spAuthenticateUser]
@UserName nvarchar(100),
@Password nvarchar(100)
as
Begin
 Declare @Count int
 
 Select @Count = COUNT(UserName) from tblUsers
 where [UserName] = @UserName and [Password] = @Password
 
 if(@Count = 1)
 Begin
  Select 1 as ReturnCode
 End
 Else
 Begin
  Select -1 as ReturnCode
 End
End
GO


