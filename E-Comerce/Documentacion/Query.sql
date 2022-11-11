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
GO
--------------------------------------------------------
-- Vistas y procedimientos almacenados para las Ventas--
--------------------------------------------------------

--Vista para visualizar el detalle de venta
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
	declare @Precio money, @Descuento money, @CantidadDisponible int, @CantidadActual int, @PrecioTotal money, 
	@DescuentoTotal money, @MontoEntrega money
	begin try 
		select @Precio=(PrecioVenta*@Cantidad) from Productos where ID_Producto=@ID_Producto
		select @Descuento=Descuento from Productos where ID_Producto=@ID_Producto
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
		Descuento=@Descuento*@Cantidad,
		Usuario_Actualiza=@UsuarioActualiza,
		Fecha_Actualiza=getdate()
		where ID_DetalleVenta=@ID_DetalleVenta

		select @PrecioTotal=sum(Precio-descuento) from DetalleVenta where ID_Venta=@ID_Venta
			select @DescuentoTotal=sum(descuento) from DetalleVenta where ID_Venta=@ID_Venta
			select @MontoEntrega =MontoEntrega from Lugares_Entrega where ID_Entrega=1

		update Ventas
			set
			PrecioTotal=@PrecioTotal+@MontoEntrega,
			DescuentoTotal=@DescuentoTotal,
			Usuario_Actualiza=@UsuarioActualiza,
			Fecha_Actualiza=GETDATE()
			where ID_Venta=@ID_Venta

	end try
	begin catch
		select ERROR_MESSAGE() Error
	end catch
end
go

--Procedimiento almacenado para eliminar el detalle de venta
create proc SP_EliminarDetalleVenta(
@ID_DetalleVenta int
)
as
begin
	declare @Cantidad int, @IDPRod int, @PrecioTotal money, @DescuentoTotal money, @MontoEntrega money, @ID_Venta int
	begin try
		Select @Cantidad=Cantidad from DetalleVenta where ID_DetalleVenta=@ID_DetalleVenta
		Select @IDPRod=ID_Producto from DetalleVenta where ID_DetalleVenta=@ID_DetalleVenta
		select @ID_Venta=ID_Venta from DetalleVenta where ID_DetalleVenta = @ID_DetalleVenta

		Delete from DetalleVenta where ID_DetalleVenta=@ID_DetalleVenta

		update Productos
		set
		 cantidadDisponible=cantidadDisponible+@Cantidad
		where ID_Producto=@IDPRod

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

		select 'El detalle de venta ha sido eliminado' as Mensaje
	end try
	begin catch
		select ERROR_MESSAGE() as Mensajee
	end catch
end
go
--Procedimiento almacenado para registrar una venta
create proc sp_RegistrarVenta(
@ID_Usuario int,
@Usuario_Inserta varchar(50)
)
as
begin
	declare @ID_Venta int, @PrecioTotal money, @DescuentoTotal money, @MontoEntrega money
	begin try
		begin tran
			insert into Ventas(ID_Usuario,ID_Entrega,Usuario_Inserta) 
			values (@ID_Usuario,1,@Usuario_Inserta)

			set @ID_Venta=scope_identity()

			update DetalleVenta
			set ID_Venta=@ID_Venta,
				Usuario_Inserta=@Usuario_Inserta,
				Fecha_Inserta=GETDATE()
			where ID_Venta is null

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

		commit tran
	end try
	begin catch
		rollback tran
	end catch
end
go

--Procedimiento almacenado para registrar el detalle de una venta
create proc sp_RegistrarDetalleVenta(
@ID_Producto int,
@Cantidad int
)
as
begin
	declare @CantProd int, @Precio money, @Descuento money
	begin try
		select @CantProd=CantidadDisponible from Productos where ID_Producto=@ID_Producto
		select @Precio=PrecioVenta from Productos where ID_Producto=@ID_Producto
		select @Descuento = Descuento from Productos where ID_Producto=@ID_Producto
		if (@Cantidad>@CantProd)
		begin
			raiserror('No se cuenta con la cantidad de productos requerida',16,1)
		end
		insert into DetalleVenta(ID_Producto,Cantidad,Precio,descuento)
		values(@ID_Producto,@Cantidad,@Precio*@Cantidad,@Descuento*@Cantidad)

		update Productos
		set cantidadDisponible=cantidadDisponible-@Cantidad
		where ID_Producto=@ID_Producto

	end try
	begin catch
	end catch
end
go

--Vista para mostrar las ventas
create view vw_Venta
as
select v.ID_Venta, v.ID_Usuario, u.Usuario, r.Rol,v.PrecioTotal,v.DescuentoTotal,v.Fecha,
v.Usuario_Inserta,v.Fecha_Inserta, v.Usuario_Actualiza, v.Fecha_Actualiza
from Ventas v inner join Usuarios u on v.ID_Usuario=u.ID_Usuario 
inner join Rol r on u.ID_Rol=r.ID_Rol
go

--Pocedimiento almacenado para eliminar ventas
create proc sp_EliminarVenta(
@ID int
)
as
begin
	declare @i int, @ID_Detalle int, @cantDt int
	set @i = 1
	begin try
		begin tran
			select @cantDt=count(ID_DetalleVenta) from DetalleVenta where ID_Venta=@ID

			while (@i<=@cantDt)
			begin
				select Top 1 @ID_Detalle=ID_DetalleVenta from DetalleVenta where ID_Venta=@ID
				exec SP_EliminarDetalleVenta @ID_Detalle
				set @i=@i+1
			end

			delete from Ventas where ID_Venta=@ID
			
		commit tran
	end try
	begin catch
		rollback tran
	end catch
end
go


--vista Repartidor
Create View Vw_Repartidor
AS
	Select c.ID_Repartidor as Id,
	c.Nombre + ' ' + c.Apellido as Repartidores,
	c.CorreoElectronico as Correo,
	c.Telefono, 
	ISNULL(c.Usuario_Actualiza,c.Usuario_Inserta) UltimoUsuarioActualiza,
    ISNULL(c.Fecha_Actualiza,c.Fecha_Inserta) UltimaFechaActualiza
	from Repartidores c
GO

--/********                           ***************/
ALTER proc [dbo].[SP_ActualizarProveedor]
@Id_Proveedor int,
@Nombre_Empresa varchar (50),
@telefono varchar (20),
@Usuario varchar (50)
as
begin
   
    begin try 
	   if not exists( select*from Proveedores where NombreEmpresa=@Nombre_Empresa or Telefono=@telefono)  
	   begin
	     RAISERROR('Nombre del proveedor o el telefono ya existe',16,1)
	   end

	   update Proveedores set NombreEmpresa = @Nombre_Empresa , Telefono = @telefono, Usuario_Inserta = @Usuario, Fecha_Inserta = GETDATE()
	   where ID_Proveedor = @Id_Proveedor

	   select 'El proveedor ha sido actualizado exitosamente!' Mensaje
	end try
	begin catch

	     select ERROR_MESSAGE() Mensaje 

	end catch
end
GO

/***** View Proveedores *******/

create  VIEW [dbo].[Vw_Proveedor]
as
  
    SELECT
	   ID_Proveedor,
	   NombreEmpresa,
	   Telefono,
		ISNULL(Usuario_Actualiza,Usuario_Inserta) UltimoUsuarioActualiza,
		ISNULL(Fecha_Actualiza,Fecha_Inserta) UltimaFechactualiza
	        FROM Proveedores
GO
