using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Server m_Server = null;
    [SerializeField] private FortuneWheel m_FortuneWheel = null;
    [SerializeField] private Transform m_FortuneWheelTransform = null;
    [SerializeField] private UnityEngine.UI.Button m_SpinButton = null;
    private const int k_AmountOfPrizes = 6;

    private void OnEnable()
    {
        m_Server.PrizeIndex_Received += onPrizeIndex_Received;
        m_FortuneWheel.FortuneWheel_Stoped += onFortuneWheel_Stoped;
    }

    /*private void Start()
    {
    }*/

    private void onPrizeIndex_Received(int i_PrizeIndex)
    {
        Debug.Log($"Server -> Prize index number: {i_PrizeIndex}{Environment.NewLine}");
        m_FortuneWheel.StopSpin(i_PrizeIndex);
    }

    public void btnSpin_Clicked()
    {
        m_SpinButton.gameObject.SetActive(false);
        m_FortuneWheelTransform.eulerAngles = Vector3.zero;
        rotateFortuneWheel();
    }

    private void rotateFortuneWheel()
    {
        m_FortuneWheel.StartSpin(k_AmountOfPrizes);
        m_Server.GetPrizeIndex();
    }

    private void onFortuneWheel_Stoped()
    {
        m_SpinButton.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        m_Server.PrizeIndex_Received -= onPrizeIndex_Received;
        m_FortuneWheel.FortuneWheel_Stoped -= onFortuneWheel_Stoped;
    }
}
