Create View V_Compras
AS
	Select c.Id_Compra as Id,
	u.Nombre + ' ' + u.Apellido as Usuario,
	c.PrecioTotal as Total,
	c.Fecha, 
	ISNULL(c.Usuario_Actualiza,c.Usuario_Inserta) UltimoUsuarioActualiza,
    ISNULL(c.Fecha_Actualiza,c.Fecha_Inserta) UltimaFechaActualiza
	from compras c
	inner join Usuarios u on c.ID_Usuario = u.ID_Usuario
	
	Select * from V_Compras

	Select * from Usuarios