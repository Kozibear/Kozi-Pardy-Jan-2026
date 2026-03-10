using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameBoardManager gameBoardManager;

    [Header("Position")]
    [SerializeField] Vector3 originalPosition;
    [SerializeField] Vector3 wheelSpinPosition;

    [Header("Rotation")]
    [SerializeField] Quaternion originalRotation;
    [SerializeField] Quaternion wheelSpinRotation;

    [Header("Speeds")]
    [SerializeField] float moveSpeed = 1;

    private Vector3 destinationPosition;
    private bool canMoveClue = false;

    void Start()
    {
        originalPosition = transform.position;
        destinationPosition = transform.position;

        originalRotation = transform.rotation;
    }

    void Update()
    {
        float step = Time.deltaTime * moveSpeed;

        if (canMoveClue)
        {
            if (transform.localPosition != destinationPosition) MoveToDestination(step);
            else ArrivedAtDestination();
        }
    }

    private void MoveToDestination(float step)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destinationPosition, step);
    }

    private IEnumerator RotateToDestination(Quaternion target)
    {
        Quaternion from = transform.rotation;

        float startDistance = Vector3.Distance(transform.localPosition, destinationPosition);

        while (transform.localPosition != destinationPosition)
        {
            float currentDistance = Vector3.Distance(transform.localPosition, destinationPosition);
            float distanceDifference = (startDistance - currentDistance) / startDistance;

            transform.rotation = Quaternion.Slerp(from, target, distanceDifference);

            yield return null;
        }
        transform.rotation = target;
    }

    private void ArrivedAtDestination()
    {
        canMoveClue = false;

        if (transform.localPosition == originalPosition)
        {
            gameBoardManager.ActivateBoardClues();
        }
    }

    public void MoveForWheelSpin()
    {
        destinationPosition = wheelSpinPosition;
        canMoveClue = true;
        StartCoroutine(RotateToDestination(wheelSpinRotation));
    }

    public void MoveBackToNormal()
    {
        destinationPosition = originalPosition;
        canMoveClue = true;
        StartCoroutine(RotateToDestination(originalRotation));
    }
}
