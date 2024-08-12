using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheel : MonoBehaviour
{
    [SerializeField] private float m_SpinSpeed = 2f;
    private float m_DecelerationFactor = 180f;
    private int m_PrizesCount = 0;
    private int m_StopIndex = -1;
    private bool m_IsSpinning = false;
    public event Action FortuneWheel_Stoped = null;

    public void StartSpin(int i_PrizesCount = 0)
    {
        m_PrizesCount = i_PrizesCount;
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
        float spinRate = m_SpinSpeed * fullSpin;
        float targetDegreeRange = fullSpin / m_PrizesCount;
        float targetStartDegree = 0f;

        m_IsSpinning = true;

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
        const float fullSpin = 360f;
        float desiredSpins = m_SpinSpeed * 360f;
        float deltaAngle = 0f;
        float deceleration = fullSpin / i_SpinRate;

        while (desiredSpins >= 0)
        {
            Spin(desiredSpins);
            deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, i_TargetStartDegree + i_TargetDegreeRange / 2);

            if (Mathf.Abs(deltaAngle) < 2f && desiredSpins <= m_DecelerationFactor)
            {
                break;
            }

            desiredSpins -= deceleration * fullSpin * Time.deltaTime;
            desiredSpins = Mathf.Max(desiredSpins, m_DecelerationFactor);

            yield return null;
        }

        FortuneWheel_Stoped?.Invoke();
    }

    private bool IsWheelArrowInTargetRange(float i_TargetStartDegree, float i_TargetDegreeRange)
    {
        return i_TargetStartDegree <= transform.eulerAngles.z && transform.eulerAngles.z <= i_TargetStartDegree + i_TargetDegreeRange;
    }
}
