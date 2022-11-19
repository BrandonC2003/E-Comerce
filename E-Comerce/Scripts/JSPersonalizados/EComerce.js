function MostrarModal(IdBoton, ModalAccion) {
    $("#" + IdBoton).click(function (eve) {
        $(".modal-body").load(ModalAccion);
    });
}