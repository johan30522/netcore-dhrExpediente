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
                title: "Opciones",
                field: "Acciones",
                width: "30%",
                hozAlign: "center",
                formatter: function () {
                    return `
                    <button class="btn btn-link btn-sm p-0 text-dark" title="Opciones">
                                <i class="fas fa-cog fa-lg"></i>
                            </button>
                    `;
                },
                cellClick: function (e, cell) {
                    // Verificar si el clic fue en el botón "Opciones"
                    if (e.target.classList.contains("fa-cog") || e.target.classList.contains("btn")) {
                        // Simular el clic derecho en la fila de Tabulator
                        e.preventDefault();
                        const rowElement = cell.getRow().getElement();
                        const event = new MouseEvent('contextmenu', { bubbles: true, clientX: e.clientX, clientY: e.clientY });
                        rowElement.dispatchEvent(event);
                    }
                }
            }
        ],
        rowContextMenu: function (e, row) {
            // Usar getContextMenuItems para personalizar el menú según el nivel de la fila
            return getContextMenuItems(row.getData());
        }
    });

    // Búsqueda en la tabla
    $("#searchInput").on("input", function () {
        const searchText = $(this).val().toLowerCase();
        table.setFilter("Descripcion", "like", searchText);
    });
}

// Función para obtener elementos del menú contextual según el nivel
function getContextMenuItems(data) {
    const menuItems = [
       
    ];

    if (data.Nivel === "Derecho") {
        menuItems.push(
            {
                label: `<i class="fas fa-edit"></i> Editar Derecho`,
                action: () => openModalDerecho(data.Id),
                //action: () => openModal(data),
            },
            {
                label: `<i class="fas fa-trash-alt"></i> Eliminar Derecho`,
                action: () => confirmDelete(data.Id, data.Nivel),
            },
            {
            label: `<i class="fas fa-plus-circle"></i> Crear Evento`,
            action: () => openModalEvento(null,data.Id),
            //action: () => openModal(data),
        });
    } else if (data.Nivel === "Evento") {
        menuItems.push(
            {
                label: `<i class="fas fa-edit"></i> Editar Evento`,
                action: () => openModalEvento(data.Id,null),
                //action: () => openModal(data),
            },
            {
                label: `<i class="fas fa-trash-alt"></i> Eliminar Evento`,
                action: () => confirmDelete(data.Id, data.Nivel),
            },
            {
                label: `<i class="fas fa-plus-circle"></i> Crear Especificidad`,
                action: () => openModalEspecificidad(null,data.Id),
            },
            {
                label: `<i class="fas fa-cog"></i> Administrar Descriptores`,
                action: () => openModalDescriptor(data.Id),
                //action: () => openModal(data),
            }
        );
    } else if (data.Nivel === "Especificidad") {
        menuItems.push(
            {
                label: `<i class="fas fa-edit"></i> Editar Especificidad`,
                action: () => openModalEspecificidad(data.Id, null),
            },
            {
                label: `<i class="fas fa-trash-alt"></i> Eliminar Especificidad`,
                action: () => confirmDelete(data.Id, data.Nivel),
            }
        );
    }
    return menuItems;
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

function openModal(data) {
    console.log(data);
}


function openModalDerecho(id = null) {
    $.get(`/Admin/Tipologia/CreateOrEditDerecho?id=${id}`, function (data) {
        $('#modalContainer').html(data);
        // Inicializar el modal con las opciones necesarias para que no se cierre al hacer clic fuera
        var modalDerecho = new bootstrap.Modal(document.getElementById('modalDerecho'), {
            backdrop: 'static',   // Evita que se cierre al hacer clic fuera del modal
            keyboard: false       // Evita que se cierre al presionar la tecla Esc
        });

        modalDerecho.show();
    });
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

function openModalEvento(id = null, derechoId = null) {
    $.get(`/Admin/Tipologia/CreateOrEditEvento?id=${id}&derechoId=${derechoId}`, function (data) {
        $('#modalContainer').html(data);
        // Inicializar el modal con las opciones necesarias para que no se cierre al hacer clic fuera
        var modalEvento = new bootstrap.Modal(document.getElementById('modalEvento'), {
            backdrop: 'static',   // Evita que se cierre al hacer clic fuera del modal
            keyboard: false       // Evita que se cierre al presionar la tecla Esc
        });

        modalEvento.show();
    });
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

function openModalEspecificidad(id = null, eventoId = null) {
    $.get(`/Admin/Tipologia/CreateOrEditEspecificidad?id=${id}&eventoId=${eventoId}`, function (data) {
        $('#modalContainer').html(data);
        var modalEspecificidad = new bootstrap.Modal(document.getElementById('modalEspecificidad'), {
            backdrop: 'static',   // Evita que se cierre al hacer clic fuera del modal
            keyboard: false       // Evita que se cierre al presionar la tecla Esc
        });

        modalEspecificidad.show();
    });
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