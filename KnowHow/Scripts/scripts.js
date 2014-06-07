$(document).ready(function () {
    $("#CategoriaId").change(function() {
        $("#buscar").submit();
    });

    $("#Participar").click(function () {
        $("#FormularioDeParticipacao").dialog();
    });
});