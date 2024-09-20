
console.log("lockRecord.js loaded");
function initLockRecord(recordId) {

    setBeforeUnloadLockRecord(recordId);
    setKeepAliveLockRecord(recordId);

}

function setKeepAliveLockRecord(recordId) {
    // Enviar "heartbeat" cada 2 minutos para mantener el bloqueo activo
    setInterval(function () {
        fetch(`/Denuncia/Lock/KeepAlive?lockId=${recordId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => response.json())
            .then(data => {
                if (!data.success) {
                    alert("No se pudo mantener el bloqueo. Posiblemente la solicitud ha sido desbloqueada.");
                    // redireccionar a la página de inicio
                    //window.location.href = "/Home/Index";
                }
            })
            .catch((error) => {
                console.error('Error al actualizar el bloqueo:', error);
            });
    }, 120000);  // 120000 ms = 2 minutos

}

function setBeforeUnloadLockRecord(recordId) {
    // Desbloquear el registro al salir de la página
    window.addEventListener('beforeunload', function (e) {
        //0 significa que es una carga normal, 
        //1 significa que es una recarga, 
        //2 significa que es un back / forward navigation`
        //if (performance.navigation.type === 1) {
        //    console.log("beforeunload: No hacemos nada si se está recargando");
        //    return;
        //}
        fetch(`/Denuncia/Lock/Unlock?lockId=${recordId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => response.json())
            .then(data => {
                if (!data.success) {
                    alert("No se pudo desbloquear el registro.");
                }
            })
            .catch((error) => {
                console.error('Error al desbloquear el registro:', error);
            });
    });
}

function unlockRecord(recordId) {
    fetch(`/Denuncia/Lock/Unlock?lockId=${recordId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            if (!data.success) {
                alert("No se pudo desbloquear el registro.");
            }
        })
        .catch((error) => {
            console.error('Error al desbloquear el registro:', error);
        });
}

