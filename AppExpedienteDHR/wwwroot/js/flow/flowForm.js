﻿
/**
 * Funcion permite iniccializar la tabla de grupos
 * @param {any} flowId , id del flujo
 * @param {any} tableGroupId , id de la tabla
 */
function initializeGroupDataTable(flowId, tableGroupId) {
    const ajaxUrl = `/admin/GroupWf/GetGroups/${flowId}`;
    const columns = [
        { "data": "order", "width": "10%" },
        { "data": "name", "width": "70%" },
        {
            "data": "id",
            "render": function (data) {
                return `
                    <div class="text-center">
                       <a href="#" class="btn btn-success btn-sm text-white" style="cursor:pointer" onclick="editGroup(${flowId},${data});return false;">
                            <i class="fas fa-edit"></i>
                        </a>
                        &nbsp;
                        <a href="#" class="btn btn-danger btn-sm text-white " style="cursor:pointer" onclick="deleteGroup(${data});return false;">
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

    // Guardar la posición actual del scroll
    const scrollPosition = $(window).scrollTop();

    $('#editFormContainer').hide(); // Ocultar el contenedor del formulario


    const url = groupId ? `/admin/GroupWf/GetGroup/${groupId}` : `/admin/GroupWf/GetGroupForm?flowId=${flowId}`;
    //const url = `/admin/GroupWf/GetGroupForm?flowId=${flowId}`;
    $.get(url, function (data) {
        $('#editFormContainer').html(data);
        initializeGroupUserSelect();
        $.validator.unobtrusive.parse('#groupForm'); // Reinicializar validaciones

        // Restaurar la posición del scroll
        $(window).scrollTop(scrollPosition);

        $('#editFormContainer').fadeIn(); // Mostrar el contenedor del formulario
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


    // Crear un objeto que contenga todos los datos del formulario y los usuarios seleccionados
    const formData = form.serializeArray();

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
    const ajaxUrl = `/admin/StateWf/GetStates/${flowId}`;
    const columns = [
        { "data": "order", "width": "10%" },
        { "data": "name", "width": "70%" },
        {
            "data": "id",
            "render": function (data) {
                return `
                    <div class="text-center">
                       <a href="#" class="btn btn-success btn-sm text-white" style="cursor:pointer" onclick="editState(${flowId},${data});return false;">
                            <i class="fas fa-edit"></i>
                        </a> &nbsp;
                        <a href="#" class="btn btn-danger btn-sm text-white" style="cursor:pointer" onclick="deleteState(${data});return false;">
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
    const url = stateId ? `/admin/StateWf/GetState/${stateId}` : `/admin/StateWf/GetStateForm?flowId=${flowId}`;
    $('#editFormContainer').hide(); // Ocultar el contenedor del formulario

    $.get(url, function (data) {
        $('#editFormContainer').html(data);
        $.validator.unobtrusive.parse('#stateForm'); // Reinicializar validaciones
        $('#editFormContainer').fadeIn(); // Mostrar el contenedor del formulario

        // Inicializar el estado de las secciones basadas en el checkbox
        toggleActionSections(); // Mostrar u ocultar la sección según el estado actual

        // Manejar el cambio del checkbox IsFinalState
        $('#isFinalStateCheckbox').off('change').on('change', function () {
            if ($(this).is(':checked')) {
                $('#isInitialStateCheckbox').prop('checked', false); // Deschequear el checkbox IsInitialState
            }
            toggleActionSections(); // Mostrar u ocultar la sección según el estado del checkbox
        });

        // Manejar el cambio del checkbox IsInitialState
        $('#isInitialStateCheckbox').off('change').on('change', function () {
            if ($(this).is(':checked')) {
                $('#isFinalStateCheckbox').prop('checked', false); // Deschequear el checkbox IsFinalState
            }
            toggleActionSections(); // Asegurar que las acciones se muestren si no es un estado final
        });


        //Iniciliza campos de Notificacion

        $('#notifications-active').change(function () {
            const isActive = $(this).is(':checked');
            $('#group-selection, #recipient-emails, #email-template-selection').toggle(isActive);
        });

        // inicializa correos al cargar la página (modo edición)
        initializeNotificationEmailsTo();

        // Cargar de Correo
        loadEmailTemplates();

        // Inicializa el select2 para seleccionar los grupos
        initializeGroupSelect();

    });



}
// Funcion para inicializar el Select2 de grupos
function initializeGroupSelect() {
    //Inicializa el select2 para seleccionar los grupos 
    $('#Notification-Groups').select2({
        placeholder: "Seleccione los grupos",
        allowClear: true,
        width: '100%',
        language: {
            noResults: function () {
                return "No se encontraron resultados";
            }
        },
        ajax: {
            url: '/Admin/GroupWf/SearchGroups', // Cambia la ruta según tu controlador
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    search: params.term // Término buscado
                };
            },
            processResults: function (data) {
                return {
                    results: $.map(data.groups, function (item) { // Accede al array 'groups'
                        return {
                            text: item.name, // Texto que se mostrará en el select
                            id: item.id // Valor del item en el select
                        };
                    })
                };
            },
            cache: true
        },
    });

    // Mostrar u ocultar el campo de selección de grupo basado en el estado del checkbox
    $('#notifications-active').on('change', function () {
        if ($(this).is(':checked')) {
            $('#notification-group-selection').show();
        } else {
            $('#notification-group-selection').hide();
            // Limpiar la selección si se desactiva
            $('#Notification-Groups').val(null).trigger('change');
        }
    }).trigger('change'); // Trigger para inicializar el estado correcto
}
// Función para inicializar correos al cargar la página (modo edición)
function initializeNotificationEmailsTo() {


    const emailString = $('#toEmailsHidden').val(); // Obtiene el valor del modelo
    if (emailString) {
        const emails = emailString.split(';'); // Divide los correos por ';'
        emails.forEach(email => {
            if (email.trim() !== '') {
                $('#email-list').append(`
                    <span class="email-tag">
                        ${email} <span class="remove-email" data-email="${email}">x</span>
                    `);
            }
        });
    }

    // Evento para agregar correos
    $('#email-input').keypress(function (e) {
        if (e.which === 13) {
            e.preventDefault();
            const email = $(this).val().trim();
            if (validateEmail(email)) {
                $(this).removeClass('is-invalid');
                $('#email-list').append(`
                <span class="email-tag">
                    ${email} <span class="remove-email" data-email="${email}">x</span>
                </span>
            `);
                syncEmails();
                $(this).val('');
            } else {
                $(this).addClass('is-invalid');
            }
        }
    });
    // Evento para eliminar correos
    $('#email-list').on('click', '.remove-email', function () {
        $(this).parent().remove();
        syncEmails();
    });

    // Sincroniza los correos al enviar el formulario
    $('form').on('submit', function () {
        syncEmails();
    });


}
/**
 * Funcion permite sincronizar los correos
 */
function syncEmails() {
    const emails = [];
    $('#email-list .email-tag').each(function () {
        emails.push($(this).text().trim().slice(0, -1)); // Remueve la "x" de cada correo
    });
    $('#toEmailsHidden').val(emails.join(';')); // Sincroniza el campo oculto con correos separados por ";"
}
/**
 * Funcion permite validar el formulario de grupo
 */
function toggleActionSections() {
    if ($('#isFinalStateCheckbox').is(':checked')) {
        $('#actionSections').hide();
    } else {
        $('#actionSections').show();
    }
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


/**
 * Funcion permite iniccializar la tabla de acciones
 */
function loadEmailTemplates() {
    $.ajax({
        url: '/admin/MailTemplate/GetAll',
        method: 'GET',
        success: function (templates) {
            // Agregar opciones dinámicamente
            templates.data.forEach(template => {
                $('#email-template').append(
                    `<option value="${template.id}">${template.name}</option>`
                );
            });

            // Seleccionar la opción correspondiente en modo edición
            const selectedTemplateId = $('#email-template').data('selected-value');
            if (selectedTemplateId) {
                $('#email-template').val(selectedTemplateId);
            }
        },
        error: function () {
            alert('Error al cargar plantillas');
        }
    });
}
/**
 * Funcion permite inicializar las validaciones de email
 * @param {any} email
 * @returns
 */
function validateEmail(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}



