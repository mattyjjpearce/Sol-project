using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Messages;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using Solnet.Programs;




namespace blockchain_test.Services;

public class TransferTokens
{

    
    
    private static readonly IRpcClient rpcClient = ClientFactory.GetClient(Cluster.TestNet);

    public TransferTokens()
    {
        Wallet toWallet = new Wallet("awkward orbit diamond satisfy glue repair average update fix slush bacon acquire");
        Wallet fromWallet = new Wallet("awkward orbit diamond satisfy glue repair average update fix slush bacon acquire");

        Account fromAccount = fromWallet.GetAccount(10);
        Account toAccount = toWallet.GetAccount(0);

        

        RequestResult<ResponseValue<LatestBlockHash>> blockHash =  rpcClient.GetLatestBlockHash();
        Console.WriteLine($"BlockHash >> {blockHash.Result.Value.Blockhash}");

        var tx = new TransactionBuilder()
            .SetRecentBlockHash(blockHash.Result.Value.Blockhash)
            .SetFeePayer(fromAccount)
            .AddInstruction(SystemProgram.Transfer(fromAccount.PublicKey, toAccount.PublicKey, 100))
            .AddInstruction(MemoProgram.NewMemo(fromAccount.PublicKey, "Hello from Sol.Net :)"))
            .Build(fromAccount);

        Console.WriteLine($"Tx base64: {Convert.ToBase64String(tx)}");
        RequestResult<ResponseValue<SimulationLogs>> txSim = rpcClient.SimulateTransaction(tx);
        //string logs = Examples.PrettyPrintTransactionSimulationLogs(txSim.Result.Value.Logs);
        //Console.WriteLine($"Transaction Simulation:\n\tError: {txSim.Result.Value.Error}\n\tLogs: \n" + logs);
        RequestResult<string> firstSig = rpcClient.SendTransaction(tx);
        Console.WriteLine($"First Tx Signature: {firstSig.Result}");
    }
    
    
}