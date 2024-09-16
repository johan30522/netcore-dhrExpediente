document.addEventListener("DOMContentLoaded", function () {
    var fileInput = document.querySelector('.file-input');
    var dropArea = document.querySelector('.file-drop-area');
    var fileList = document.getElementById('fileList');
    var fileMsg = document.querySelector('.file-msg');

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
        fileInput.files = event.dataTransfer.files;
        updateFileList(fileInput.files);
    });

    // Mostrar archivos seleccionados al hacer clic en el input
    fileInput.addEventListener('change', function () {
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
        var dt = new DataTransfer();
        var files = fileInput.files;

        Array.from(files).forEach(function (file, i) {
            if (i !== index) {
                dt.items.add(file); // Añadir archivos que no queremos eliminar
            }
        });

        fileInput.files = dt.files; // Actualizar el input de archivos
        updateFileList(fileInput.files);
    }

    function formatFileSize(bytes) {
        if (bytes >= 1000000) {
            return (bytes / 1000000).toFixed(2) + ' MB';
        }
        return (bytes / 1000).toFixed(2) + ' KB';
    }
});