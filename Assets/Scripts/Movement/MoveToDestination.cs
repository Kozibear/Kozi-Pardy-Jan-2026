using UnityEngine;

public class MoveToDestination : MonoBehaviour
{
    [SerializeField] Transform destinationPosition;
    [SerializeField] float moveSpeed = 1.0f;

    void Update()
    {
        float step = Time.deltaTime * moveSpeed;

        if (transform.localPosition != destinationPosition.position)
        {
            MoveToDestinationPosition(step);
        }
    }

    private void MoveToDestinationPosition(float step)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destinationPosition.position, step);
    }
}
