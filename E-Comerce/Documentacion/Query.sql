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

----------------------------------------------------------------------
--****************procedure para repartidor***************************
----------------------------------------------------------------------


--procedimiento para insertar repartidor
Create procedure SP_InsertarRepartido
@Nombre varchar(50),
@Apellido VARCHAR(50), 
@correo VARCHAR(50),
@telefono varchar(15),
@Usuario_Inserta varchar(100)
as 
begin
	begin try
		if exists(Select *  from Repartidores where CorreoElectronico = @correo)
			begin
				raiserror('Repartidor ya existe',16,1)
			end
			insert into Repartidores(Nombre,Apellido,CorreoElectronico,Telefono,Usuario_Inserta,Fecha_Inserta) 
			values (@Nombre,@Apellido,@Correo,@telefono,@Usuario_Inserta,GETDATE())
			
			Select 'Repartidor insertado Exitosamente!' Mensaje
	end try
	begin catch
		Select ERROR_MESSAGE() Mensaje
	end catch
end
go


--procedimiento almacenado para editar categoria (Modificado)
create procedure SP_EditarRepartidor
@ID_Repartidor int,
@Nombre VARCHAR(50),
@Apellido VARCHAR(50), 
@correo VARCHAR(50),
@telefono varchar(15),
@Usuario_Actualiza varchar(100)
as 
begin
	begin try
		begin tran			
			UPDATE Repartidores set  Nombre = @Nombre, Apellido = @Apellido, 
			CorreoElectronico = @correo, Telefono = @telefono, 
			Usuario_Actualiza = @Usuario_Actualiza, Fecha_Actualiza = getdate()
			where ID_Repartidor = @ID_Repartidor
			
		commit
	end try
	begin catch
		if @@TRANCOUNT>0
			rollback
	end catch
end
go


--Procedimiento almacenado para eliminar repartidor
create proc SP_EliminarRepartidor
@ID_Repartidor int
as
begin
      begin try
         delete from Repartidores where ID_Repartidor=@ID_Repartidor

			select 'El repartidor a sido eliminado del sistema exitosamente' Mensaje
        end try
        begin catch
            select ERROR_MESSAGE() Mensaje

         end catch
end
go

----------Procedimiento almacenado que retorna total de ventas de los ultimos 3 meses--
create procedure SP_RetornarVentas
as 
begin 

declare @fecha_maxima datetime;
declare @fecha_minima datetime;

set @fecha_maxima = ( select MAX(fecha) from Ventas)

set @fecha_minima = DATEADD(MONTH,-2,@fecha_maxima);
SET @fecha_minima = DATEADD(DAY,- (day(@fecha_minima) -1 ), @fecha_minima);

select year(fecha) as 'Año',DATENAME(MONTH,fecha) as 'Mes',sum(PrecioTotal) as 'Total' from Ventas
where Fecha between @fecha_minima and @fecha_maxima 
group by year(fecha), MONTH(fecha), DATENAME(MONTH, Fecha)
order by year(Fecha), MONTH(Fecha) asc  

end
go


----------------------------------------------------------------------
--****************			Compra			***************************
----------------------------------------------------------------------
Create Procedure SP_GuardarCompra
@Id_Usuario int,
@PrecioTotal  money, 
@Usuario varchar(50)
AS
	begin
		begin try
			declare @Idtransaccion int;

			Insert Into Compras (ID_Usuario, PrecioTotal, Fecha, Fecha_Inserta, Usuario_Inserta)
			Values (@Id_Usuario, @PrecioTotal, GETDATE(), GETDATE(),@Usuario)

			set @Idtransaccion = SCOPE_IDENTITY();

			Select @Idtransaccion IdTransaccion, 'guardado correctamente' mensaje
		end try
		begin catch
			Set @idTransaccion= 0;
			Select @idTransaccion IdTransaccion, 'succedio un error' mensaje
		end catch
	end
go

Create View V_DetalleCompra
AS
	Select d.ID_DetalleCompra,
	d.ID_Producto,
	p.NombreProducto,
	d.ID_Compra,
	d.Cantidad,
	d.PrecioUnitario,
	d.Total,	
	d.Usuario_Actualiza,
	d.Fecha_Actualiza,
	d.Usuario_Inserta,
	d.Fecha_Inserta
	from Detalle_Compra d
	inner join Productos p on d.ID_Producto = p.ID_Producto
GO

Select * from V_DetalleCompra

Alter table Detalle_Compra 
add PrecioUnitario Money null,
Usuario_Inserta varchar(50) null,
Fecha_Inserta datetime null
GO


--------------------------------------------------------------------------------------
-----------procedimientos para el carrito---------------------------------------------
--------------------------------------------------------------------------------------
--Procedimiento almacenado para registrar el detalle de una venta
create proc sp_RegistrarCarrito(
@ID_Producto int,
@Cantidad int,
@UsuarioInserta varchar(50)
)
as
begin
	declare @CantProd int, @Precio money, @Descuento money
	begin try
		begin tran
			select @CantProd=CantidadDisponible from Productos where ID_Producto=@ID_Producto
			select @Precio=PrecioVenta from Productos where ID_Producto=@ID_Producto
			select @Descuento = Descuento from Productos where ID_Producto=@ID_Producto
			if (@Cantidad>@CantProd)
			begin
				raiserror('No se cuenta con la cantidad de productos requerida',16,1)
			end
			insert into DetalleVenta(ID_Producto,Cantidad,Precio,descuento,Usuario_Inserta)
			values(@ID_Producto,@Cantidad,@Precio*@Cantidad,@Descuento*@Cantidad,@UsuarioInserta)

			update Productos
			set cantidadDisponible=cantidadDisponible-@Cantidad
			where ID_Producto=@ID_Producto

		commit tran

	end try
	begin catch	
		rollback 
	end catch
end
go


--Procedimiento almacenado sumar cantidad de producto al carrito
create proc sp_SumarCarrito(
@ID_DetalleVenta int
)
as
begin	
	declare @ID_producto int
	begin try
		begin tran

			select @ID_producto= ID_Producto from DetalleVenta
			where ID_DetalleVenta=@ID_DetalleVenta

			update DetalleVenta 
			set Cantidad=Cantidad+1
			where ID_DetalleVenta=@ID_DetalleVenta

			update Productos
			set
			 cantidadDisponible=cantidadDisponible-1
			where ID_Producto=@ID_producto

		commit tran

	end try
	begin catch	
		rollback 
	end catch
end
go


--Procedimiento almacenado restar cantidad de producto al carrito
create proc sp_RestarCarrito(
@ID_DetalleVenta int
)
as
begin	
	declare @ID_producto int
	begin try
		begin tran

			select @ID_producto= ID_Producto from DetalleVenta
			where ID_DetalleVenta=@ID_DetalleVenta

			update DetalleVenta 
			set Cantidad=Cantidad-1
			where ID_DetalleVenta=@ID_DetalleVenta

			update Productos
			set
			 cantidadDisponible=cantidadDisponible+1
			where ID_Producto=@ID_producto

		commit tran

	end try
	begin catch	
		rollback 
	end catch
end
go


--Procedimiento almacenado para eliminar el producto agregado al carrito
create proc SP_EliminarCarrito(
@ID_DetalleVenta int
)
as
begin
	declare @Cantidad int, @IDPRod int
	begin try
		Select @Cantidad=Cantidad from DetalleVenta where ID_DetalleVenta=@ID_DetalleVenta
		Select @IDPRod=ID_Producto from DetalleVenta where ID_DetalleVenta=@ID_DetalleVenta		

		Delete from DetalleVenta where ID_DetalleVenta=@ID_DetalleVenta

		update Productos
		set
		 cantidadDisponible=cantidadDisponible+@Cantidad
		where ID_Producto=@IDPRod

		select 'El producto a sido eliminado del carrito' as Mensaje
	end try
	begin catch
		select ERROR_MESSAGE() as Mensajee
	end catch
end
go


--Vista para el carrito
create view vw_Carrito
as
Select ID_DetalleVenta,ID_Venta,v.ID_Producto,p.NombreProducto,p.Imagen,cantidad,Precio, v.descuento, 
v.Usuario_Inserta,v.Fecha_Inserta,v.Usuario_Actualiza,v.Fecha_Actualiza 
from DetalleVenta v inner join Productos p on p.ID_Producto=v.ID_Producto
go


--modificacion de la tabla de producto para agregar la imagen del producto
Alter table Productos add Imagen image
go

----------Modificacion sp Producto mas Vendido----
 alter procedure sp_ProMasV
as
begin
  begin try
    if not exists(select*from DetalleVenta)
	begin
	raiserror('No hay Productos Vendidos',16,1)
	end
     select top 5 p.NombreProducto as Producto,sum(d.cantidad)as Total from DetalleVenta d inner join Productos p
     on d.ID_Producto=p.ID_Producto
     group by p.NombreProducto
     order by total desc
	 end try
	 begin catch
	 select ERROR_MESSAGE() Mensaje
	 end catch
	end
go

create view [dbo].[V_Producto]
as
	select
	p.ID_Producto,
	NombreProducto,
	Categoria,
	NombreEmpresa,
	PrecioCompra,
	PrecioVenta,
	Descuento,
	cantidadDisponible,
	imagen,
	ISNULL(p.Usuario_Actualiza,p.Usuario_Inserta) UltimoUsuarioActualiza,
	ISNULL(p.Fecha_Actualiza,p.Fecha_Inserta) UltimoFechaActualiza
	from Productos p 
	inner join Categorias c on p.ID_Categoria=c.ID_Categoria inner join Proveedores pr on p.ID_Proveedor=pr.ID_Proveedor
GO

--SP que actualiza productos
create proc [dbo].[SP_ACTUALIZAR_PRODUCTOS](
@ID_Producto int,
@ID_Categoria int,
@ID_Proveedor int,
@PrecioCompra money,
@PrecioVenta money,
@Descuento money,
@cantidadDisponible int,
@Usuario_Actualiza varchar(50)
)
as
begin
--Condicion IF para que actualice segun la existencia del ID del producto 
if(select count(*) from Productos
where ID_Producto = @ID_Producto) = 1 --si el ID existe procede a actualizar
update Productos
set ID_Categoria = @ID_Categoria, ID_Proveedor = @ID_Proveedor, PrecioCompra = @PrecioCompra, 
PrecioVenta = @PrecioVenta, Descuento = @Descuento, cantidadDisponible = @cantidadDisponible, 
Usuario_Actualiza = @Usuario_Actualiza, Fecha_Actualiza = getdate()
where ID_Producto = @ID_Producto
else  --si el ID no existe, envia un mensaje de error que el ID no existe
print 'Error, no se pudo actualizar porque el ID del producto no existe' 
end

GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINAR_PRODUCTOS]    Script Date: 01/12/2022 23:24:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------------------------------------------
--SP que elimina productos
create proc [dbo].[SP_ELIMINAR_PRODUCTOS]
@ID_Producto int
as
begin
--Condicion IF para eliminar productos por medio del ID
if(select count(*) from Productos
where ID_Producto = @ID_Producto) = 1 -- si existe 1 producto con ese ID procede a eliminar
delete from Productos
where ID_Producto = @ID_Producto
else --si no existe 1 prodcuto con ese ID manda un mensaje de error
print 'Error, no se pudo eliminar porque no existe el producto'
end

GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_PRODUCTOS]    Script Date: 01/12/2022 23:24:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--SP que inserta nuevos productos
create proc [dbo].[SP_INSERTAR_PRODUCTOS]
@ID_Categoria int,
@ID_Proveedor int,
@NombreProducto varchar(50),
@PrecioCompra money,
@PrecioVenta money,
@Descuento money,
@cantidadDisponible int,
@Usuario_Inserta varchar(50)
as
begin
--Condicion IF para realizar al validacion del nombre del producto y que no este repetido
if(select count(*) from Productos
where NombreProducto = @NombreProducto) = 0 --si no exisste ningun nombre igual
insert into Productos(ID_Categoria, ID_Proveedor, NombreProducto, PrecioCompra,
PrecioVenta, Descuento, cantidadDisponible, Usuario_Inserta, Fecha_Inserta)
values(@ID_Categoria, @ID_Proveedor, @Nombreproducto, @PrecioCompra, @PrecioVenta,
@Descuento, @cantidadDisponible, @Usuario_Inserta, getdate()) --si es nombre es valido inserta el nuevo producto
else --sino es valido, envia un mensaje de error y especifica cual es el error
print 'Error al ingresar el nuevo producto, ya existe un producto con el nombre: ' + @NombreProducto
end

GO