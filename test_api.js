const axios = require('axios');

async function test() {
    try {
        const res = await axios.post('http://localhost:5148/api/Rutinas', {
            nombre: "Test Routine Script",
            nivel: 1,
            ejercicios: [
                {
                    ejercicioId: 2,
                    series: 4,
                    descansoSegundos: 60,
                    tipo: 0,
                    repeticiones: "10-12"
                }
            ]
        }, {
            headers: {
                // Need to simulate an auth token or bypass auth for local test. 
                // Actually the backend has [Authorize] so this will fail with 401 without a token.
            }
        });
        console.log(res.data);
    } catch (e) {
        console.error(e.message);
    }
}
test();
