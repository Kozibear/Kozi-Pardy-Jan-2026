using KoziPardy.Core;
using System.Collections;
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
    [SerializeField] Quaternion halfRotation;
    [SerializeField] Quaternion finalRotation;

    [Header("Speeds")]
    [SerializeField] float movementInSpeed = 1;
    [SerializeField] float movementOutSpeed = 1;
    [SerializeField] float pullBackSpeed = 1;
    private float currentMovementSpeed = 0;
    private float step = 0;
    private bool pullingBack = false;

    private bool canMove = false;

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
            if (transform.localPosition != destinationPosition) MoveToDestination();
            else ArrivedAtDestination();
        }
    }
    private void MoveToDestination()
    {
        if (!pullingBack) step = Time.deltaTime * currentMovementSpeed;
        else step = Time.deltaTime * pullBackSpeed;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destinationPosition, step);
    }

    IEnumerator RotateOneEightyCoroutine(Quaternion destinationRotation)
    {
        float startDistance = Vector3.Distance(transform.localPosition, destinationPosition);
        Quaternion from = transform.localRotation;

        while (transform.localRotation != destinationRotation)
        {
            //secondsPassed += Time.deltaTime;
            //float rotationStep = secondsPassed / SecondsToCompleteRotation;

            float currentDistance = Vector3.Distance(transform.localPosition, destinationPosition);
            float distanceDifference = (startDistance - currentDistance) / startDistance;

            transform.localRotation = Quaternion.Slerp(from, destinationRotation, distanceDifference);

            //THIS IS VERY IMPORTANT! WE NEED TO TELL UNITY TO ADVANCE TO THE NEXT FRAME AT THE END OF A LOOP IN A COROUTINE!
            //IF WE DON'T, UNITY WILL ATTEMPT TO DO THIS ENTIRE LOOP INSTANTLY; WITHIN A SINGLE FRAME
            yield return null;
        }

        hasBeenRotated = true;
    }

    public void MoveFinalClueIn()
    {
        currentMovementSpeed = movementInSpeed;
        destinationPosition = moveInPosition;
        canMove = true;
    }

    public void PullFinalClueBack()
    {
        finalClueControl.FadeOutVignette(0.3f);

        currentMovementSpeed = pullBackSpeed;
        destinationPosition = pullBackPosition;
        StartCoroutine(RotateOneEightyCoroutine(halfRotation));
        canMove = true;
    }

    public void PullFinalClueForward()
    {
        finalClueControl.FadeInVignette(0.3f);

        currentMovementSpeed = pullBackSpeed;
        destinationPosition = moveInPosition;
        StartCoroutine(RotateOneEightyCoroutine(finalRotation));
        canMove = true;
    }

    public void MoveFinalClueOut()
    {
        finalClueControl.ClueIsMovingOut();

        currentMovementSpeed = movementOutSpeed;
        destinationPosition = moveOutPosition;
        canMove = true;
    }

    private void ArrivedAtDestination()
    {
        canMove = false;

        if (transform.localPosition == moveInPosition)
        {
            finalClueControl.FinalClueIsInPlace();
        }

        if (transform.localPosition == pullBackPosition)
        {
            PullFinalClueForward();
        }
        
        if (transform.localPosition == moveOutPosition)
        {
            finalClueControl.FinalClueIsMovedOut();
            transform.localPosition = resetPosition;
        }
    }

    public bool GetHasBeenRotated()
    {
        return hasBeenRotated;
    }
}
