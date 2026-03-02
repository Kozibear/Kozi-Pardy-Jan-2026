using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    [SerializeField] float rotationRate = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationRate * Time.deltaTime);
    }
}
