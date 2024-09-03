
/**
 * Funcion permite iniccializar la tabla de grupos
 * @param {any} flowId , id del flujo
 * @param {any} tableGroupId , id de la tabla
 */
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
                       <a href="#" class="btn btn-success text-white" style="cursor:pointer" onclick="editGroup(${flowId},${data})">
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
/**
 * Funcion permite cargar el formulario de grupo ya sea para crear o editar
 * @param {any} flowId, id del flujo
 * @param {any} groupId, id del grupo
 */
function loadGroupForm(flowId, groupId = 0) {
    console.log(`testCargando formulario de grupo para el flujo ${flowId} y el grupo ${groupId}`);
    const url = groupId ? `/admin/GroupWf/GetGroup/${groupId}` : `/admin/GroupWf/GetGroupForm?flowId=${flowId}`;
    $.get(url, function (data) {
        $('#editFormContainer').html(data);
        initializeGroupUserSelect();
        $.validator.unobtrusive.parse('#groupForm'); // Reinicializar validaciones

    });
}


/**
 * Funcion permite validar el formulario de grupo
 * @returns, retorna si es valido o no
 */

function validateGroupForm() {
    let isValid = true;
    const orderInput = $('#Order');
    const nameInput = $('#Name');

    // Validar el campo Order
    if (orderInput.val().trim() === '') {
        orderInput.addClass('is-invalid');
        orderInput.next('.text-danger').text('El campo Orden es requerido.');
        isValid = false;
    } else {
        orderInput.removeClass('is-invalid');
        orderInput.next('.text-danger').text('');
    }

    // Validar el campo Name
    if (nameInput.val().trim() === '') {
        nameInput.addClass('is-invalid');
        nameInput.next('.text-danger').text('El campo Nombre es requerido.');
        isValid = false;
    } else {
        nameInput.removeClass('is-invalid');
        nameInput.next('.text-danger').text('');
    }
    return isValid;
}

/**
 * Funcion permite guardar el grupo
 * @returns
 */

function saveGroup() {

    if (!validateGroupForm()) {
        return; // Detener si el formulario no es válido
    }

    const form = $('#groupForm');

    // Obtener todos los usuarios seleccionados en el Select2
    const selectedUsers = $('#Users').val();

    console.log(selectedUsers);

    // Crear un objeto que contenga todos los datos del formulario y los usuarios seleccionados
    const formData = form.serializeArray();

    console.log(formData);

    //// Agregar los usuarios seleccionados al objeto de datos del formulario
    //selectedUsers.forEach(user => {
    //    formData.push({ name: 'selectedUsers', value: user });
    //});
     //Enviar la solicitud AJAX
    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: formData,
        success: function (response) {
            if (response.success) {
                // Recargar la tabla de grupos
                $('#tblGroups').DataTable().ajax.reload();
                clearForm();
            } else {
                // Mostrar errores de validación
                $('#editFormContainer').html(response);
            }
        }
    });
}


/**
 * Funcion permite editar un grupo
 * @param {any} flowId, id del flujo
 * @param {any} groupId, id del grupo
 */
function editGroup(flowId, groupId) {
    loadGroupForm(flowId, groupId);
}

/**
 * funcion permite borrar un grupo
 * @param {any} groupId, id del grupo a borrar
 */
function deleteGroup(groupId) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: "No podrás revertir esta acción!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminarlo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/admin/GroupWf/DeleteGroup/${groupId}`,
                type: 'DELETE',
                success: function (response) {
                    if (response.success) {
                        $('#tblGroups').DataTable().ajax.reload();
                        Swal.fire(
                            'Eliminado!',
                            'El grupo ha sido eliminado.',
                            'success'
                        );
                    } else {
                        Swal.fire(
                            'Error!',
                            'Hubo un problema al eliminar el grupo.',
                            'error'
                        );
                    }
                }
            });
        }
    });
}

/**
 * Funcion permite limpiar el formulario
 
 */

function clearForm() {
    $('#editFormContainer').html('');
}

/**
 * Funcion permite inicializar el select2 de usuarios
 */

function initializeGroupUserSelect() {
    $('#Users').select2({
        ajax: {
            url: '/Admin/user/SearchUsers', // Ruta del controlador para buscar usuarios
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    search: params.term // término de búsqueda
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data.users, function (item) { // Accede al array 'users'
                        return {
                            text: item.fullName, // Texto que se mostrará en el select
                            id: item.id // Valor del item en el select
                        };
                    })
                };
            },
            cache: true
        },
        minimumInputLength: 1,
        placeholder: 'Seleccione usuarios',
        allowClear: true
    });
}


/**
* Funcion permite iniccializar la tabla de grupos
* @param {any} flowId , id del flujo
* @param {any} tableStateId , id de la tabla
*/
function initializeStateDataTable(flowId, tableStateId) {
    console.log(`Inicializando tabla de Estados para el flujo ${flowId} con el id ${tableStateId}`);
    const ajaxUrl = `/admin/StateWf/GetStates/${flowId}`;
    const columns = [
        { "data": "order", "width": "10%" },
        { "data": "name", "width": "70%" },
        {
            "data": "id",
            "render": function (data) {
                return `
                    <div class="text-center">
                       <a href="#" class="btn btn-success text-white" style="cursor:pointer" onclick="editState(${flowId},${data})">
                            <i class="fas fa-edit"></i>
                        </a> &nbsp;
                        <a href="#" class="btn btn-danger text-white" style="cursor:pointer" onclick="deleteState(${data})">
                            <i class="fas fa-trash-alt"></i>
                        </a>
                    </div>
                `;
            },
            "width": "20%"
        }
    ];

    // Inicializar la tabla
    const dataTable = loadDataTable(tableStateId, ajaxUrl, columns);

    // Asociar la función de guardado al botón de edición
    $(document).on('click', `.btn-edit-${tableStateId}`, function () {
        saveTableState(tableStateId);
    });
}


/**
* Funcion permite cargar el formulario de grupo ya sea para crear o editar
* @param {any} flowId, id del flujo
* @param {any} stateId, id del grupo
*/
function loadStateForm(flowId, stateId = 0) {
    console.log(`testCargando formulario de grupo para el flujo ${flowId} y el grupo ${stateId}`);
    const url = stateId ? `/admin/StateWf/GetState/${stateId}` : `/admin/StateWf/GetStateForm?flowId=${flowId}`;
    $.get(url, function (data) {
        $('#editFormContainer').html(data);
        $.validator.unobtrusive.parse('#stateForm'); // Reinicializar validaciones

    });
}


/**
 * Funcion permite validar el formulario de grupo
 * @returns, retorna si es valido o no
 */

function validateStateForm() {
    let isValid = true;
    const orderInput = $('#Order');
    const nameInput = $('#Name');

    // Validar el campo Order
    if (orderInput.val().trim() === '') {
        orderInput.addClass('is-invalid');
        orderInput.next('.text-danger').text('El campo Orden es requerido.');
        isValid = false;
    } else {
        orderInput.removeClass('is-invalid');
        orderInput.next('.text-danger').text('');
    }

    // Validar el campo Name
    if (nameInput.val().trim() === '') {
        nameInput.addClass('is-invalid');
        nameInput.next('.text-danger').text('El campo Nombre es requerido.');
        isValid = false;
    } else {
        nameInput.removeClass('is-invalid');
        nameInput.next('.text-danger').text('');
    }
    return isValid;
}

/**
 * Funcion permite guardar el grupo
 * @returns
 */

function saveState() {

    if (!validateStateForm()) {
        return; // Detener si el formulario no es válido
    }

    const form = $('#stateForm');



    // Crear un objeto que contenga todos los datos del formulario y los usuarios seleccionados
    const formData = form.serializeArray();

    console.log(formData);

    //// Agregar los usuarios seleccionados al objeto de datos del formulario
    //selectedUsers.forEach(user => {
    //    formData.push({ name: 'selectedUsers', value: user });
    //});
    //Enviar la solicitud AJAX
    $.ajax({
        url: form.attr('action'),
        type: 'POST',
        data: formData,
        success: function (response) {
            if (response.success) {
                // Recargar la tabla de grupos
                $('#tblStates').DataTable().ajax.reload();
                clearForm();
            } else {
                // Mostrar errores de validación
                $('#editFormContainer').html(response);
            }
        }
    });
}


/**
 * Funcion permite editar un grupo
 * @param {any} flowId, id del flujo
 * @param {any} stateId, id del grupo
 */
function editState(flowId, stateId) {
    loadStateForm(flowId, stateId);
}

/**
 * funcion permite borrar un grupo
 * @param {any} stateId, id del grupo a borrar
 */
function deleteState(stateId) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: "No podrás revertir esta acción!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminarlo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: `/admin/StateWf/DeleteState/${stateId}`,
                type: 'DELETE',
                success: function (response) {
                    if (response.success) {
                        $('#tblStates').DataTable().ajax.reload();
                        Swal.fire(
                            'Eliminado!',
                            'El estado ha sido eliminado.',
                            'success'
                        );
                    } else {
                        Swal.fire(
                            'Error!',
                            'Hubo un problema al eliminar el estado.',
                            'error'
                        );
                    }
                }
            });
        }
    });
}
