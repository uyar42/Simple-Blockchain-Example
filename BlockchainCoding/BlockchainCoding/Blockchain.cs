using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainCoding
{
    class Blockchain
    {//ınterface listesi

        public IList<Transaction> PendingTransaction = new List<Transaction>();
        public IList<Block> Chain { get; set; }
        public int difficulty { get; set; } = 2;
        public int Reward { get; set; } = 1;
        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }
        public void InitializeChain()
        {
            Chain = new List<Block>();

        }
        //tüm block chainlerde genesis(doğum) bloğu olmalıdır 
        public Block CreateGenesisBlock()
        {
            Block block = new Block(DateTime.Now, null, PendingTransaction);
            block.Mine(difficulty);
            PendingTransaction = new List<Transaction>();

            return block;
        }//ekle
        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }
        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }
        public void AddBlock(Block block)
        {
            Block latestblock = GetLatestBlock();
            block.Index = latestblock.Index + 1;
            block.PreviousHash = latestblock.Hash;
            block.Hash = block.CalculateHash();
            block.Mine(this.difficulty);
            Chain.Add(block);

        }
        public void CreateTransaction(Transaction transaction)
        {
            PendingTransaction.Add(transaction);
        }
        public void ProcessPendingTransaction(string minerAddress)
        {
            CreateTransaction(new Transaction(null, minerAddress, Reward));
            Block block = new Block(DateTime.Now, GetLatestBlock().Hash, PendingTransaction);
            AddBlock(block);
            PendingTransaction = new List<Transaction>();
            

        }
        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];
                if (currentBlock.Hash!=currentBlock.CalculateHash())
                {
                    return false;
                }
                if (currentBlock.PreviousHash!=previousBlock.Hash)
                {
                    return false;
                }
               
            }
            return true;
        }
        public int GetBalance(string address)
        {
            int balance = 0;
            for (int i = 0; i < Chain.Count; i++)
            {
                
                for (int j = 0; j < Chain[i].Transactions.Count; j++)//
                {
                    var transaction = Chain[i].Transactions[j];
                    if (transaction.FromAddress == address)
                    {
                        balance -= transaction.Amount;
                    }
                    if (transaction.ToAddress == address)
                    {
                        balance += transaction.Amount;
                    }
                }
            }
            return balance;
        }
    }
}
