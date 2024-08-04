using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour
{
    public event Action<int> PrizeIndex_Received = null;

    public void GetPrizeIndex()
    {
        StartCoroutine(GetIndexFromServer());
    }

    private IEnumerator GetIndexFromServer()
    {
        int prizeIndex = UnityEngine.Random.Range(0, 6);
        float responseTime = UnityEngine.Random.Range(2f, 8f);

        yield return new WaitForSeconds(responseTime);

        onPrizeIndexReceived(prizeIndex);
    }

    private void onPrizeIndexReceived(int i_PrizeIndex)
    {
        PrizeIndex_Received?.Invoke(i_PrizeIndex);
    }
}
