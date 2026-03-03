using KoziPardy.ColorManagement;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinMovement : MonoBehaviour
{
    [Header("Wheel References")]
    [SerializeField] WheelArrow wheelArrow;
    [SerializeField] GameObject justWheel;

    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;

    [Header("Moving Positions")]
    [SerializeField] Vector3 ResetPosition;
    [SerializeField] Vector3 moveInPosition;
    [SerializeField] Vector3 moveOutPosition;

    [Header("Move Speed")]
    [SerializeField] float moveInSpeed;
    [SerializeField] float moveOutSpeed;
    private float moveSpeed;

    [Header("Rotation")]
    [SerializeField] float desiredDurationInSeconds = 0;
    private float elapsedTimeInSeconds = 0;
    [SerializeField] float startingRotationSpeed = 100;

    private bool canMove = false;
    private bool canRotate = false;

    private Vector3 destinationPosition;

    private void Start()
    {
        transform.localPosition = ResetPosition;
    }

    private void Update()
    {
        float step = Time.deltaTime * moveSpeed;

        if (canMove)
        {
            if (transform.localPosition != destinationPosition)
            {
                MoveToDestination(step);
            }
            else ArrivedAtDestination();
        }

        if (canRotate)
        {
            Rotate();
        }
    }

    private void MoveToDestination(float step)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destinationPosition, step);
    }

    private void ArrivedAtDestination()
    {
        canMove = false;

        if (destinationPosition == moveOutPosition)
        {
            GetComponent<WheelSpinSegmentControl>().DestroyWheelSegments();
            transform.localPosition = ResetPosition;
        }
    }

    private void Rotate()
    {
        //About Rotation issues:
        //Previously, my code was tempRotationSpeed -= Time.deltaTime * subtractionFactor
        //This resulted in the wheel spinning a lot on fast computers, and only a little on slow computers
        //This is because the way my code was written made the time the wheel took to slow always consistent,
        //Rather than making the speed at which the wheel moved consistent

        elapsedTimeInSeconds += Time.deltaTime;
        float percentageComplete = elapsedTimeInSeconds / desiredDurationInSeconds;

        percentageComplete = Mathf.Sin(percentageComplete * Mathf.PI * 0.5f);
        float zRotation = Mathf.Lerp(startingRotationSpeed, 0, percentageComplete);

        justWheel.transform.Rotate(0, 0, -zRotation);

        if (elapsedTimeInSeconds >= desiredDurationInSeconds)
        {
            canRotate = false;
            justWheel.transform.Rotate(0, 0, 0);
            GetComponent<WheelSpinBoardSelections>().ConfirmBoardSelection();
        }
    }

    public void StartWheelSpin()
    {
        //Wheel segments setup
        GetComponent<WheelSpinSegmentControl>().UpdatePossibleWheelSegments();

        //Movement setup
        destinationPosition = moveInPosition;
        moveSpeed = moveInSpeed;
        canMove = true;

        //Rotation setup
        float randomDegree = Random.Range(0f, 360f);
        justWheel.transform.Rotate(0, 0, randomDegree);

        elapsedTimeInSeconds = 0;
        canRotate = true;

        wheelArrow.SetTriggerSelection(true);
    }

    public void MoveOutWheelSpin()
    {
        destinationPosition = moveOutPosition;
        moveSpeed = moveOutSpeed;
        canMove = true;

        wheelArrow.SetTriggerSelection(false);
    }
}