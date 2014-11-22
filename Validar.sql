SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Validar] 
	@user varchar(50),
	@pass varchar(50)
AS
BEGIN
	SELECT * from dbo.Usuarios
	where usuario = @user and contraseña = @pass
END
GO
