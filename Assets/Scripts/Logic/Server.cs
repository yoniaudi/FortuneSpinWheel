using System;
using System.Threading.Tasks;

public class Server
{
    private const int k_AmountOfPrizes = 6;
    public int AmountOfPrizes { get { return k_AmountOfPrizes; } }
    public event Action<int> PrizeIndex_Received = null;

    public void GetPrizeIndex()
    {
        Task.Run(() => GetIndexFromServer());
    }

    async Task GetIndexFromServer()
    {
        System.Random random = new System.Random();
        int prizeIndex = random.Next(k_AmountOfPrizes);
        int responseTime = random.Next(1, 4) * 1000;

        await Task.Delay(responseTime);
        PrizeIndex_Received?.Invoke(prizeIndex);
    }
}
