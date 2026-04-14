using UnityEngine;

public class GradientRotation : MonoBehaviour
{
    [SerializeField] float speed = 1;

    private void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * -speed);
    }
}
