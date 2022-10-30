$("#btnDetails").click(function (eve) {
    $(".modal-body").load("/ModificarVentas/Details/" + $(this).data("id"));
});
$("#btnEdit").click(function (eve) {
    $(".modal-body").load("/ModificarVentas/Edit/" + $(this).data("id"));
});