$(document).ready(function () {
    // Cargar todos los datos al inicializar
    loadAllData();

    // Inicializar tooltips
    $('[data-bs-toggle="tooltip"]').tooltip();

    // Inicializar dropdowns
    $('.dropdown-toggle').dropdown();
});

function loadAllData() {
    $.ajax({
        url: "http://localhost:5225/Admin/Tipologia/GetAllDerechos",
        type: "GET",
        success: function (response) {
            const allData = parseData(response.data.$values);
            initializeTabulator(allData);
        }
    });
}

// Configurar Tabulator con datos jerárquicos y acciones personalizadas
function initializeTabulator(data) {
    const table = new Tabulator("#table-container", {
        data: data, // Datos procesados para Tabulator
        layout: "fitDataFill",
        movableColumns: true,
        pagination: "local", // Paginar en el cliente
        paginationSize: 20,
        paginationSizeSelector: [5, 10, 20],
        placeholder: "No se encontraron datos",
        dataTree: true, // Activar estructura jerárquica
        dataTreeStartExpanded: false, // Expandir nodos de primer nivel
        columns: [
            { title: "Código", field: "Codigo", width: "20%" },
            { title: "Descripción", field: "Descripcion", width: "50%" },
            {
                title: "Acciones", field: "Acciones", width: "30%", formatter: function (cell, formatterParams) {
                    const data = cell.getRow().getData();
                    let buttons = `
                <div class="btn-group">
                   
            `;

                    if (data.Nivel === "Derecho") {
                        buttons += `
                        <button class='btn btn-sm btn-outline-secondary' onclick='openModalDerecho(${JSON.stringify(data)})' data-bs-toggle="tooltip" title="Editar Derecho">
                        <i class='fas fa-edit'></i>
                    </button>
                    <button class='btn btn-sm btn-outline-danger' onclick='confirmDelete(${data.Id}, "${data.Nivel}")' data-bs-toggle="tooltip" title="Eliminar">
                        <i class='fas fa-trash-alt'></i>
                    </button>
                    <button class='btn btn-sm btn-outline-success' onclick='openModalEvento(${data.Id})' data-bs-toggle="tooltip" title="Crear Evento">
                        <i class='fas fa-plus-circle'></i> Crear Evento
                    </button>
                `;
                    } else if (data.Nivel === "Evento") {
                        buttons += `
                            <button class='btn btn-sm btn-outline-secondary' onclick='openModalDerecho(${JSON.stringify(data)})' data-bs-toggle="tooltip" title="Editar Evento">
                                <i class='fas fa-edit'></i>
                            </button>
                            <button class='btn btn-sm btn-outline-danger' onclick='confirmDelete(${data.Id}, "${data.Nivel}")' data-bs-toggle="tooltip" title="Eliminar Evento">
                                <i class='fas fa-trash-alt'></i>
                            </button>
                            <div class="dropdown d-inline">
                                <button class="btn btn-sm btn-outline-info dropdown-toggle" type="button" id="dropdownMenuButton-${data.Id}" data-bs-toggle="dropdown" aria-expanded="false">
                                    Opciones
                                </button>
                                <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton-${data.Id}" style="z-index: 1050;">
                                    <li>
                                        <a class="dropdown-item" href="#" onclick="openModalEspecificidad(${data.Id})">
                                            <i class='fas fa-plus-circle'></i> Crear Especificidad
                                        </a>
                                    </li>
                                    <li>
                                        <a class="dropdown-item" href="#" onclick="openModalDescriptor(${data.Id})">
                                            <i class='fas fa-cog'></i> Administrar Descriptores
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        `;
                    
                } else if(data.Nivel === "Especificidad") {
                    buttons += `
                        <button class='btn btn-sm btn-outline-secondary' onclick='openModalDerecho(${JSON.stringify(data)})' data-bs-toggle="tooltip" title="Editar Especificidad">
                        <i class='fas fa-edit'></i>
                    </button>
                    <button class='btn btn-sm btn-outline-danger' onclick='confirmDelete(${data.Id}, "${data.Nivel}")' data-bs-toggle="tooltip" title="Eliminar Especificidad">
                        <i class='fas fa-trash-alt'></i>
                    </button>
                `;
}

                    buttons += `</div>`;


                    return buttons;
                }
            }
        ]
    });

    // Búsqueda
    $("#searchInput").on("input", function () {
        const searchText = $(this).val().toLowerCase();
        table.setFilter("Descripcion", "like", searchText);
    });
    // Activar los tooltips y dropdowns después de la renderización
    table.on("renderComplete", function () {
        $('[data-bs-toggle="tooltip"]').tooltip();
        $('.dropdown-toggle').dropdown();
    });
}

// Convertir datos a formato para Tabulator
function parseData(derechos) {
    return derechos.map(derecho => {
        return {
            Id: derecho.Id,
            Codigo: derecho.Codigo,
            Descripcion: derecho.Descripcion,
            Nivel: "Derecho",
            _children: derecho.Eventos.$values.map(evento => ({
                Id: evento.Id,
                Codigo: evento.Codigo,
                Descripcion: evento.Descripcion,
                Nivel: "Evento",
                _children: evento.Especificidades.$values.map(especificidad => ({
                    Id: especificidad.Id,
                    Codigo: especificidad.Codigo,
                    Descripcion: especificidad.Descripcion,
                    Nivel: "Especificidad"
                }))
            }))
        };
    });
}

// Función para crear un evento
function createEvent(derechoId) {
    window.location.href = `/Admin/Tipologia/CreateEvent?derechoId=${derechoId}`;
}

// Función para crear una especificidad
function createEspecificidad(eventoId) {
    window.location.href = `/Admin/Tipologia/CreateEspecificidad?eventoId=${eventoId}`;
}

// Función de eliminación con confirmación de SweetAlert
function confirmDelete(id, nivel) {
    Swal.fire({
        title: `¿Está seguro de eliminar este ${nivel}?`,
        text: "Esta acción no se puede deshacer.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            deleteItem(id, nivel);
        }
    });
}

// Función para eliminar un elemento
function deleteItem(id, nivel) {
    $.ajax({
        url: `/Admin/Tipologia/Delete${nivel}?id=${id}`,
        type: "POST",
        success: function () {
            Swal.fire("Eliminado", `${nivel} eliminado con éxito.`, "success");
            loadAllData(); // Recargar datos después de eliminar
        },
        error: function () {
            Swal.fire("Error", `Hubo un problema al eliminar el ${nivel}.`, "error");
        }
    });
}

// Limpiar búsqueda
function clearSearch() {
    $("#searchInput").val("");
    table.clearFilter();
}


function closeModal(modalId) {
    const modalElement = document.getElementById(modalId);
    const modal = bootstrap.Modal.getInstance(modalElement);
    if (modal) {
        modal.hide();
    }
}


function openModalDerecho(derecho = null) {
    if (derecho) {
        $("#Codigo").val(derecho.Codigo);
        $("#Descripcion").val(derecho.Descripcion);
        $("#derechoId").val(derecho.Id);
    } else {
        $("#Codigo").val('');
        $("#Descripcion").val('');
        $("#derechoId").val('');
    }
    const modalElement = document.getElementById('modalDerecho');
    const modal = new bootstrap.Modal(modalElement, {
        backdrop: 'static',
        keyboard: false
    });
    modal.show();
}

function saveDerecho() {
    const id = $("#derechoId").val();
    const codigo = $("#Codigo").val();
    const descripcion = $("#Descripcion").val();
    const data = { Id: id, Codigo: codigo, Descripcion: descripcion };
    const url = id ? `/Admin/Tipologia/EditDerecho` : `/Admin/Tipologia/CreateDerecho`;

    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function () {
            const modal = bootstrap.Modal.getInstance(document.getElementById('modalDerecho'));
            modal.hide();
            loadAllData(); // Recargar datos en la tabla
        },
        error: function () {
            Swal.fire("Error", "Hubo un problema al guardar el derecho.", "error");
        }
    });
}
function openModalEvento(evento = null) {
    if (evento) {
        $("#Codigo").val(evento.Codigo);
        $("#Descripcion").val(evento.Descripcion);
        $("#derechoId").val(evento.Id);
    } else {
        $("#Codigo").val('');
        $("#Descripcion").val('');
        $("#derechoId").val('');
    }
    const modalElement = document.getElementById('modalEvento');
    const modal = new bootstrap.Modal(modalElement, {
        backdrop: 'static',
        keyboard: false
    });

    modal.show();
}

function saveEvento() {
    const id = $("#eventoId").val();
    const codigo = $("#Codigo").val();
    const descripcion = $("#Descripcion").val();
    const data = { Id: id, Codigo: codigo, Descripcion: descripcion };
    const url = id ? `/Admin/Tipologia/EditEvento` : `/Admin/Tipologia/CreateEvento`;

    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function () {
            const modal = bootstrap.Modal.getInstance(document.getElementById('modalEvento'));
            modal.hide();
            loadAllData(); // Recargar datos en la tabla
        },
        error: function () {
            Swal.fire("Error", "Hubo un problema al guardar el evento.", "error");
        }
    });
}

function openModalEspecificidad(especificidad = null) {
    if (especificidad) {
        $("#Codigo").val(especificidad.Codigo);
        $("#Descripcion").val(especificidad.Descripcion);
        $("#derechoId").val(especificidad.Id);
    } else {
        $("#Codigo").val('');
        $("#Descripcion").val('');
        $("#derechoId").val('');
    }

    const modalElement = document.getElementById('modalEspecificidad');
    const modal = new bootstrap.Modal(modalElement, {
        backdrop: 'static',
        keyboard: false
    });

    modal.show();
}

function saveEspecificidad() {
    const id = $("#eventoId").val();
    const codigo = $("#Codigo").val();
    const descripcion = $("#Descripcion").val();
    const data = { Id: id, Codigo: codigo, Descripcion: descripcion };
    const url = id ? `/Admin/Tipologia/EditEspecificidad` : `/Admin/Tipologia/CreateEspecificidad`;

    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function () {
            const modal = bootstrap.Modal.getInstance(document.getElementById('modalEspecificidad'));
            modal.hide();
            loadAllData(); // Recargar datos en la tabla
        },
        error: function () {
            Swal.fire("Error", "Hubo un problema al guardar la especificidad.", "error");
        }
    });
}