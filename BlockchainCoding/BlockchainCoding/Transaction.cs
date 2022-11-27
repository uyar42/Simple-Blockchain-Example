using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainCoding
{
    public class Transaction
    {
        //gonderen kısnın adresı
        public string FromAddress { get; set; }
       //gönderilecek kişi
        public string ToAddress { get; set; }
        //miktar
        public int Amount { get; set; }
        public Transaction(string fromAddress,string toAddress,int amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
        }

    }
}
