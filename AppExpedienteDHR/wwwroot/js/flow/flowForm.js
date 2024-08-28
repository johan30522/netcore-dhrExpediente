function initializeGroupDataTable(flowId, tableGroupId) {
    console.log(`Inicializando tabla de grupos para el flujo ${flowId} con el id ${tableGroupId}`);
    const ajaxUrl = `/admin/flowwf/GetGroups/${flowId}`;
    const columns = [
        { "data": "order", "width": "10%" },
        { "data": "name", "width": "70%" },
        {
            "data": "id",
            "render": function (data) {
                return `
                    <div class="text-center">
                       <a href="#" class="btn btn-success text-white" style="cursor:pointer" onclick="editGroup(${data})">
                            <i class="fas fa-edit"></i>
                        </a> &nbsp;
                        <a href="#" class="btn btn-danger text-white" style="cursor:pointer" onclick="deleteGroup(${data})">
                            <i class="fas fa-trash-alt"></i>
                        </a>
                    </div>
                `;
            },
            "width": "20%"
        }
    ];

    // Inicializar la tabla
    const dataTable = loadDataTable(tableGroupId, ajaxUrl, columns);

    // Asociar la función de guardado al botón de edición
    $(document).on('click', `.btn-edit-${tableGroupId}`, function () {
        saveTableState(tableGroupId);
    });
}
function loadGroupForm(flowId, groupId = 0) {
    const url = groupId ? `/admin/GroupWf/GetGroup/${groupId}` : `/admin/GroupWf/GetGroupForm?flowId=${flowId}`;
    $.get(url, function (data) {
        $('#groupFormContainer').html(data);
    });
}

function saveGroup() {
    const form = $('#groupForm');
    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                // Recargar la tabla de grupos
                $('#tblGroups').DataTable().ajax.reload();
                clearForm();
            } else {
                // Mostrar errores de validación
                $('#groupFormContainer').html(response);
            }
        }
    });
}

function editGroup(flowId, groupId) {
    loadGroupForm(flowId, groupId);
}

function deleteGroup(groupId) {
    if (confirm('¿Estás seguro de que quieres borrar este grupo?')) {
        $.ajax({
            url: `/admin/GroupWf/DeleteGroup/${groupId}`,
            type: 'DELETE',
            success: function (response) {
                if (response.success) {
                    $('#tblGroups').DataTable().ajax.reload();
                }
            }
        });
    }
}

function clearForm() {
    $('#groupFormContainer').html('');
}