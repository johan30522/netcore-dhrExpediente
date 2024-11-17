





$(document).ready(function () {


    //Inicializa el manejo de bloqueos de registro


    document.getElementById('btnBack').addEventListener('click', function () {
        const previousView = sessionStorage.getItem('previousView');
        if (previousView) {
            window.location.href = previousView; // Redirige a la vista anterior
        } else {
            window.history.back(); // O usa el historial si no hay vista guardada
        }
    });

    //Tabs
    // Restaurar la pestaña activa desde localStorage
    var activeTab = localStorage.getItem('activeTab');
    if (activeTab) {
        $('#v-pills-tab a[href="' + activeTab + '"]').tab('show');
    }

    // Guardar la pestaña activa en localStorage al cambiar de pestaña
    $('#v-pills-tab a').on('shown.bs.tab', function (e) {
        var tabId = $(e.target).attr('href');
        localStorage.setItem('activeTab', tabId);
    });

});








