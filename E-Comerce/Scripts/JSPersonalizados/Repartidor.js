$("#btnAgregar").click(function (eve) {
    $(".modal-body").load("/Repartidor/Create");
});
$(".btnEditar").click(function (eve) {
    $(".modal-body").load("/Repartidor/Edit/" + $(this).data("id"));
});
$(".btnDetail").click(function (eve) {
    $(".modal-body").load("/Repartidor/Details/" + $(this).data("id"));
});
$(".btnEliminar").click(function (eve) {
    $(".modal-body").load("/Repartidor/Delete/" + $(this).data("id"));
});
