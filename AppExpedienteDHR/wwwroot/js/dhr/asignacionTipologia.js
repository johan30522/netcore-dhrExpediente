
function openTipologiaModal() {
    $.get("/Admin/Tipologia/GetAllDerechos", function (response) {
        const data = parseTipologiaData(response.data); // Función que convierte datos al formato de Tabulator
        initializeTabulator(data); // Inicializar el árbol
        $('#modalTipologia').modal('show');
    });
}

function initializeTabulator(data) {
    const table = new Tabulator("#tipologia-tree-container", {
        data: data,
        layout: "fitDataFill",
        movableColumns: true,
        pagination: "local",
        paginationSize: 20,
        dataTree: true,
        dataTreeChildField: "_children",
        dataTreeStartExpanded: false,
        columns: [
            { title: "Código", field: "Codigo", width: "20%" },
            { title: "Descripción", field: "Descripcion", width: "50%" },
            {
                title: "Acciones",
                field: "Acciones",
                width: "30%",
                hozAlign: "center",
                formatter: function (cell, formatterParams, onRendered) {
                    const rowData = cell.getRow().getData();
                    // Mostrar botón solo si el nivel es "Especificidad"
                    if (rowData.Nivel === "Especificidad") {
                        return `<button type="button" class="btn btn-sm btn-primary"><i class="fas fa-hand-point-right"></i></i>Asignar</button>`;
                    }
                    return ''; // No mostrar botón para otros niveles
                },
                cellClick: function (e, cell) {
                    const rowData = cell.getRow().getData();
                    if (rowData.Nivel === "Especificidad") {
                        assignTipologia(rowData);
                        $('#modalTipologia').modal('hide'); // Cerrar el modal al asignar
                    }
                }
            }
        ]
    });
}

function parseTipologiaData(derechos) {
    return derechos.map(derecho => ({
        Id: derecho.id,
        Codigo: derecho.codigo,
        Descripcion: derecho.descripcion,
        Nivel: "Derecho",
        _children: derecho.eventos ? derecho.eventos.map(evento => ({
            Id: evento.id,
            Codigo: evento.codigo,
            Descripcion: evento.descripcion,
            Nivel: "Evento",
            DerechoId: derecho.id, // Añadir el DerechoId al nivel Evento
            DerechoDescripcion: derecho.descripcion, // Añadir la descripción del Derecho
            _children: evento.especificidades ? evento.especificidades.map(especificidad => ({
                Id: especificidad.id,
                Codigo: especificidad.codigo,
                Descripcion: especificidad.descripcion,
                Nivel: "Especificidad",
                DerechoId: derecho.id, // Añadir el DerechoId al nivel Especificidad
                DerechoDescripcion: derecho.descripcion, // Añadir la descripción del Derecho
                EventoId: evento.id, // Añadir el EventoId
                EventoDescripcion: evento.descripcion // Añadir la descripción del Evento
            })) : null
        })) : null
    }));
}

function assignTipologia(data) {
    console.log(data);
    // Asignar los valores a los campos ocultos
    $('#DerechoId').val(data.DerechoId);
    $('#EventoId').val(data.EventoId);
    $('#EspecificidadId').val(data.Id);

    // Asignar descripciones a los campos visibles
    $('#derecho-descripcion').val(data.DerechoDescripcion);
    $('#evento-descripcion').val(data.EventoDescripcion);
    $('#especificidad-descripcion').val(data.Descripcion);

    // asigna los descriptores
    loadDescriptors(data.EventoId);
}

// Función para cargar descriptores cuando se asigna un Evento
function loadDescriptors(eventoId) {
    return new Promise((resolve, reject) => {
        if (!eventoId) {
            resolve(); // Resuelve inmediatamente si no hay eventoId
            return;
        }
        $.get(`/Admin/Tipologia/GetDescriptorsByEventoId?eventoId=${eventoId}`, function (data) {
            const descriptorDropdown = $('#DescriptorId');
            descriptorDropdown.empty(); // Limpia el dropdown antes de agregar nuevos elementos

            // Agrega una opción predeterminada
            descriptorDropdown.append('<option value="">Seleccione un descriptor</option>');

            // Llena el dropdown con los descriptores obtenidos
            data.forEach(descriptor => {
                descriptorDropdown.append(`<option value="${descriptor.id}">${descriptor.nombre}</option>`);
            });
            descriptorDropdown.prop('disabled', false); // Habilita el dropdown

            resolve(); // Resuelve el Promise cuando los datos han sido cargados
        }).fail(() => reject()); // Rechaza en caso de error
    });
}
//Funcion para seleccionar un Item por el id
function selectDescriptors(descriptorId, isEdit) {

    const descriptorDropdown = $('#DescriptorId');
    // Selecciona el valor de DescriptorId si está presente
    if (descriptorId) {
        descriptorDropdown.val(descriptorId);
    }

    // Deshabilitar el dropdown si el formulario no está en edición
    if (!isEdit) {
        descriptorDropdown.prop('disabled', true);
    }

}