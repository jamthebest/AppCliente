SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE [dbo].[nuevoCliente]
	@user varchar(50),
	@pass varchar(50)
AS
BEGIN
	if(not exists(select * from Usuarios where usuario = @user))
		insert into dbo.Usuarios(usuario, contraseña, estado) values (@user, @pass, 1)
END
GO

exec nuevoCliente javier, jam
select * from dbo.Usuarios