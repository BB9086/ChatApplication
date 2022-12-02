USE [Chat]
GO

/****** Object:  StoredProcedure [dbo].[spGetMessage]    Script Date: 10/28/2020 8:37:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[spGetMessage] 
@Sender nvarchar(100),
@Receiver nvarchar(100),
@PageNumber int,
@PageSize int
as
begin
  Declare @StartRow int
    Declare @EndRow int

    Set @StartRow = ((@PageNumber - 1) * @PageSize) + 1
    Set @EndRow = @PageNumber * @PageSize;
 WITH RESULT AS
    (
     SELECT Id, Sender, Receiver, Message, Datum,
             ROW_NUMBER() OVER (ORDER BY Datum desc) AS ROWNUMBER
     From    tblMessenger
	 where (Sender=@Sender and Receiver=@Receiver) or (Sender=@Receiver and Receiver=@Sender)
    )
    SELECT *
    FROM RESULT
    WHERE ROWNUMBER BETWEEN @StartRow AND @EndRow


end
GO


