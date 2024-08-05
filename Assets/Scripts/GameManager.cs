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
        //StartCoroutine(rotateWheel());
    }

    public void btnTestButton_Click()
    {
        m_FortuneWheel.StopSpin();
        //m_Server.GetPrizeIndex();
    }

/*
    private IEnumerator rotateWheel()
    {
        float currentTime = 0f;
        float spinSpeed = m_FortuneWheelSpinSpeed * 360;
        float deceleration = spinSpeed / m_FortuneWheelSpinDuration;
        float targetAngle = 0f;
        bool isTargetAngleSet = false;
        //float rotation = 0f;

        while (currentTime < m_FortuneWheelSpinDuration)
        {
            if (m_PrizeIndex != -1 && isTargetAngleSet == false)
            {
                float rangeOfPrizeAngle = 360f / (k_AmountOfPrizes + 1);

                targetAngle = rangeOfPrizeAngle * m_PrizeIndex;
                isTargetAngleSet = true;
                Debug.Log($"Server -> Prize index number: {m_PrizeIndex}{Environment.NewLine}, Target angle: {targetAngle}");
            }

            currentTime += Time.deltaTime;
            spinSpeed -= deceleration * Time.deltaTime;
            m_FortuneWheelTransform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

            if (isTargetAngleSet == true && targetAngle <= m_FortuneWheelTransform.rotation.z &&
                m_FortuneWheelTransform.rotation.z <= targetAngle + (360f / (k_AmountOfPrizes + 1)))
            {
                m_FortuneWheelTransform.rotation = quaternion.Euler(0, 0, targetAngle);
                break;
            }
            else
            {
                currentTime += Time.deltaTime;
                spinSpeed -= deceleration * Time.deltaTime;
                m_FortuneWheelTransform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            }

            if (isTargetAngleSet == true)
            {
                float currentRotation = 0f;
                float deltaAngle = 0f;

                currentTime += Time.deltaTime;
                spinSpeed -= deceleration * Time.deltaTime;
                currentRotation = m_FortuneWheelTransform.eulerAngles.z % 360;
                deltaAngle = Mathf.DeltaAngle(currentRotation, targetAngle);

                if (Mathf.Abs(deltaAngle) < 30f)
                {
                    spinSpeed = Mathf.Lerp(spinSpeed, 0, Time.deltaTime * 5);
                }
            }

            if (spinSpeed >= 0 || currentTime < m_FortuneWheelSpinDuration)
            {
                m_FortuneWheelTransform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
            }

            //m_FortuneWheelTransform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            //rotation += spinSpeed * Time.deltaTime;
            //m_FortuneWheelTransform.rotation = Quaternion.Euler(0, 0, rotation);

            yield return null;
        }

        //m_FortuneWheelTransform.Rotate(Vector3.back, 0f);
        //m_FortuneWheelTransform.rotation = Quaternion.Euler(0, 0, targetAngle);
        m_PrizeIndex = -1;
    }*/

    private void OnDestroy()
    {
        m_Server.PrizeIndex_Received -= prizeIndexReceived;
    }
}
