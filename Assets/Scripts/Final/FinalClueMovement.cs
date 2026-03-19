using KoziPardy.Core;
using UnityEngine;

public class FinalClueMovement : MonoBehaviour
{
    [Header("Final Clue Control")]
    [SerializeField] FinalClueControl finalClueControl;

    [Header("Positions")]
    [SerializeField] Vector3 resetPosition;
    [SerializeField] Vector3 moveInPosition;
    [SerializeField] Vector3 pullBackPosition;
    [SerializeField] Vector3 moveOutPosition;
    private Vector3 destinationPosition;

    [Header("Rotations")]
    [SerializeField] Quaternion initialRotation;
    [SerializeField] Quaternion finalRotation;

    [Header("Speeds")]
    [SerializeField] float movementSpeed = 1;
    [SerializeField] float pullBackSpeed = 1;
    [SerializeField] float SecondsToCompleteRotation = 1;
    private float secondsPassed = 0;
    private float step = 0;
    private bool pullingBack = false;

    private bool canMove = false;
    private bool canRotate = false;

    private bool hasBeenRotated = false;

    private void Start()
    {
        transform.localPosition = resetPosition;
        destinationPosition = transform.localPosition;
    }

    void Update()
    {
        if (canMove)
        {
            if (transform.position != destinationPosition) MoveToDestination();
            else ArrivedAtDestination();
        }

        if (canRotate)
        {
            if (transform.rotation != finalRotation) RotateOneEighty();
            else ArrivedAtRotation();
        }
    }
    private void MoveToDestination()
    {
        if (!pullingBack) step = Time.deltaTime * movementSpeed;
        else step = Time.deltaTime * pullBackSpeed;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destinationPosition, step);
    }

    private void RotateOneEighty()
    {
        secondsPassed += Time.deltaTime;
        float rotationStep = secondsPassed / SecondsToCompleteRotation;

        transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, rotationStep);
    }

    public void MoveFinalClueIn()
    {
        destinationPosition = moveInPosition;
        canMove = true;
    }

    public void PullFinalClueBack()
    {
        destinationPosition = pullBackPosition;
        canMove = true;
    }

    public void MoveFinalClueOut()
    {
        destinationPosition = moveOutPosition;
        canMove = true;
    }

    private void ArrivedAtDestination()
    {
        canMove = false;

        if (transform.position == moveInPosition)
        {
            finalClueControl.FinalClueIsInPlace();
        }

        if (transform.position == pullBackPosition)
        {
            canRotate = true;
        }

        if (transform.position == moveOutPosition)
        {
            finalClueControl.FinalClueIsMovedOut();
        }
    }

    private void ArrivedAtRotation()
    {
        canRotate = false;
        hasBeenRotated = true;
        MoveFinalClueIn();
    }
}
