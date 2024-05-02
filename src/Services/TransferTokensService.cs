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
  
    var wallet = new Wallet("adult audit laugh allow roast exchange lady chef faith online punch stairs");
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
            )) // Signer account needed to authorize the transfer
        .Build(wallet.Account);

    Console.WriteLine(transaction);

    var result = rpcClient.SendTransaction(transaction);
    Console.WriteLine(result.WasSuccessful ? $"Transaction successful, signature: {result.Result}" : $"Transaction failed:$ {result} ");


    return result;


    // string toWalletAddress = "DrRHHgKqiY6msgkCon7VwmVtEf7vW8J9KhvymH3Z24Lv";
    // Wallet toWallet = new Wallet(toWalletAddress);
    // Wallet fromWallet = new Wallet("CpaGH14v7XdbAPEATWNtx1mGEXWjD6V71zaFwCfL2SRr");

    // Account fromAccount = fromWallet.GetAccount(10);
    // PublicKey toPublicKey = toWallet.GetAccount(0).PublicKey;

    // string tokenAddress = "Ead2p8MbCNcU1eREpAsBhwrD2haMtHSuWmYMsjHeZCyF"; // Replace <token_address> with your token address
    // ulong amountToSend = 100; // Adjust the amount of tokens to send as needed



    // var tx = new TransactionBuilder()
    //     .SetFeePayer(fromAccount)
    //     .AddInstruction(TokenProgram.Transfer(
    //         fromAccount.PublicKey,
    //         toPublicKey,
    //         amountToSend,
    //         fromAccount.PublicKey
    //         ))
    //     .Build(fromAccount);

    // RequestResult<ResponseValue<SimulationLogs>> txSim = await rpcClient.SimulateTransactionAsync(tx);
    // Console.WriteLine($"Transaction Simulation:\n\tError: {txSim.Result.Value.Error}");

    // if (txSim.Result.Value.Error != null)
    // {
    //     Console.WriteLine($"Simulation failed with error: {txSim.Result.Value.Error}");
    //     return null;
    // }

    // RequestResult<string> firstSig = await rpcClient.SendTransactionAsync(tx);
    // if (firstSig.WasSuccessful)
    // {
    //     Console.WriteLine($"First Tx Signature: {firstSig.Result}");
    // }
    // else
    // {
    //     Console.WriteLine($"Sending transaction failed with error: {firstSig.ErrorData.ToString}");
    // }

    // return firstSig;
}

    
}