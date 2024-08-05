using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheel : MonoBehaviour
{
    [SerializeField] private float m_SpinSpeed = 2f;
    [SerializeField] private float m_DecelerationFactor = 50f;
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
        int extraSpins = 3;
        float deltaAngle = 0;

        while (m_IsSpinningOffsetActive == true)
        {
            Spin(i_SpinRate);

            if (IsInTargetRange(i_TargetStartDegree, i_TargetDegreeRange) == true)
            {
                if (transform.eulerAngles.z + 1f > i_TargetStartDegree + i_TargetDegreeRange)
                {
                    extraSpins--;
                    Debug.Log(extraSpins);
                }
            }

            float fineTune = i_SpinRate - m_DecelerationFactor * Time.deltaTime;
            i_SpinRate = Mathf.Max(fineTune, 10f);
            deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, i_TargetStartDegree + i_TargetDegreeRange / 2);

            if (extraSpins <= 0 && Mathf.Abs(deltaAngle) < 1f)
            {
                m_IsSpinningOffsetActive = false;
            }

            yield return null;
        }
    }

    /*private IEnumerator DecelerateAndStop(float i_SpinRate, float i_TargetStartDegree, float i_TargetDegreeRange)
    {
        int extraSpins = 3;
        float deltaAngle = 0;
        float decelerationFactor = 0.5f;

        while (m_IsSpinningOffsetActive == true)
        {
            Spin(i_SpinRate);

            if (IsInTargetRange(i_TargetStartDegree, i_TargetDegreeRange) == true)
            {
                if (transform.eulerAngles.z + 1f > i_TargetStartDegree + i_TargetDegreeRange)
                {
                    extraSpins--;
                    Debug.Log(extraSpins);
                    i_SpinRate *= decelerationFactor;
                }
            }

            deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, i_TargetStartDegree + i_TargetDegreeRange / 2);

            if (extraSpins <= 0 && Mathf.Abs(deltaAngle) < 1f)
            {
                m_IsSpinningOffsetActive = false;
            }

            yield return null;
        }
    }*/

    private bool IsInTargetRange(float i_TargetStartDegree, float i_TargetDegreeRange)
    {
        return i_TargetStartDegree <= transform.eulerAngles.z && transform.eulerAngles.z <= i_TargetStartDegree + i_TargetDegreeRange;
    }
}
