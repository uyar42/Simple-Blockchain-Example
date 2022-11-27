using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
namespace BlockchainCoding
{
    class P2PServer : WebSocketBehavior
    {
        bool chainSynched = false;
        WebSocketServer wss = null;
        public void Start()
        {
            wss = new WebSocketServer($"ws://127.0.0.1:{Program.Port}");
            wss.AddWebSocketService<P2PServer>("/Blockhain");
            wss.Start();
            Console.WriteLine($"Server şu adreste başlatıldı ws://127.0.0.1:{Program.Port}");

        }
        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Merhaba Server")
            {
                Console.WriteLine(e.Data);
                Send("Merhaba Client");
            }
            else
            {
                Blockchain newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);
                if (newChain.IsValid() && newChain.Chain.Count > Program.ourblockchain.Chain.Count)
                {
                    List<Transaction> newTransaction = new List<Transaction>();
                    newTransaction.AddRange(newChain.PendingTransaction);
                    newTransaction.AddRange(Program.ourblockchain.PendingTransaction);
                    newChain.PendingTransaction = newTransaction;
                    Program.ourblockchain = newChain;
                }
                
            }
            if (!chainSynched)
            {
                Send(JsonConvert.SerializeObject(Program.ourblockchain));
                chainSynched = true;
            }

        }

    }
}
