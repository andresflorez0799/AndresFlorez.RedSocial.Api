namespace AndresFlorez.RedSocial.Api.Hubs
{
    public class UserChat
    {
        /// <summary>
        /// Identificador del Usuario dentro del Sistema
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Identificador del Uusario dentro del Hub del Chat
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// Correo o Nombre del usuario 
        /// </summary>
        public string UserName { get; set; }
    }
}
