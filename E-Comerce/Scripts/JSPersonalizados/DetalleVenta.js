$(".btnDetailles").click(function (eve) {
    $(".modal-body").load("/ModificarVentas/Details/" + $(this).data("id"));
});
$(".btnEditar").click(function (eve) {
    $(".modal-body").load("/ModificarVentas/EditarDetalle/" + $(this).data("id"));
});
$(".btnEliminar").click(function (eve) {
    $(".modal-body").load("/ModificarVentas/Delete/" + $(this).data("id"));
});

$(".btnEliminarVenta").click(function (eve) {
    $(".modal-body").load("/ModificarVentas/EliminarVenta/" + $(this).data("id"));
});
