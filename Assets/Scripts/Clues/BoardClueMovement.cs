using NUnit.Framework;
using UnityEngine;

public class BoardClueMovement : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] Vector3 frontPosition;
    [SerializeField] Vector3 originalPosition;

    [Header("Rotations")]
    [SerializeField] Vector3 frontRotation;
    [SerializeField] Vector3 originalRotation;

    [Header("Speeds")]
    [SerializeField] float moveSpeed = 1;

    private Vector3 destinationPosition;
    private bool canMoveClue = false;

    void Start()
    {
        originalPosition = transform.position;
        destinationPosition = transform.position;
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

    private void ArrivedAtDestination()
    {
        canMoveClue = false;

        if (transform.localPosition == frontPosition)
        {
            GetComponent<BoardClueMediaManager>().HideFrontText();

            //gameManager.HardDeActivationAndHidePointNumber();
            //backButtonInvisible.Interactable();
            //wheelSpin.CheckIfSinglesAreDone();
        }
        if (transform.localPosition == originalPosition)
        {
            GetComponent<BoardClueMediaManager>().ClueCleanup();

            //gameManager.BoardBeforeWheelSpin();
            //transform.localPosition = moveOffscreenBelowPosition;
            //if (ClueImagesSpawnPoint.transform.childCount > 0) Destroy(ClueImagesSpawnPoint.transform.GetChild(0).gameObject);
        }
    }

    [ContextMenu("Move In Clue")]
    public void MoveInClue()
    {
        canMoveClue = true;
        destinationPosition = frontPosition;
        GetComponent<BoardClueMediaManager>().CluePrep();
    }

    [ContextMenu("Move Out Clue")]
    public void MoveOutClue()
    {
        canMoveClue = true;
        destinationPosition = originalPosition;
    }
}
