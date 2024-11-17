// Función para inicializar Quill en un campo específico
function initializeQuillEditor(selector, hiddenInputId, isReadOnly, initialContent) {
    var editor = new Quill(selector, {
        theme: 'snow',
        readOnly: isReadOnly,
        modules: {
            toolbar: [
                ['bold', 'italic', 'underline'],
                [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                ['link']
            ]
        }
    });

    // Cargar el contenido inicial, si existe
    editor.root.innerHTML = initialContent || '';
    // Sincronizar el contenido de Quill con el campo oculto en cada cambio
    editor.on('text-change', function () {
        document.querySelector(hiddenInputId).value = editor.root.innerHTML;
    });

    // Sincronizar el contenido de Quill con el campo oculto al enviar el formulario
    document.querySelector('form').onsubmit = function () {
        document.querySelector(hiddenInputId).value = editor.root.innerHTML;
    };

    return editor;
}