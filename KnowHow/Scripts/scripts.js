$(document).ready(function () {
    $("#CategoriaId").change(function() {
        $("#buscar").submit();
    });

    $("#Participar").click(function () {
        $("#FormularioDeParticipacao").dialog();
    });

    $("#enviar").click(function() {
        $(".mensagem-sucesso").show().delay( 800 ).queue(function () {
            location.href = "/";
            $(this).dequeue();
        });
    });
});