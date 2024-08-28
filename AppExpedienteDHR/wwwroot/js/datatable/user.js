var dataTable;



console.log("user.js");
$(document).ready(function () {
    loadDataTable();
}
);

function saveTableState(tableId) {
    const state = $(`#${tableId}`).DataTable().state();
    localStorage.setItem(`${tableId}_tableState`, JSON.stringify(state));
}

 

function loadDataTable() {
    dataTable = $('#tblUsers').DataTable({
        "processing": true, 
        "ajax": {
            "url": "/admin/user/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "userName", "width": "10%" },
            { "data": "email", "width": "10%" },
            { "data": "fullName", "width": "10%" },
            { "data": "address", "width": "10%" },
            { "data": "phoneNumber", "width": "10%" },
            { "data": "position", "width": "10%" },
            {
                "data": "roles",
                "render": function (data) {
                    if (data && data.length > 0) {
                        return data.map(role => role.name).join(", ");
                    } else {
                        return " - ";
                    }
                },
                "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/admin/user/edit/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i>
                                </a> &nbsp;
                                <a onclick=Delete("/Admin/user/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                           `;
                }, "width": "20%"
            }
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay información",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
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
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}




