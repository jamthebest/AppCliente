SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[estadoConectado]
	@user varchar(50),
	@estado int
AS
BEGIN
	update dbo.Usuarios 
	set [estado] = @estado
	where usuario = @user
END
GO