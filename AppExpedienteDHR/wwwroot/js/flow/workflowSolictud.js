// Función para cargar las acciones disponibles dinámicamente
function cargarAccionesDisponibles(flowId, currentStateId) {
    $.get('/General/Workflow/GetAvailableActions', { flowId: flowId, currentStateId: currentStateId }, function (data) {
        // Limpiar el select de acciones antes de llenarlo con nuevas opciones
        $('#actionSelect').empty();
        $('#actionSelect').append('<option value="">Seleccione una acción</option>');

        // Recorrer las acciones disponibles y agregarlas al select
        console.log(data);
        $.each(data, function (index, action) {
            $('#actionSelect').append('<option value="' + action.id + '">' + action.name + '</option>');
        });
    }).fail(function () {
        alert('Error al cargar las acciones disponibles.');
    });
}

// Función para guardar y procesar acción
function guardarYProcesarAccion(flowHeaderId) {
    // Guardar la solicitud antes de procesar la acción
    const form = $('#formSeleccionarAccion');
    const actionId = $('#actionSelect').val();
    const comments = $('#actionComments').val();

    if (!actionId) {
        Swal.fire({
            icon: 'warning',
            title: 'Acción requerida',
            text: 'Debe seleccionar una acción.',
        });
        return;
    }

    if (!comments) {
        Swal.fire({
            icon: 'warning',
            title: 'Comentarios requeridos',
            text: 'Debe ingresar comentarios.',
        });
        return;
    }

    console.log(`flowHeaderId: ${flowHeaderId}`);


    // Primera llamada: Guardar la solicitud
    $.post('/Expediente/Solicitud/Save', $('#formExpediente').serialize(), function () {
        // Segunda llamada: Procesar la acción en el flujo
        $.post('/General/Workflow/ProcessAction', {
            requestId: flowHeaderId,
            actionId: actionId,
            comments: comments
        }, function (response) {
            // Mostrar mensaje de confirmación antes de redirigir
            Swal.fire({
                icon: 'success',
                title: 'Proceso exitoso',
                text: 'La acción se ha procesado correctamente.',
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.isConfirmed) {
                    const requestId = response.requestId;
                    const requestType = response.requestType;
                    const redirectUrl = `/${requestType}/Solicitud/Info/${requestId}`;
                    window.location.href = redirectUrl;
                }
            });

        }).fail(function () {
            alert("Error al procesar la acción.");
        });
    }).fail(function () {
        alert("Error al guardar la solicitud.");
    });
}