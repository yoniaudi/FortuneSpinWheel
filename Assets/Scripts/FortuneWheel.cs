using System;
using System.Collections;
using UnityEngine;

public class FortuneWheel : MonoBehaviour
{
    [SerializeField] private float m_SpinSpeed = 2f;
    private float m_DecelerationFactor = 270f;
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
        transform.rotation *= Quaternion.Euler(i_SpinRate * Time.deltaTime * Vector3.forward);
    }

    private IEnumerator DecelerateAndStop(float i_SpinRate, float i_TargetStartDegree, float i_TargetDegreeRange)
    {
        const float fullSpin = 360f;
        float deltaAngle = 0f;
        float deceleration = fullSpin / i_SpinRate;
        float timeToWaitBeforeShowingPrize = 0.5f;

        while (i_SpinRate >= 0)
        {
            Spin(i_SpinRate);
            deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.z, i_TargetStartDegree + i_TargetDegreeRange / 2);

            if (Mathf.Abs(deltaAngle) < 2f && i_SpinRate <= m_DecelerationFactor)
            {
                transform.rotation = Quaternion.Euler(0, 0, i_TargetStartDegree + i_TargetDegreeRange / 2);
                break;
            }

            i_SpinRate -= deceleration * fullSpin * Time.deltaTime * 2f;
            i_SpinRate = Mathf.Max(i_SpinRate, m_DecelerationFactor);

            yield return null;
        }

        yield return new WaitForSeconds(timeToWaitBeforeShowingPrize);

        FortuneWheel_Stoped?.Invoke();
        Reset();
    }

    private void Reset()
    {
        m_StopIndex = -1;
        transform.rotation = Quaternion.identity;
    }
}
