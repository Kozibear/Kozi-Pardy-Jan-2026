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
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationSubtractionFactor;
    [SerializeField] float rotationSlowFactor;
    private float tempRotationSpeed;
    private float tempSubtractionFactor;

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
            //justWheel.transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localPosition = ResetPosition;
        }
    }

    private void Rotate()
    {
        justWheel.transform.Rotate(0, 0, -tempRotationSpeed);
        tempRotationSpeed -= Time.deltaTime * tempSubtractionFactor;

        //tempSubtractionFactor = Mathf.Clamp(tempSubtractionFactor -= rotationSlowFactor, 0.5f, rotationSubtractionFactor);

        if (tempRotationSpeed <= 0)
        {
            canRotate = false;
            justWheel.transform.Rotate(0, 0, 0);
            GetComponent<WheelSpinBoardSelections>().ConfirmBoardSelection();
        }
    }

    public void StartWheelSpin()
    {
        GetComponent<WheelSpinSegmentControl>().UpdatePossibleWheelSegments();

        float randomDegree = Random.Range(0f, 360f);
        //justWheel.transform.eulerAngles = new Vector3(0, 0, randomDegree);

        destinationPosition = moveInPosition;
        moveSpeed = moveInSpeed;
        canMove = true;

        tempRotationSpeed = rotationSpeed;
        tempSubtractionFactor = rotationSubtractionFactor;
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