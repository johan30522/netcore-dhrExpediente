function loadActionForm(stateId, actionId = 0) {
    const url = actionId ? `/admin/ActionWf/GetAction/${actionId}` : `/admin/ActionWf/GetActionForm?stateId=${stateId}`;
    $.get(url, function (data) {
        $('#actionFormContainer').html(data);
        $.validator.unobtrusive.parse('#actionForm'); // Reinicializar validaciones
        $('#actionModal').modal('show');
        InitActionForm(); // Inicializar eventos del formulario
        // Habilitar scroll en el contenedor del formulario
        console.log('Habilitando scroll en el contenedor del formulario ambos');
        // Habilitar scroll vertical en el contenedor del formulario
        $('#actionFormContainer').css({
            'max-height': '70vh',
            'overflow-y': 'auto',
            'overflow-x': 'hidden' // Asegurarse de que el scroll horizontal esté deshabilitado
        });

        // Habilitar scroll vertical en el contenedor del modal
        $('#actionModal .modal-body').css({
            'max-height': '70vh',
            'overflow-y': 'auto',
            'overflow-x': 'hidden' // Asegurarse de que el scroll horizontal esté deshabilitado
        });
 
    });
}

function saveAction() {



    const form = $('#actionForm');

    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: form.serialize(),
        success: function (response) {
            if (response.success) {

                loadActionsTable(response.stateId); // Recargar la tabla de acciones
            } else {
                $('#actionFormContainer').html(response);
            }
        }
    });
}

function loadActionsTable(stateId) {
    console.log(`Cargando acciones del estado ${stateId}...`);
    const url = `/admin/StateWf/GetActionsTable?stateId=${stateId}`;
    $.get(url, function (data) {
        $('#actionsTableContainer').html(data); // Aquí se actualizará solo la tabla de acciones
    });
}

function editAction(stateId, actionId) {
    loadActionForm(stateId, actionId);
}

function deleteAction(stateId, actionId) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: "No podrás revertir esta acción!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminarla!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/admin/ActionWf/DeleteAction/${actionId}`,
                type: 'POST',
                success: function (response) {
                    if (response.success) {
                        $('#stateForm').submit(); // Recargar el formulario del estado para mostrar las acciones actualizadas
                        Swal.fire(
                            'Eliminada!',
                            'La acción ha sido eliminada.',
                            'success'
                        );
                    } else {
                        Swal.fire(
                            'Error!',
                            'Hubo un problema al eliminar la acción.',
                            'error'
                        );
                    }
                }
            });
        }
    });
}

function InitActionForm() {
 
    // se obtiene el id del flujo del campo oculto FlowId  <input type="hidden" asp-for="FlowId" />
    const flowId = $('#FlowId').val();
    const selectedStateId = $('#NextState').data('selected'); // Obtener el estado seleccionado si existe


    $(document).ready(function () {
        // Mostrar el combo de estados cuando se selecciona la opción "Estático"
    InitializeActionGroupSelect(); 
        $('input[name="EvaluationType"]').change(function () {
            if ($('#staticEvaluation').is(':checked')) {
                $('#staticStateSelect').show();
                $('#rulesTableContainer').hide();
                loadStateOptions(flowId, selectedStateId);
                // oculta las reglas de acción
            } else {
                $('#rulesTableContainer').show();
                $('#staticStateSelect').hide();
                //loadRulesTable();
            }
        }).trigger('change'); // Trigger para inicializar correctamente al cargar la vista
    });


}


// 
function loadStateOptions(flowId, selectedStateId = null) {
    console.log(`Cargando estados del flujo ${flowId}...`);
    console.log(`selectedStateId: ${selectedStateId}`);
    $.ajax({
        url: '/admin/StateWf/GetStates/' + flowId,
        type: 'GET',
        success: function (data) {
            var select = $('#NextState');
            select.empty();
            select.append('<option value="">Seleccione un estado</option>');
            data.data.forEach(function (state) {
                select.append('<option value="' + state.id + '">' + state.name + '</option>');
            });

            if (selectedStateId) {
                select.val(selectedStateId);
            }
        }
    });


}


function InitializeActionGroupSelect() {
    console.log('Inicializando select2 de grupos');
    $('#Groups').select2({
        ajax: {
            url: '/Admin/GroupWf/SearchGroups', // Ruta del controlador para buscar grupos
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    search: params.term // término de búsqueda
                };
            },
            processResults: function (data) {
                console.log(data);
                return {
                    results: $.map(data.groups, function (item) { // Accede al array 'groups'
                        console.log(item);
                        return {
                            text: item.name, // Texto que se mostrará en el select
                            id: item.id // Valor del item en el select
                        };
                    })
                };
            },
            cache: true
        },
        minimumInputLength: 1,
        placeholder: 'Seleccione Grupos',
        allowClear: true,
        dropdownParent: $('#actionModal') 
    });
}
