function setPreviousView(view) {
    console.log(`setPreviousView22: ${view}`);
    const currentView = window.location.href;
    sessionStorage.setItem('previousView', currentView);
    // Usar un fetch para establecer la vista anterior en el servidor
    // Usar fetch para enviar solo el valor de la cadena 'view'
    fetch('/General/Utils/SetPreviousView', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(view) // Aquí se envía solo la cadena, no un objeto
    }).then(response => {
        console.log('Vista anterior establecida en el servidor.');
    });
}