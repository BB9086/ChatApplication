USE [Chat]
GO

/****** Object:  StoredProcedure [dbo].[spRegisterUser]    Script Date: 10/28/2020 8:36:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spRegisterUser] 
@UserName NVARCHAR(100),
@Password NVARCHAR(100),
@Photo NVARCHAR(200)=null

AS
BEGIN

INSERT INTO tblUsers
VALUES (
@UserName,
@Password,
@Photo
)
END
GO


