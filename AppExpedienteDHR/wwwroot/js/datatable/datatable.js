function saveTableState(tableId) {
    const state = $(`#${tableId}`).DataTable().state();
    localStorage.setItem(`${tableId}_tableState`, JSON.stringify(state));
}

function loadDataTable(tableId, ajaxUrl, columns) {
    const savedState = localStorage.getItem(`${tableId}_tableState`);
    const dataTableConfig = {
        "processing": true,
        "ajax": {
            "url": ajaxUrl,
            "type": "GET",
            "datatype": "json"
        },
        "columns": columns,
        "stateSave": true,
        "stateDuration": -1, // Guarda el estado indefinidamente
        "language": {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    };

    if (savedState) {
        dataTableConfig.stateLoadCallback = function () {
            return JSON.parse(savedState);
        };
    }

    const dataTable = $(`#${tableId}`).DataTable(dataTableConfig);

    // Limpiar el estado almacenado después de cargarlo
    if (savedState) {
        localStorage.removeItem(`${tableId}_tableState`);
    }

    return dataTable;
}

function Delete(url) {
    Swal.fire({
        title: '¿Estás seguro de que quieres borrar?',
        text: 'No podrás recuperar los datos borrados',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        confirmButtonText: 'Sí, borrar',
        cancelButtonText: 'Cancelar',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        $(`#${tableId}`).DataTable().ajax.reload(); // Recarga la tabla
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}