using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Server m_Server = null;
    [SerializeField] private FortuneWheel m_FortuneWheel = null;
    [SerializeField] private RewardManager m_RewardManager = null;
    [SerializeField] private Button m_SpinButton = null;
    private int m_PrizeIndex = -1;

    private void OnEnable()
    {
        m_Server.PrizeIndex_Received += onPrizeIndex_Received;
        m_FortuneWheel.FortuneWheel_Stoped += onFortuneWheel_Stoped;
    }

    private void onPrizeIndex_Received(int i_PrizeIndex)
    {
        m_PrizeIndex = i_PrizeIndex;
        Debug.Log($"Server -> Prize index number: {i_PrizeIndex}{Environment.NewLine}");
        m_FortuneWheel.StopSpin(i_PrizeIndex);
    }

    public void btnSpin_Clicked()
    {
        m_SpinButton.gameObject.SetActive(false);
        rotateFortuneWheel();
    }

    private void rotateFortuneWheel()
    {
        m_FortuneWheel.StartSpin(m_Server.AmountOfPrizes);
        m_Server.GetPrizeIndex();
    }

    private void onFortuneWheel_Stoped()
    {
        m_RewardManager.ShowPrize(m_PrizeIndex);
        Reset();
    }

    private void OnDestroy()
    {
        m_Server.PrizeIndex_Received -= onPrizeIndex_Received;
        m_FortuneWheel.FortuneWheel_Stoped -= onFortuneWheel_Stoped;
    }

    private void Reset()
    {
        m_PrizeIndex = -1;
        m_SpinButton.gameObject.SetActive(true);
    }
}
