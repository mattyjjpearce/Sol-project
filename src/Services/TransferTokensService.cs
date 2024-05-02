using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Messages;
using Solnet.Rpc.Models;
using Solnet.Wallet;
using Solnet.Programs;
using Microsoft.AspNetCore.Http.HttpResults;




namespace blockchain_test.Services;

public class TransferTokensService
{

    private static readonly IRpcClient rpcClient = ClientFactory.GetClient(Cluster.DevNet);

    public TransferTokensService()
    {

    }


public  RequestResult<string> TransferTokens()
{
  
    var wallet = new Wallet("PRIVATE PASS PHRASE :)");
    var rpcClient = ClientFactory.GetClient(Cluster.DevNet);
    var publicKey = wallet.Account.PublicKey;
    string destinationAddress = "CXkJGmdFHfWMxNnGYxndnL4DHb37QqJTkRaM5JWkDCzd";
    string tokenMintAddress = "DG414jG6ux9sKCcTJLzRZGwzfsEjGFfyZLpyVfT9VKMm";
    ulong amount = 5; // Number of tokens to send
    // Getting the associated token accounts for the sender and recipient
    var sourceTokenAccount = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(publicKey, new PublicKey(tokenMintAddress));
    var destinationTokenAccount = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(new PublicKey(destinationAddress), new PublicKey(tokenMintAddress));
    var blockHash = rpcClient.GetRecentBlockHash();
    if (!blockHash.WasSuccessful) {
        Console.WriteLine("Failed to get recent block hash");
        return null;
    }

 Console.WriteLine(wallet);

    var transaction = new TransactionBuilder()
        .SetRecentBlockHash(blockHash.Result.Value.Blockhash)
        .SetFeePayer(publicKey)
        .AddInstruction(TokenProgram.Transfer(
            sourceTokenAccount,
            destinationTokenAccount,
            amount,
            publicKey// This is the authority's public key, required to authorize the transfer
            )) 
        .Build(wallet.Account);

    Console.WriteLine(transaction);

    var result = rpcClient.SendTransaction(transaction);
    Console.WriteLine(result.WasSuccessful ? $"Transaction successful, signature: {result.Result}" : $"Transaction failed:$ {result} ");


    return result;

}

    
}