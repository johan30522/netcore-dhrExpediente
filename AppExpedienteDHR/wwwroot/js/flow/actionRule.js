
function addRule(actionId) {
    const url = `/admin/ActionRuleWf/GetActionRuleForm?actionId=${actionId}`;
    $.get(url, function (data) {
        $('#ruleFormContainer').html(data);
        loadStateResultOptions();
    });
}

function editRule(ruleId) {
    //const url = ruleId ? `/admin/ActionWf/GetActionRule/${ruleId}` : `/admin/ActionWf/GetActionRuleForm?stateId=${stateId}`;  
    const url = `/admin/ActionRuleWf/GetActionRule?ruleId=${ruleId}`;
    $.get(url, function (data) {
        $('#ruleFormContainer').html(data);

        // Obtener el valor de ResultStateId del select cargado
        const selectedStateId = $('#ResultState').data('selected');

        loadStateResultOptions(selectedStateId);
    });
}

function saveRule() {
    const form = $('#ruleForm');
    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: form.serialize(),
        success: function (response) {
            if (response.success) {
                loadRulesTable(response.actionId); // Recargar la tabla de reglas
                clearRuleForm(); // Limpiar el formulario de reglas
            } else {
                $('#ruleFormContainer').html(response);
            }
        }
    });
}

function deleteRule(ruleId) {
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
                url: `/admin/ActionRuleWf/DeleteRule/${ruleId}`,
                type: 'POST',
                success: function (response) {
                    if (response.success) {
                        loadRulesTable(); // Recargar la tabla de reglas
                        Swal.fire(
                            'Eliminada!',
                            'La regla ha sido eliminada.',
                            'success'
                        );
                    } else {
                        Swal.fire(
                            'Error!',
                            'Hubo un problema al eliminar la regla.',
                            'error'
                        );
                    }
                }
            });
        }
    });
}

function loadRulesTable() {
    const actionId = $('#actionForm input[name="Id"]').val();
    const url = `/admin/ActionWf/GetActionRulesTable?actionId=${actionId}`;
    $.get(url, function (data) {
        $('#rulesTablePartial').html(data); // Aquí se actualizará solo la tabla de reglas
    });
}

function clearRuleForm() {
    $('#ruleFormContainer').html(''); // Limpiar el formulario de reglas
}

function loadStateResultOptions(selectedStateId = null) {
    console.log(`selectedStateId: ${selectedStateId}`);
    const flowId = $('#FlowId').val();
    console.log(`Cargando estados del flujo ${flowId}...`);
    $.ajax({
        url: '/admin/StateWf/GetStates/' + flowId,
        type: 'GET',
        success: function (data) {
            var select = $('#ResultState');
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