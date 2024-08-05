using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheel : MonoBehaviour
{
    [SerializeField] private float m_SpinSpeed = 2f;
    private int m_StopIndex = -1;
    private bool m_IsSpinning = false;
    private bool m_IsSpinningOffsetActive = false;

    public void StartSpin()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(SpinWheel());
    }

    public void StopSpin(int i_Index = -1)
    {
        m_StopIndex = i_Index;
        m_IsSpinning = false;
    }

    private IEnumerator SpinWheel()
    {
        float fullSpin = 360f;
        float spinRate = m_SpinSpeed * fullSpin;
        float deceleration = spinRate / 3f;
        float targetDegreeRange = fullSpin / 6f;
        float targetAngleWithOffset = 0f;
        float targetStartDegree = 0f;

        m_IsSpinning = true;
        m_IsSpinningOffsetActive = true;

        while (m_IsSpinning == true)
        {
            transform.rotation *= Quaternion.Euler(Vector3.forward * spinRate * Time.deltaTime);

            yield return null;
        }

        targetStartDegree = targetDegreeRange * m_StopIndex;
        int count = 0;

        while (m_IsSpinningOffsetActive == true)
        {
            transform.rotation *= Quaternion.Euler(Vector3.forward * spinRate * Time.deltaTime);

            if (targetStartDegree <= transform.eulerAngles.z && transform.eulerAngles.z <= targetStartDegree + targetDegreeRange)
            {
                if (transform.eulerAngles.z + 1f > targetStartDegree + targetDegreeRange)
                {
                    count++;
                    Debug.Log(count);
                }

                if (count > 2)
                {
                    m_IsSpinningOffsetActive = false;
                }
            }

            yield return null;
        }
    }
}
