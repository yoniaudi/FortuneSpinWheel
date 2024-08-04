using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Server m_Server = null;
    [SerializeField] private Transform m_FortuneWheelTransform = null;
    [SerializeField] private float m_FortuneWheelSpinSpeed = 5f;
    [SerializeField] private float m_FortuneWheelSpinDuration = 7f;
    private const int k_AmountOfPrizes = 6;
    private int m_PrizeIndex = -1;

    private void Start()
    {
        m_Server.PrizeIndex_Received += prizeIndexReceived;
    }

    private void prizeIndexReceived(int i_PrizeIndex)
    {
        m_PrizeIndex = i_PrizeIndex;
    }

    public void btnSpin_Clicked()
    {
        rotateFortuneWheel();
    }

    private void rotateFortuneWheel()
    {
        m_Server.GetPrizeIndex();
        StartCoroutine(rotateWheel());
    }
    
    private IEnumerator rotateWheel()
    {
        float currentTime = 0f;
        float spinSpeed = m_FortuneWheelSpinSpeed * 360;
        float deceleration = spinSpeed / m_FortuneWheelSpinDuration;
        float targetAngle = 0f;
        bool isTargetAngleSet = false;

        while (currentTime < m_FortuneWheelSpinDuration)
        {
            if (m_PrizeIndex != -1 && isTargetAngleSet == false)
            {
                float rangeOfPrizeAngle = 360f / (k_AmountOfPrizes + 1);

                targetAngle = rangeOfPrizeAngle * m_PrizeIndex;
                isTargetAngleSet = true;
            }

            if (isTargetAngleSet == true)
            {
                float currentRotation = 0f;
                float deltaAngle = 0f;

                currentTime += Time.deltaTime;
                spinSpeed -= deceleration * Time.deltaTime;
                currentRotation = m_FortuneWheelTransform.eulerAngles.z;
                deltaAngle = Mathf.DeltaAngle(currentRotation, targetAngle);

                if (Mathf.Abs(deltaAngle) < 5f )
                {
                    spinSpeed = Mathf.Lerp(spinSpeed, 0, Time.deltaTime * 10);
                }
            }

            if (spinSpeed >= 0)
            {
                m_FortuneWheelTransform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
            }

            yield return null;
        }
                
        m_FortuneWheelTransform.Rotate(Vector3.back, 0f);
        m_PrizeIndex = -1;
    }

    private void OnDestroy()
    {
        m_Server.PrizeIndex_Received -= prizeIndexReceived;
    }
}
