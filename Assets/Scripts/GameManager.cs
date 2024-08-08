using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Server m_Server = null;
    [SerializeField] private FortuneWheel m_FortuneWheel = null;
    [SerializeField] private Transform m_FortuneWheelTransform = null;
    private const int k_AmountOfPrizes = 6;

    private void Start()
    {
        m_Server.PrizeIndex_Received += prizeIndexReceived;
    }

    private void prizeIndexReceived(int i_PrizeIndex)
    {
        Debug.Log($"Server -> Prize index number: {i_PrizeIndex}{Environment.NewLine}");
        m_FortuneWheel.StopSpin(i_PrizeIndex);
    }

    public void btnSpin_Clicked()
    {
        m_FortuneWheelTransform.eulerAngles = Vector3.zero;
        rotateFortuneWheel();
    }

    private void rotateFortuneWheel()
    {
        m_FortuneWheel.StartSpin();
        m_Server.GetPrizeIndex();
    }

    private void OnDestroy()
    {
        m_Server.PrizeIndex_Received -= prizeIndexReceived;
    }
}
