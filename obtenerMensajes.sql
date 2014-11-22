SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[obtenerMensajes]
	@de varchar(50),
	@para varchar(50)
AS
BEGIN
	SELECT * from dbo.mensajes
	where (de = @de and para = @para) 
	or (para = @de and de = @para)
	order by fecha desc
END
GO

exec dbo.obtenerMensajes 'jam', 'jam2'