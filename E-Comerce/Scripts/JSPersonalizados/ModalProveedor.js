$(".btnAgrega").click(function (eve) {
    $(".form").load("/Proveedor/Create");
});
$(".btnUpdate").click(function (eve) {
    $(".form").load("/Proveedor/Edit/" + $(this).data("id"));
});
$(".btnDetail").click(function (eve) {
    $(".form").load("/Proveedor/Details/" + $(this).data("id"));
});
$(".btnDelete").click(function (eve) {
    $(".form").load("/Proveedor/Delete/" + $(this).data("id"));
});




