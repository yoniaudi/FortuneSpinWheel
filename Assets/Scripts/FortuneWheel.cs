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
        const float fullSpin = 360f;
        const int prizeSegments = 6;
        float spinRate = m_SpinSpeed * fullSpin;
        float targetDegreeRange = fullSpin / prizeSegments;
        float targetStartDegree = 0f;

        m_IsSpinning = true;
        m_IsSpinningOffsetActive = true;

        while (m_IsSpinning == true)
        {
            Spin(spinRate);
            yield return null;
        }

        targetStartDegree = targetDegreeRange * m_StopIndex;

        yield return StartCoroutine(DecelerateAndStop(spinRate, targetStartDegree, targetDegreeRange));
    }

    private void Spin(float i_SpinRate)
    {
        transform.rotation *= Quaternion.Euler(Vector3.forward * i_SpinRate * Time.deltaTime);
    }

    private IEnumerator DecelerateAndStop(float i_SpinRate, float i_TargetStartDegree, float i_TargetDegreeRange)
    {
        int count = 0;

        while (m_IsSpinningOffsetActive == true)
        {
            Spin(i_SpinRate);

            if (IsInTargetRange(i_TargetStartDegree, i_TargetDegreeRange))
            {
                if (transform.eulerAngles.z + 1f > i_TargetStartDegree + i_TargetDegreeRange)
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

    private bool IsInTargetRange(float targetStartDegree, float targetDegreeRange)
    {
        return targetStartDegree <= transform.eulerAngles.z && transform.eulerAngles.z <= targetStartDegree + targetDegreeRange;
    }
}
