using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace NeosVrCache
{
    public class NeosVrCacheService : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"Incomming Message: {e.Data}");
            base.OnMessage(e);
        }
    }
}