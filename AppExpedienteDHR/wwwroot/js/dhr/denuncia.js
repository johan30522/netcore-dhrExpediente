$(document).ready(function () {


    // Manejar cambio de provincia y cargar cantones
    $('#provinciaSelect').change(function () {
        var provinciaId = $(this).val();
        $.get('/Denuncia/Solicitud/GetCantones', { provinciaId: provinciaId }, function (cantones) {
            var cantonSelect = $('#cantonSelect');
            cantonSelect.empty();
            cantonSelect.append('<option value="">Seleccione un cantón</option>');
            $.each(cantones, function (i, canton) {
                cantonSelect.append('<option value="' + canton.codigoCanton + '">' + canton.nombre + '</option>');
            });
            // Limpiar distritos
            $('#distritoSelect').empty().append('<option value="">Seleccione un distrito</option>');
        });
    });

    // Manejar cambio de cantón y cargar distritos
    $('#cantonSelect').change(function () {
        var cantonId = $(this).val();
        $.get('/Denuncia/Solicitud/GetDistritos', { cantonId: cantonId }, function (distritos) {
            var distritoSelect = $('#distritoSelect');
            distritoSelect.empty();
            distritoSelect.append('<option value="">Seleccione un distrito</option>');
            $.each(distritos, function (i, distrito) {
                distritoSelect.append('<option value="' + distrito.codigoDistrito + '">' + distrito.nombre + '</option>');
            });
        });
    });



    // Mostrar u ocultar el botón de consulta dependiendo del tipo de identificación seleccionado
    $('#tipoIdentificacionSelect').change(function () {
        var tipoIdentificacion = $(this).val();
        if (tipoIdentificacion == 1) {  // Si el tipo de identificación es 1, mostrar el botón
            $('#consultaCiudadanoBtn').show();
        } else {
            $('#consultaCiudadanoBtn').hide();
        }
    });

    // Al hacer clic en el botón de consulta
    $('#consultaCiudadanoBtn').on('click', function () {
        var cedula = $('#numeroIdentificacion').val();

        // Verificar si el número de identificación está vacío
        if (cedula === '') {
            alert('Por favor, ingrese un número de identificación.');
            return;
        }

        // Llamada al API para obtener el ciudadano
        $.get('/Denuncia/Solicitud/GetCiudadano', { cedula: cedula }, function (data) {
            // Si se obtiene la información del ciudadano, llenar los campos
            $('#nombreDenunciante').val(data.nombre);
            $('#primerApellidoDenunciante').val(data.apellido1);
            $('#segundoApellidoDenunciante').val(data.apellido2);
            $('#errorCiudadano').hide();  // Ocultar el mensaje de error si se encuentra el ciudadano
        }).fail(function () {
            // Mostrar mensaje de error si no se encuentra el ciudadano
            $('#errorCiudadano').show();
        });
    });

    // Capturar el evento de presionar "Enter" en el campo de Número de Identificación
    $('#numeroIdentificacion').on('keypress', function (e) {
        if (e.which === 13) { // Código de la tecla "Enter"
            if ($('#consultaCiudadanoBtn').is(':visible')) {
                // Simula un clic en el botón de consulta si está visible
                $('#consultaCiudadanoBtn').click();
                e.preventDefault(); // Evita que el formulario se envíe al presionar Enter
            }
        }
    });



    // Función para limpiar y deshabilitar los campos
    function limpiarCamposPersonaAfectada() {
        $('.persona-afectada-field').each(function () {
            $(this).val('');  // Limpiar campos
            $(this).prop('disabled', true);  // Deshabilitar campos
        });
    }




    // Activar/desactivar los campos de Persona Afectada según el checkbox
    $('#incluyePersonaAfectada').change(function () {
        var isChecked = $(this).is(':checked');
        $('.persona-afectada-field').prop('disabled', !isChecked);

        if (!isChecked) {
            // Limpiar y deshabilitar los campos si se desmarca
            limpiarCamposPersonaAfectada();
        }
    });


    //// Validar el campo de Aceptar Términos y SweetAlert para confirmar el envío del formulario
    //$('#formDenuncia').on('submit', function (e) {
    //    e.preventDefault(); // Detener el envío del formulario

    //    // Validar el checkbox de Acepta los Términos
    //    var aceptaTerminos = $('#aceptaTerminosCheckbox').is(':checked');
    //    if (!aceptaTerminos) {
    //        $('#aceptaTerminosCheckbox').addClass('is-invalid');
    //        return;
    //    } else {
    //        $('#aceptaTerminosCheckbox').removeClass('is-invalid').addClass('is-valid');
    //    }

    //    // Mostrar el SweetAlert de confirmación
    //    Swal.fire({
    //        title: '¿Está seguro de que desea guardar?',
    //        text: "No podrá deshacer esta acción.",
    //        icon: 'warning',
    //        showCancelButton: true,
    //        confirmButtonText: 'Sí, guardar',
    //        cancelButtonText: 'No, cancelar'
    //    }).then((result) => {
    //        if (result.isConfirmed) {
    //            // Enviar el formulario si el usuario confirma
    //            $('form').off('submit').submit();
    //        }
    //    });
    //});

});

// Función para limpiar y deshabilitar los campos
function limpiarCamposPersonaAfectada() {
    $('.persona-afectada-field').each(function () {
        $(this).val('');  // Limpiar campos
        $(this).prop('disabled', true);  // Deshabilitar campos
    });
}

// Cargar cantones y distritos preseleccionados en caso de edición
function cargarCantonesYDistritos(provinciaSeleccionada, cantonSeleccionado, distritoSeleccionado) {
    if (provinciaSeleccionada) {
        // Cargar los cantones según la provincia seleccionada
        $.get('/Denuncia/Solicitud/GetCantones', { provinciaId: provinciaSeleccionada }, function (cantones) {
            var cantonSelect = $('#cantonSelect');
            cantonSelect.empty();
            cantonSelect.append('<option value="">Seleccione un cantón</option>');
            $.each(cantones, function (i, canton) {
                cantonSelect.append('<option value="' + canton.codigoCanton + '"' +
                    (canton.codigoCanton == cantonSeleccionado ? ' selected' : '') + '>' + canton.nombre + '</option>');
            });

            // Si hay un cantón seleccionado, cargar los distritos correspondientes
            if (cantonSeleccionado) {
                $.get('/Denuncia/Solicitud/GetDistritos', { cantonId: cantonSeleccionado }, function (distritos) {
                    var distritoSelect = $('#distritoSelect');
                    distritoSelect.empty();
                    distritoSelect.append('<option value="">Seleccione un distrito</option>');
                    $.each(distritos, function (i, distrito) {
                        distritoSelect.append('<option value="' + distrito.codigoDistrito + '"' +
                            (distrito.codigoDistrito == distritoSeleccionado ? ' selected' : '') + '>' + distrito.nombre + '</option>');
                    });
                });
            }
        });
    }
}

// Permite confirmar la generación de un nuevo expediente
// Permite confirmar la generación de un nuevo expediente
function confirmGenerateExpediente(expedienteId) { // Asegúrate de pasar el ID del expediente aquí
    console.log(` Generar expediente ${expedienteId}`);
    Swal.fire({
        title: '¿Estás seguro?',
        text: "Esta acción generará un nuevo expediente.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, generar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Denuncia/Solicitud/GenerateExpediente',
                type: 'POST',
                data: { id: expedienteId },  // Enviamos el ID como parámetro
                success: function (response) {
                    Swal.fire({
                        title: '¡Generado!',
                        text: 'El expediente ha sido generado exitosamente.',
                        icon: 'success',
                        confirmButtonText: 'OK'
                    }).then(() => {
                        // Redirige a la nueva página
                        window.location.href = `/Denuncia/Solicitud/Info/${expedienteId}`;
                    });
                },
                error: function (xhr, status, error) {
                    Swal.fire(
                        'Error',
                        'Hubo un problema al generar el expediente.',
                        'error'
                    );
                }
            });
        }
    });
}
    