// Función para cargar las acciones disponibles dinámicamente
function cargarAccionesDisponibles(flowId, currentStateId) {

    console.log('intenta cargar acciones disponibles');
    console.log(flowId);
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
let isProcessing = false;

function guardarYProcesarAccion(flowHeaderId, requestType) {
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

    // Configurar el estado de procesamiento
    isProcessing = true;
    window.onbeforeunload = function () {
        return isProcessing ? "La solicitud está en proceso. ¿Seguro que deseas salir?" : null;
    };

    // Mostrar el spinner y deshabilitar los elementos del modal
    $('#processingSpinner').removeClass('d-none'); // Muestra el spinner
    $('#modalSeleccionarAccion .form-control, #btnContinuar, .btn-close').prop('disabled', true); // Deshabilitar todos los controles

    // Procesar la solicitud
    $.post(`/${requestType}/Solicitud/Save`, $('#formExpediente').serialize(), function () {
        $.post('/General/Workflow/ProcessAction', {
            requestId: flowHeaderId,
            actionId: actionId,
            comments: comments
        }, function (response) {
            Swal.fire({
                icon: 'success',
                title: 'Proceso exitoso',
                text: 'La acción se ha procesado correctamente.',
                confirmButtonText: 'OK'
            }).then((result) => {
                if (result.isConfirmed) {
                    const redirectUrl = `/${response.requestType}/Solicitud/Info/${response.requestId}`;
                    isProcessing = false; // Desactivar confirmación de navegación
                    window.location.href = redirectUrl;
                }
            });
        }).fail(function () {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Error al procesar la acción.'
            });
        }).always(function () {
            // Restaurar el estado original después del procesamiento
            $('#processingSpinner').addClass('d-none'); // Oculta el spinner
            $('#modalSeleccionarAccion .form-control, #btnContinuar, .btn-close').prop('disabled', false); // Habilitar todos los controles
            isProcessing = false; // Desactivar confirmación de navegación
            window.onbeforeunload = null;
        });
    }).fail(function () {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Error al guardar la solicitud.'
        });
    }).always(function () {
        $('#processingSpinner').addClass('d-none'); // Oculta el spinner
        $('#modalSeleccionarAccion .form-control, #btnContinuar, .btn-close').prop('disabled', false); // Habilitar todos los controles
        isProcessing = false;
        window.onbeforeunload = null;
    });
}


// Destruir y recargar DataTable al abrir el modal+

console.log('flowHistoryModal 222');


function loadFlowHistory(requestFlowHeaderId) {
    if (!$.fn.DataTable.isDataTable('#flowHistoryTable')) {
        $('#flowHistoryTable').DataTable({
            processing: true,
            scrollX: true, // Habilitar scroll horizontal si es necesario
            autoWidth: false, // Evitar que se compriman las columnas automáticamente
            ajax: {
                url: `/General/Workflow/GetFlowHistory?requestFlowHeaderId=${requestFlowHeaderId}`,
                type: 'GET',
                datatype: 'json'
            },
            columns: [
                {
                    className: 'details-control', // Esta columna contendrá el botón de expandir
                    orderable: false,
                    data: null,
                    defaultContent: '<i class="fas fa-plus-circle"></i>',
                    width: '5%'
                },
                {
                    data: 'actionDate',
                    width: '20%',
                    render: function (data, type, row) {
                        // Convertir el formato de la fecha de DD/MM/YYYY HH:mm a YYYY-MM-DDTHH:mm
                        var parts = data.split(' '); // Separar la fecha y la hora
                        var dateParts = parts[0].split('/'); // Separar día, mes y año

                        // Crear una cadena en formato ISO (YYYY-MM-DDTHH:mm)
                        var isoDate = `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}T${parts[1]}`;

                        // Crear un objeto Date usando la cadena en formato ISO
                        var date = new Date(isoDate);

                        // Retornar la fecha y hora en formato local de 12 horas (AM/PM)
                        return date.toLocaleString('es-ES', {
                            year: 'numeric',
                            month: '2-digit',
                            day: '2-digit',
                            hour: '2-digit',
                            minute: '2-digit',
                            second: '2-digit',
                            hour12: true // Esto habilita el formato de 12 horas con AM/PM
                        });
                    }
                },
                { data: 'performedByUser', width: '15%' },
                { data: 'previousState', width: '15%' },
                { data: 'newState', width: '15%' },
                { data: 'actionPerformed', width: '15%' }
            ],
            order: [[1, 'asc']], // Orden por fecha
            language: {
                decimal: ",",
                emptyTable: "No hay información",
                info: "Mostrando _START_ a _END_ de _TOTAL_ entradas",
                infoEmpty: "Mostrando 0 a 0 de 0 entradas",
                infoFiltered: "(filtrado de _MAX_ entradas totales)",
                lengthMenu: "Mostrar _MENU_ entradas",
                loadingRecords: "Cargando...",
                processing: "Procesando...",
                search: "Buscar:",
                zeroRecords: "No se encontraron resultados",
                paginate: {
                    first: "Primero",
                    last: "Último",
                    next: "Siguiente",
                    previous: "Anterior"
                },
                aria: {
                    sortAscending: ": activar para ordenar la columna de manera ascendente",
                    sortDescending: ": activar para ordenar la columna de manera descendente"
                }
            },
            destroy: true
        });
    } else {
        $('#flowHistoryTable').DataTable().ajax.url(`/General/Workflow/GetFlowHistory?requestFlowHeaderId=${requestFlowHeaderId}`).load();
    }

    // Desregistrar cualquier evento anterior antes de configurar el nuevo
    $('#flowHistoryTable tbody').off('click', 'td.details-control');


    // Configurar el evento para abrir los comentarios
    $('#flowHistoryTable tbody').on('click', 'td.details-control', function () {
        var table = $('#flowHistoryTable').DataTable();
        var tr = $(this).closest('tr');
        var row = table.row(tr);
        var icon = $(this).find('i');

        if (row.child.isShown()) {
            // Si está abierto, lo cerramos y cambiamos el ícono a "expandir"
            row.child.hide();
            tr.removeClass('shown');
            icon.removeClass('fa-minus-circle').addClass('fa-plus-circle');
        } else {
            // Si está cerrado, lo abrimos y cambiamos el ícono a "colapsar"
            row.child(format(row.data())).show();
            tr.addClass('shown');
            icon.removeClass('fa-plus-circle').addClass('fa-minus-circle');
        }
    });
    // Ajustar las columnas del DataTable
    //$('#flowHistoryTable').DataTable().columns.adjust().draw();
}


// Función para formatear los detalles de la fila (mostrar los comentarios)
function format(data) {
    return `<div class="comment-details">
                <strong>Comentarios:</strong> ${data.comments}
            </div>`;
}


// Al mostrar el modal, redibujar el DataTable
$('#flowHistoryModal').on('shown.bs.modal', function () {
    $('#flowHistoryTable').DataTable().columns.adjust().draw(); // Ajustar las columnas del DataTable
});