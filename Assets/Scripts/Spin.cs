using UnityEngine;

public class Spin : MonoBehaviour
{
    public float Speed = 0;

    void Update()
    {
        transform.rotation = Quaternion.Euler(Speed * Time.deltaTime * Vector3.forward) * transform.rotation;
    }
}