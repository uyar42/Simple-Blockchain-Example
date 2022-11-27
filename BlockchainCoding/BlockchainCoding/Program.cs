using Newtonsoft.Json;
using System;

namespace BlockchainCoding
{
    class Program
    {
        private const int V = 2;
        public static Blockchain ourblockchain = new Blockchain();
        public static int Port = 0;
        public static P2PClient Client = new P2PClient();
        public static P2PServer Server = null;
        public static string name = "Bilinmiyor";
        static void Main(string[] args)
        {
            
            DateTime startTime = DateTime.Now;
            ourblockchain.InitializeChain();
            if (args.Length>=1)
            {
                Port = int.Parse(args[0]);
            }
            if (args.Length >= 2)
            {
                name =(args[1]);
            }
            if(Port>0)
            {
                Server = new P2PServer();
                Server.Start();
            }
            if(name!="Bilinmiyor")
                Console.WriteLine($"Şu an ki kullanıcı:{name}");

            Console.WriteLine("====================");
            Console.WriteLine("1. Server a Bağlan");
            Console.WriteLine("2. Transaction Ekle");
            Console.WriteLine("3. Blockchain i Goster");
            Console.WriteLine("4. Cıkıs");
            Console.WriteLine("====================");

            int selection = 0;
            while (selection!=4)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("Lütfen server URL sini giriniz");
                        string serverURL = Console.ReadLine();
                        Client.Connect($"{Server}/Blockchain");
                        break;
                    case 2:
                        Console.WriteLine("Lütfen alıcı adını giriniz");
                        string receiverName = Console.ReadLine();
                        Console.WriteLine("Miktari giriniz");
                        string amount = Console.ReadLine();
                        ourblockchain.CreateTransaction(new Transaction(name, receiverName, int.Parse(amount)));
                        ourblockchain.ProcessPendingTransaction(name);
                        Client.Broadcast(JsonConvert.SerializeObject(ourblockchain));
                        break;
                    case 3:
                        Console.WriteLine("Blockhain");
                        Console.WriteLine(JsonConvert.SerializeObject(ourblockchain,Formatting.Indented));
                        break;


                  
                      

                }
                Console.WriteLine("lütfen bir seçenek seçin");
                string action = Console.ReadLine();
                selection = int.Parse(action);
            }
            Client.Close();



            ////ourblockchain.AddBlock(new Block(DateTime.Now, null, "{sender:Mehmet Ali , receiver:Ahmet,amount:5"));
            ////ourblockchain.AddBlock(new Block(DateTime.Now, null, "{sender:Berk , receiver:Çağdaş,amount:10"));
            ////ourblockchain.AddBlock(new Block(DateTime.Now, null, "{sender:Ömer , receiver:Eren,amount:8"));
            //ourblockchain.CreateTransaction(new Transaction("Selcuk", "Ahmet",15));
            //ourblockchain.ProcessPendingTransaction("Ali");
            //ourblockchain.CreateTransaction(new Transaction("Ahmet", "Selcuk", 10));
            //ourblockchain.CreateTransaction(new Transaction("Ahmet", "Selcuk", 2 ));
            //ourblockchain.ProcessPendingTransaction("Ali");
            //DateTime finishTime = DateTime.Now;
            //Console.WriteLine("Süre : " + (finishTime-startTime).ToString());
            //Console.WriteLine("Selçuk balance:" + ourblockchain.GetBalance("Selcuk").ToString());
            //Console.WriteLine("Ahmet balance:" + ourblockchain.GetBalance("Ahmet").ToString());
            //Console.WriteLine("Ali balance:" + ourblockchain.GetBalance("Ali").ToString());



            //Console.WriteLine(JsonConvert.SerializeObject(ourblockchain, Formatting.Indented));
            ////Console.WriteLine("Geçerli mi ?" + ourblockchain.IsValid().ToString());

            ////Console.WriteLine("Veri değiştiriliyor....");
            //////data değişme
            ////ourblockchain.Chain[1].Data = "{sender:ABC , receiver:Eren,amount:32";
            ////Console.WriteLine("Değiştirildi ve Geçerli mi?" + ourblockchain.IsValid().ToString());

            //////hash değişme
            ////Console.WriteLine("Hash güncelleniyor");
            ////ourblockchain.Chain[1].Hash = ourblockchain.Chain[1].CalculateHash();
            ////Console.WriteLine("Hash Değiştirildi ve Geçerli mi?" + ourblockchain.IsValid().ToString());
            ////2
            ////ourblockchain.Chain[2].PreviousHash = ourblockchain.Chain[1].Hash;
            ////ourblockchain.Chain[2].Hash = ourblockchain.Chain[2].CalculateHash();
            //    //3
            ////ourblockchain.Chain[3].PreviousHash = ourblockchain.Chain[2].Hash;
            ////ourblockchain.Chain[3].Hash = ourblockchain.Chain[3].CalculateHash();
            ////Console.WriteLine("Tüm zincir hash Değiştirildi ve Geçerli mi?" + ourblockchain.IsValid().ToString());
            Console.ReadKey();
        }
    }
}
