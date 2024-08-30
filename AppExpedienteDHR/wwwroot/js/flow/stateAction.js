function loadActionForm(stateId, actionId = 0) {
    const url = actionId ? `/admin/ActionWf/GetAction/${actionId}` : `/admin/ActionWf/GetActionForm?stateId=${stateId}`;
    $.get(url, function (data) {
        $('#actionFormContainer').html(data);
        $.validator.unobtrusive.parse('#actionForm'); // Reinicializar validaciones
        $('#actionModal').modal('show');
    });
}

function saveAction() {
    const form = $('#actionForm');
    // get stateId from hidden input
    //const stateId = form.find('input[name="StateId"]').val
    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                $('#actionModal').modal('hide');
                //loadActionsTable(stateId); // Recargar la tabla de acciones
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
    if (confirm('¿Estás seguro de que quieres borrar esta acción?')) {
        $.ajax({
            url: `/admin/ActionWf/DeleteAction/${actionId}`,
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    $('#stateForm').submit(); // Recargar el formulario del estado para mostrar las acciones actualizadas
                }
            }
        });
    }
}