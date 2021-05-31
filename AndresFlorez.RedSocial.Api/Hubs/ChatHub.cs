using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using AndresFlorez.RedSocial.Logica.Contrato;
using AndresFlorez.RedSocial.Modelo.EF;

namespace AndresFlorez.RedSocial.Api.Hubs
{
    public class ChatHub : Hub
    {
        public static List<UserChat> SignalRUsers = new List<UserChat>();
        private readonly IComunicacionBl _logica;
        private readonly IUsuarioBl _usuario;

        public ChatHub(IComunicacionBl comunicacion, IUsuarioBl usuario) 
        {
            _logica = comunicacion;
            _usuario = usuario;
        }

        public async Task Connect(string email)
        {
            try
            {
                var id = Context.ConnectionId;
                if (SignalRUsers.Count(x => x.ConnectionId == id) == 0)
                {                    
                    var usuarioBd = _usuario.GetByEmail(email);

                    UserChat usuarioNuevo = new UserChat
                    {
                        UserId = usuarioBd.Id,
                        ConnectionId = id,
                        UserName = $"{usuarioBd.Nombre} {usuarioBd.Apellido}"
                    };
                    SignalRUsers.Add(usuarioNuevo);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Envia un mensaje a un usuario específico
        /// </summary>
        /// <param name="userTo">Identificador de usuario dentro del Sistema (Id)</param>
        /// <param name="message">Contenido del mensaje</param>
        /// <returns>Promesa de envio de mensaje a Usuario especifico con el contenido del mensaje e informacion del usuario que envia</returns>
        public async Task SendMessageToUser(int userTo, string message)
        {
            try
            {
                var clientFrom = GetCurrentContextUser();
                await Clients.Caller.SendAsync("ReceiveMessage", clientFrom.ConnectionId, userTo, message);
                var clientFilter = SignalRUsers.Where(x => x.UserId == userTo);

                if (clientFilter != null && clientFilter.Any())
                {
                    string clientId = clientFilter.FirstOrDefault().ConnectionId;
                    await Clients.Client(clientId).SendAsync("ReceiveMessage", userTo, clientId, message);
                }

                RsocialMensajerium msj = new();
                msj.Mensaje = message;
                msj.IdUsuarioOrigen = clientFilter.FirstOrDefault().UserId;
                msj.IdUsuarioDestino = userTo;
                msj.IdEstado = 1;
                _logica.GuardarMensaje(msj);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task GetMessagesMe(int idUsuario)
        {
            try
            {
                var currentUser = GetCurrentContextUser();
                if (currentUser != null)
                {
                    var datos = _logica.ConsultarMensajes(currentUser.UserId);
                    if (datos != null)
                    {
                        List<ChatHistorico> chatHistoryResponse = new List<ChatHistorico>();
                        datos.ToList().ForEach(x =>
                        {
                            chatHistoryResponse.Add(new ChatHistorico()
                            {
                                Fecha = x.Fecha,
                                IdUsuarioOrigen = Convert.ToInt32(x.IdUsuarioOrigen),
                                Mensaje = x.Mensaje,
                                IdUsuarioDestino = x.IdUsuarioDestino,
                            });
                        });
                        await Clients.Caller.SendAsync("ReceiveMessagesHistory", JsonSerializer.Serialize(chatHistoryResponse));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task OnConnectedAsync() => await base.OnConnectedAsync();

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var item = SignalRUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                SignalRUsers.Remove(item);
            }
            await base.OnDisconnectedAsync(exception);
        }

        private UserChat GetCurrentContextUser()
        {
            try
            {
                var usuarioActuales = SignalRUsers.Where(x => x.ConnectionId == Context.ConnectionId);
                if (usuarioActuales != null && usuarioActuales.Any())
                    return usuarioActuales.FirstOrDefault();
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
