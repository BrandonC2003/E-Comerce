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

--VEntas

create view vw_DetalleVenta
as
Select ID_DetalleVenta,ID_Venta,v.ID_Producto,p.NombreProducto, cantidad,Precio, v.descuento, 
v.Usuario_Inserta,v.Fecha_Inserta,v.Usuario_Actualiza,v.Fecha_Actualiza 
from DetalleVenta v inner join Productos p on p.ID_Producto=v.ID_Producto
go
	
--Procedimiento almacenado para editar el detalle de venta
create proc sp_EditarDetalleVenta(
@ID_DetalleVenta int,
@ID_Venta int,
@ID_Producto int, 
@Cantidad int,
@UsuarioActualiza varchar(100)
)
as
begin
	declare @Precio money, @Descuento money, @CantidadDisponible int, @CantidadActual int
	begin try 
		select @Precio=(PrecioVenta*@Cantidad) from Productos
		select @Descuento=Descuento from Productos
		select @CantidadActual=Cantidad from DetalleVenta where ID_DetalleVenta=@ID_DetalleVenta
		select @CantidadDisponible=cantidadDisponible from Productos where ID_Producto=@ID_Producto
		if (@Cantidad > @CantidadDisponible )
		begin
			raiserror('La cantidad de productos disponibles no es suficiente',16,1)
		end
		update productos
		set
		cantidadDisponible=@CantidadDisponible+(@CantidadActual-@Cantidad)
		where ID_Producto = @ID_Producto

		update DetalleVenta
		set
		ID_Venta=@ID_Venta,
		ID_Producto=@ID_Producto,
		Cantidad=@Cantidad,
		Precio=@Precio,
		Descuento=@Descuento,
		Usuario_Actualiza=@UsuarioActualiza,
		Fecha_Actualiza=getdate()
		where ID_DetalleVenta=@ID_DetalleVenta
	end try
	begin catch
		select ERROR_MESSAGE() Error
	end catch
end
go

--Procedimiento almacenado para eliminar el detalle de venta
alter proc SP_EliminarDetalleVenta(
@ID_DetalleVenta int
)
as
begin
	declare @Cantidad int, @IDPRod int
	begin try
		Delete from DetalleVenta where ID_DetalleVenta=@ID_DetalleVenta

		Select @Cantidad=Cantidad from DetalleVenta where ID_DetalleVenta=@ID_DetalleVenta
		Select @IDPRod=ID_Producto from DetalleVenta where ID_DetalleVenta=@ID_DetalleVenta

		update Productos
		set
		 cantidadDisponible=cantidadDisponible+@Cantidad
		where ID_Producto=@IDPRod

		select 'El detalle de venta ha sido eliminado' as Mensaje
	end try
	begin catch
		select ERROR_MESSAGE() as Mensajee
	end catch
end
go


--Procedimiento almaceado para registrar Venta
create type tp_Ventas as table(
ID_product int,
cantidad int
)
go

create proc sp_RegistrarVenta(
@ID_Usuario int,
@Usuario_Inserta varchar(50),
@DetalleVenta tp_Ventas readonly
)
as
begin
	declare @ID_Venta int, @PrecioTotal money, @DescuentoTotal money, @MontoEntrega money
	begin try
		begin tran
			insert into Ventas(ID_Usuario,ID_Entrega,Usuario_Inserta) 
			values (@ID_Usuario,1,@Usuario_Inserta)

			set @ID_Venta=scope_identity()

			insert into DetalleVenta(ID_Venta,ID_Producto,Cantidad,Precio,descuento,Usuario_Inserta,Fecha_Inserta)
			select @ID_Venta, ID_product, cantidad,
			(select PrecioVenta from Productos where ID_Producto=ID_Product)*cantidad,
			(select Descuento from Productos where ID_Producto=ID_Product)*cantidad,
			@Usuario_Inserta, getdate() from @DetalleVenta

			select @PrecioTotal=sum(Precio-descuento) from DetalleVenta where ID_Venta=@ID_Venta
			select @DescuentoTotal=sum(descuento) from DetalleVenta where ID_Venta=@ID_Venta
			select @MontoEntrega =MontoEntrega from Lugares_Entrega where ID_Entrega=1

			update Ventas
			set
			PrecioTotal=@PrecioTotal+@MontoEntrega,
			DescuentoTotal=@DescuentoTotal,
			Fecha=getdate(),
			Fecha_Inserta=getdate()
			where ID_Venta=@ID_Venta

			update p --Hay que validar que el producto no este repetido
			set p.cantidadDisponible=p.cantidadDisponible-d.cantidad
			from Productos p inner join @DetalleVenta d on p.ID_Producto=d.ID_product

		commit tran
	end try
	begin catch
		rollback tran
	end catch
end
go

