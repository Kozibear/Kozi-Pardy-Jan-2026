using UnityEngine;

public class ClueScreen : MonoBehaviour
{
    [Header("Moving in")]
    [SerializeField] Vector3 moveInPosition;

    [Header("Moving Out")]
    [SerializeField] Vector3 moveOffscreenAbovePosition;
    [SerializeField] Vector3 moveOffscreenBelowPosition;

    [SerializeField] float moveSpeed;

    [Header("Reference Objects")]
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject ClueImagesSpawnPoint;
    [SerializeField] BackButton backButton;
    [SerializeField] BackButtonInvisible backButtonInvisible;
    [SerializeField] WheelSpin wheelSpin;

    private bool canMoveClue = false;
    private Vector3 destinationPosition;

    private void Start()
    {
        destinationPosition = transform.position;
    }

    void Update()
    {
        float step = Time.deltaTime * moveSpeed;

        if (transform.localPosition != destinationPosition && canMoveClue)
        {
            MoveToDestination(step);
        }

        if (canMoveClue && transform.localPosition == destinationPosition)
        {
            ArrivedAtDestination();
        }
    }

    private void ArrivedAtDestination()
    {
        canMoveClue = false;

        if (transform.localPosition == moveInPosition)
        {
            gameManager.HardDeActivationAndHidePointNumber();

            backButtonInvisible.Interactable();
            //backButton.CanMoveArrowIn();

            wheelSpin.CheckIfSinglesAreDone();
        }
        if (transform.localPosition == moveOffscreenAbovePosition)
        {
            gameManager.BoardBeforeWheelSpin();
            transform.localPosition = moveOffscreenBelowPosition;
            if (ClueImagesSpawnPoint.transform.childCount > 0) Destroy(ClueImagesSpawnPoint.transform.GetChild(0).gameObject);
        }
    }

    private void MoveToDestination(float step)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destinationPosition, step);
        //Insert a bit about resizing it here
    }

    public void MoveInClue()
    {
        canMoveClue = true;
        destinationPosition = moveInPosition;
    }

    public void MoveOutClue()
    {
        canMoveClue = true;
        destinationPosition = moveOffscreenAbovePosition;
    }
}
