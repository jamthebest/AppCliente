SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[obtenerClientes] 
	@user varchar(50)
AS
BEGIN
	SELECT * from dbo.Usuarios
	where usuario != @user and estado = 1
END
GO

exec dbo.obtenerClientes 'jam'