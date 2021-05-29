namespace AndresFlorez.RedSocial.Api.Models
{
    public class ApiResponse<T>
    {
        /// <summary>
        /// estado de la solicitud, true para exitoso
        /// </summary>
        public bool RequestState { get; set; }
        /// <summary>
        /// Codigo Http de la solicitud
        /// </summary>
        public int RequestCode { get; set; }
        /// <summary>
        /// Miembro generico con el contenido de la solicitud/respuesta
        /// </summary>
        public T RequestData { get; set; }
    }
}
