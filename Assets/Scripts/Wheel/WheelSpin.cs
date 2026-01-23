using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    [Header("Wheel References")]
    [SerializeField] string[] wheelSections;
    [SerializeField] GameObject wheelAndArrow;
    [SerializeField] GameObject justWheel;
    [SerializeField] List<WheelSegment> wheelSegments;
    private List<int> wheelSegmentsAvailable = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    [Header("Arrow References")]
    [SerializeField] GameObject justArrow;
    [SerializeField] WheelArrow wheelArrow;

    [Header("Board Buttons")]
    [SerializeField] List<BoardButton> boardButtons;
    private List<int> boardButtonsAvailable = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };

    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;

    [Header("Moving in")]
    [SerializeField] Vector3 moveInPosition;

    [Header("Moving Out")]
    [SerializeField] Vector3 moveOutPosition;

    [Header("Move Speed")]
    [SerializeField] float moveInSpeed;
    [SerializeField] float moveOutSpeed;
    private float moveSpeed;

    [Header("Rotation")]
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationSubtractionFactor;
    private float tempRotationSpeed;

    [Header("Panic Button")]
    [SerializeField] PanicButton panicButton;

    private bool canMove = false;
    private bool canRotate = false;
    private Vector3 destinationPosition;

    private void Start()
    {
        wheelAndArrow.transform.localPosition = moveOutPosition;
    }

    private void FixedUpdate()
    {
        float step = Time.deltaTime * moveSpeed;

        if (canMove && wheelAndArrow.transform.localPosition != destinationPosition)
        {
            wheelAndArrow.transform.localPosition = Vector3.MoveTowards(wheelAndArrow.transform.localPosition, destinationPosition, step);

            if (wheelAndArrow.transform.localPosition == destinationPosition)
            {
                canMove = false;
                if (destinationPosition == moveOutPosition)
                {
                    foreach (WheelSegment segment in wheelSegments) { segment.DestroyChild(); }
                    justWheel.transform.eulerAngles = new Vector3(0, 0, 0);
                }    
            }
        }

        if (canRotate)
        {
            justWheel.transform.Rotate(0, 0, -tempRotationSpeed);
            tempRotationSpeed -= Time.deltaTime * rotationSubtractionFactor;

            if (tempRotationSpeed <= 0)
            {
                canRotate = false;
                justWheel.transform.Rotate(0, 0, 0);
                wheelArrow.SelectSegment();
            }
        }
    }

    public void StartWheelSpin()
    {
        UpdatePossibleWheelSegments();

        canMove = true;
        moveSpeed = moveInSpeed;
        destinationPosition = moveInPosition;

        float randomDegree = Random.Range(0f, 360f);
        justWheel.transform.eulerAngles = new Vector3(0, 0, randomDegree);

        canRotate = true;
        tempRotationSpeed = rotationSpeed;
    }

    public void MoveOutWheelSpin()
    {
        canMove = true;
        moveSpeed = moveOutSpeed;
        destinationPosition = moveOutPosition;
        panicButton.Interactable();
    }

    void UpdatePossibleWheelSegments()
    {
        foreach (BoardButton boardButton in boardButtons)
        {
            if (boardButton.GetHasBeenClicked())
            {
                boardButtonsAvailable.Remove(boardButton.GetNumber());
            }
        }

        if (!boardButtonsAvailable.Contains(0)
            && !boardButtonsAvailable.Contains(5)
            && !boardButtonsAvailable.Contains(10)
            && !boardButtonsAvailable.Contains(15)
            && !boardButtonsAvailable.Contains(20)
            && !boardButtonsAvailable.Contains(25))
        {
            wheelSegmentsAvailable.Remove(0); //Removes $200 / $400 Only
        }

        if (!boardButtonsAvailable.Contains(0)
            && !boardButtonsAvailable.Contains(1)
            && !boardButtonsAvailable.Contains(2)
            && !boardButtonsAvailable.Contains(3)
            && !boardButtonsAvailable.Contains(4)
            && !boardButtonsAvailable.Contains(5)
            && !boardButtonsAvailable.Contains(6)
            && !boardButtonsAvailable.Contains(7)
            && !boardButtonsAvailable.Contains(8)
            && !boardButtonsAvailable.Contains(9))
        {
            wheelSegmentsAvailable.Remove(1); //Removes left Third
        }

        if (!boardButtonsAvailable.Contains(1)
            && !boardButtonsAvailable.Contains(6)
            && !boardButtonsAvailable.Contains(11)
            && !boardButtonsAvailable.Contains(16)
            && !boardButtonsAvailable.Contains(21)
            && !boardButtonsAvailable.Contains(26))
        {
            wheelSegmentsAvailable.Remove(2); //Removes $400 / $800 Only
        }

        if (!boardButtonsAvailable.Contains(10)
            && !boardButtonsAvailable.Contains(11)
            && !boardButtonsAvailable.Contains(12)
            && !boardButtonsAvailable.Contains(13)
            && !boardButtonsAvailable.Contains(14)
            && !boardButtonsAvailable.Contains(15)
            && !boardButtonsAvailable.Contains(16)
            && !boardButtonsAvailable.Contains(17)
            && !boardButtonsAvailable.Contains(18)
            && !boardButtonsAvailable.Contains(19))
        {
            wheelSegmentsAvailable.Remove(3); //Removes middle Third
        }

        if (!boardButtonsAvailable.Contains(2)
            && !boardButtonsAvailable.Contains(7)
            && !boardButtonsAvailable.Contains(12)
            && !boardButtonsAvailable.Contains(17)
            && !boardButtonsAvailable.Contains(22)
            && !boardButtonsAvailable.Contains(27))
        {
            wheelSegmentsAvailable.Remove(4); //Removes $600 / $1200 Only
        }

        if (!boardButtonsAvailable.Contains(20)
            && !boardButtonsAvailable.Contains(21)
            && !boardButtonsAvailable.Contains(22)
            && !boardButtonsAvailable.Contains(23)
            && !boardButtonsAvailable.Contains(24)
            && !boardButtonsAvailable.Contains(25)
            && !boardButtonsAvailable.Contains(26)
            && !boardButtonsAvailable.Contains(27)
            && !boardButtonsAvailable.Contains(28)
            && !boardButtonsAvailable.Contains(29))
        {
            wheelSegmentsAvailable.Remove(5); //Removes right Third
        }

        if (!boardButtonsAvailable.Contains(3)
            && !boardButtonsAvailable.Contains(8)
            && !boardButtonsAvailable.Contains(13)
            && !boardButtonsAvailable.Contains(18)
            && !boardButtonsAvailable.Contains(23)
            && !boardButtonsAvailable.Contains(28))
        {
            wheelSegmentsAvailable.Remove(6); //Removes $800 / $1600 Only
        }

        if (!boardButtonsAvailable.Contains(0)
            && !boardButtonsAvailable.Contains(1)
            && !boardButtonsAvailable.Contains(2)
            && !boardButtonsAvailable.Contains(3)
            && !boardButtonsAvailable.Contains(4)
            && !boardButtonsAvailable.Contains(5)
            && !boardButtonsAvailable.Contains(6)
            && !boardButtonsAvailable.Contains(7)
            && !boardButtonsAvailable.Contains(8)
            && !boardButtonsAvailable.Contains(9)
            && !boardButtonsAvailable.Contains(10)
            && !boardButtonsAvailable.Contains(11)
            && !boardButtonsAvailable.Contains(12)
            && !boardButtonsAvailable.Contains(13)
            && !boardButtonsAvailable.Contains(14))
        {
            wheelSegmentsAvailable.Remove(7); //Removes left Half
        }

        if (!boardButtonsAvailable.Contains(4)
            && !boardButtonsAvailable.Contains(9)
            && !boardButtonsAvailable.Contains(14)
            && !boardButtonsAvailable.Contains(19)
            && !boardButtonsAvailable.Contains(24)
            && !boardButtonsAvailable.Contains(29))
        {
            wheelSegmentsAvailable.Remove(8); //Removes $1000 / $2000 Only
        }

        if (!boardButtonsAvailable.Contains(15)
            && !boardButtonsAvailable.Contains(16)
            && !boardButtonsAvailable.Contains(17)
            && !boardButtonsAvailable.Contains(18)
            && !boardButtonsAvailable.Contains(19)
            && !boardButtonsAvailable.Contains(20)
            && !boardButtonsAvailable.Contains(21)
            && !boardButtonsAvailable.Contains(22)
            && !boardButtonsAvailable.Contains(23)
            && !boardButtonsAvailable.Contains(24)
            && !boardButtonsAvailable.Contains(25)
            && !boardButtonsAvailable.Contains(26)
            && !boardButtonsAvailable.Contains(27)
            && !boardButtonsAvailable.Contains(28)
            && !boardButtonsAvailable.Contains(29))
        {
            wheelSegmentsAvailable.Remove(9); //Removes right Half
        }

        AssignWheelSegments();
    }

    void AssignWheelSegments()
    {
        List<int> currentWheelSegments = new List<int>();

        while (currentWheelSegments.Count < 10)
        {
            for (int i = 0; i < wheelSegmentsAvailable.Count; i++)
            {
                currentWheelSegments.Add(wheelSegmentsAvailable[i]);

                if (currentWheelSegments.Count == 10) break;

                //We add either a left third, middle third or right third if we have a currentWheelSegments count remaining
                //and we haven't removed an available wheelSegment
                if (currentWheelSegments.Count == 9 && wheelSegmentsAvailable.Count == 9) 
                {
                    int randomSelection = Random.Range(3, 6);
                    currentWheelSegments.Add(wheelSegmentsAvailable[randomSelection]);
                    break;
                }
            }
        }

        for (int i = 0; i < wheelSegments.Count; i++ )
        {
            wheelSegments[i].SetCurrentSegment(currentWheelSegments[i]);
            wheelSegments[i].DisplaySegmentName();
        }
    }

    public void CheckIfSinglesAreDone()
    {
        if (boardButtonsAvailable.Count <= 1)
        {
            boardButtonsAvailable = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
            wheelSegmentsAvailable = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (WheelSegment wheelSegment in wheelSegments) { wheelSegment.SwitchToDoubleSegments(); }

            gameManager.SwitchCluesToDoubles();
        }
    }
}
