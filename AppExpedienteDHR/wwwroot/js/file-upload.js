document.addEventListener("DOMContentLoaded", function () {
    var fileInput = document.querySelector('.file-input');
    var dropArea = document.querySelector('.file-drop-area');
    var fileList = document.getElementById('fileList');
    var fileMsg = document.querySelector('.file-msg');

    // Detecta si se permite múltiples archivos desde la vista (se puede hacer desde el atributo data o directamente desde el input)
    var allowMultiple = fileInput.hasAttribute('multiple');
    console.log(`Permitir múltiples archivos: ${allowMultiple}`);

    var dt = new DataTransfer(); // Utilizamos DataTransfer para manipular los archivos

    // Añadir eventos de arrastrar y soltar
    dropArea.addEventListener('dragenter', function () {
        dropArea.classList.add('is-active');
    });

    dropArea.addEventListener('dragleave', function () {
        dropArea.classList.remove('is-active');
    });

    dropArea.addEventListener('dragover', function (event) {
        event.preventDefault();
    });

    dropArea.addEventListener('drop', function (event) {
        event.preventDefault();
        dropArea.classList.remove('is-active');

        let files = event.dataTransfer.files;

        if (!allowMultiple && files.length > 1) {
            // Si no se permiten múltiples archivos y se intentan arrastrar más de uno, solo se selecciona el primero
            files = [files[0]];
        }

        // Añadir los archivos al DataTransfer
        for (let i = 0; i < files.length; i++) {
            dt.items.add(files[i]);
        }

        fileInput.files = dt.files; // Actualizar el input con los archivos manipulados
        updateFileList(dt.files);
    });

    // Mostrar archivos seleccionados al hacer clic en el input
    fileInput.addEventListener('change', function () {
        let selectedFiles = fileInput.files;

        if (!allowMultiple && selectedFiles.length > 1) {
            // Limita a solo un archivo si no se permite múltiples
            dt.clear(); // Limpiar el DataTransfer
            dt.items.add(selectedFiles[0]); // Añadir el primer archivo
            fileInput.files = dt.files; // Actualizar el input
        } else {
            // Agregar todos los archivos seleccionados al DataTransfer
            for (let i = 0; i < selectedFiles.length; i++) {
                dt.items.add(selectedFiles[i]);
            }
            fileInput.files = dt.files;
        }

        updateFileList(fileInput.files);
    });

    function updateFileList(files) {
        fileList.innerHTML = '';
        if (files.length === 0) {
            fileMsg.textContent = 'Ningún archivo seleccionado';
        } else {
            fileMsg.textContent = files.length + ' archivo(s) seleccionado(s)';
        }

        Array.from(files).forEach(function (file, index) {
            var listItem = document.createElement('li');
            listItem.classList.add('border', 'p-2', 'mb-1');
            listItem.innerHTML = '<span>' + file.name + ' (' + formatFileSize(file.size) + ')</span><button type="button" data-index="' + index + '"><i class="fas fa-trash"></i> Eliminar</button>';
            fileList.appendChild(listItem);
        });

        // Añadir eventos de eliminar archivo
        var deleteButtons = fileList.querySelectorAll('button');
        deleteButtons.forEach(function (button) {
            button.addEventListener('click', function () {
                removeFile(parseInt(button.getAttribute('data-index')));
            });
        });
    }

    function removeFile(index) {
        dt.items.remove(index); // Remover archivo de DataTransfer
        fileInput.files = dt.files; // Actualizar el input de archivos
        updateFileList(fileInput.files);
    }

    function formatFileSize(bytes) {
        if (bytes >= 1000000) {
            return (bytes / 1000000).toFixed(2) + ' MB';
        }
        return (bytes / 1000).toFixed(2) + ' KB';
    }

    // Limpiar el DataTransfer al cerrar el modal
    // Verificar si el modal existe antes de agregar el evento
    var modal = document.getElementById('modalAgregarAnexo');
    if (modal) {
        $('#modalAgregarAnexo').on('hidden.bs.modal', function () {
            dt = new DataTransfer(); // Crear un nuevo DataTransfer vacío
            fileInput.files = dt.files; // Asignar al input de archivo
            fileList.innerHTML = ''; // Limpiar la lista de archivos
            fileMsg.textContent = 'Ningún archivo seleccionado'; // Actualizar el mensaje
        });
    }





});