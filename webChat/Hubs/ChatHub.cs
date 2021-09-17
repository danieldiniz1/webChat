using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webChat.Data;
using webChat.Models;

namespace webChat.Hubs
{
    public class ChatHub : Hub
    {
        Context _context;
        public ChatHub(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }

        public async Task sendMessage(Message m)
        {
            m.Datetime = DateTime.Now;
            _context.Message.Add(m);
            _context.SaveChanges();
            await Clients.All.SendAsync("ReceiveMessage", m);
        }

        public string GetConnectionId() => Context.ConnectionId;
    }
}
