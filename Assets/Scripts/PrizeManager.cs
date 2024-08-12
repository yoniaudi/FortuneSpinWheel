using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrizeManager : MonoBehaviour
{
    [SerializeField] private PrizeData[] m_PrizeDatas = null;
    [SerializeField] private Image m_PrizeImage = null;
    [SerializeField] private Text m_PrizeText = null;
    private int m_PrizeIndex = -1;

    public void ShowPrize(int i_PrizeIndex)
    {
        m_PrizeIndex = i_PrizeIndex;
        setPrizeData();
        gameObject.SetActive(true);
        StartCoroutine(ShowPrizePanel());
    }

    private void setPrizeData()
    {
        if (m_PrizeIndex != -1 && m_PrizeIndex < m_PrizeDatas.Length)
        {
            string prizeText = $"+{m_PrizeDatas[m_PrizeIndex].Amount}";

            if (m_PrizeDatas[m_PrizeIndex].Amount == 0)
            {
                prizeText = "0";
            }

            m_PrizeText.text = prizeText;
            m_PrizeImage.sprite = m_PrizeDatas[m_PrizeIndex].Image;
        }
    }

    IEnumerator ShowPrizePanel()
    {
        yield return new WaitForSeconds(4);

        gameObject.SetActive(false);
    }

    private void Reset()
    {
        m_PrizeIndex = -1;
    }
}
