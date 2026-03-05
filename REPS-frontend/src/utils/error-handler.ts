/**
 * Transforma un error de Axios o un error genérico en un mensaje de usuario en español.
 */
export const getErrorMessage = (error: any): string => {
    // Si no es un error de Axios
    if (!error.isAxiosError) {
        return `Error interno (Frontend): ${error.message || 'Desconocido'}`;
    }

    // Error de red (sin respuesta del servidor)
    if (!error.response) {
        if (error.code === 'ECONNABORTED') {
            return 'La conexión ha tardado demasiado tiempo. Revisa tu conexión.';
        }
        return 'No se pudo conectar con el servidor. Revisate si el proxy o backend está corriendo.';
    }

    const status = error.response.status;
    const data = error.response.data;

    // Manejo según código de estado HTTP
    switch (status) {
        case 401:
            return 'Email o contraseña incorrectos. Inténtalo de nuevo.';

        case 403:
            return 'No tienes permiso para realizar esta acción comercial.';

        case 404:
            return 'Lo que buscas no existe o ha sido movido.';

        case 400:
            // Caso especial para registro: usuario ya existe
            if (typeof data === 'string' && (data.includes('already exists') || data.includes('registrado'))) {
                return 'Este correo electrónico ya está registrado.';
            }
            // Mensaje de error personalizado del back si existe
            if (data?.mensaje) return data.mensaje;
            if (data?.message) return data.message;
            if (typeof data === 'string') return data;

            return 'Los datos enviados no son válidos. Revisa los campos.';

        case 413:
            return 'El archivo es demasiado grande.';

        case 429:
            return 'Demasiadas peticiones. Por favor, espera un momento.';

        case 500:
            return 'Error interno del servidor. Nuestros ingenieros han sido notificados.';

        default:
            return 'Ocurrió un error inesperado al procesar tu solicitud.';
    }
};
