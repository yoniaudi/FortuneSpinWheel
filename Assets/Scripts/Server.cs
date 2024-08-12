using System;
using System.Collections;
using UnityEngine;

public class Server : MonoBehaviour
{
    private const int k_AmountOfPrizes = 6;
    public int AmountOfPrizes { get { return k_AmountOfPrizes; } }
    public event Action<int> PrizeIndex_Received = null;

    public void GetPrizeIndex()
    {
        StartCoroutine(GetIndexFromServer());
    }

    private IEnumerator GetIndexFromServer()
    {
        int prizeIndex = UnityEngine.Random.Range(0, k_AmountOfPrizes);
        float responseTime = UnityEngine.Random.Range(1f, 4f);

        yield return new WaitForSeconds(responseTime);

        onPrizeIndexReceived(prizeIndex);
    }

    private void onPrizeIndexReceived(int i_PrizeIndex)
    {
        PrizeIndex_Received?.Invoke(i_PrizeIndex);
    }
}
