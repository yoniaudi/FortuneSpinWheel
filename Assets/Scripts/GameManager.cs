using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform m_FortuneWheelTransform = null;
    [SerializeField] private float m_FortuneWheelSpinSpeed = 5f;
    [SerializeField] private float m_FortuneWheelSpinDuration = 7f;
    private const int k_AmountOfPrizes = 6;

    public void btnSpin_Clicked()
    {
        Debug.Log("Clicked");
        rotateFortuneWheel();
    }

    private void rotateFortuneWheel()
    {
        StartCoroutine(rotateWheel());
    }
    
    private IEnumerator rotateWheel()
    {
        int multiplyer = Random.Range(1, 4);
        float currentTime = 0f;
        float spinSpeed = m_FortuneWheelSpinSpeed * 360;
        float deceleration = spinSpeed / m_FortuneWheelSpinDuration;

        while (currentTime < m_FortuneWheelSpinDuration)
        {
            currentTime += Time.deltaTime;
            spinSpeed -= deceleration * Time.deltaTime;

            if (spinSpeed >= 0)
            {
                m_FortuneWheelTransform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
            }

            yield return null;
        }
                
        m_FortuneWheelTransform.Rotate(Vector3.back, 0f);
    }
}
