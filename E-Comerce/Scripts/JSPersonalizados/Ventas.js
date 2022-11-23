$("#btnAgregar").click(function (eve) {
    $(".modal-body").load("/Ventas/Create");
});
$("#btnFinalizar").click(function (eve) {
    $(".modal-body").load("/Ventas/Finalizar");
});
$(".btnEditar").click(function (eve) {
    $(".modal-body").load("/Ventas/Edit/" + $(this).data("id"));
});
$(".btnEliminar").click(function (eve) {
    $(".modal-body").load("/Ventas/Delete/" + $(this).data("id"));
});
