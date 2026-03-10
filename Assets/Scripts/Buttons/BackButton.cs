using UnityEngine;
using System.Collections;

public class BackButton : ButtonSprite
{
    [Header("Scripts")]
    [SerializeField] ClueScreen clueScreen;
    [SerializeField] GameBoardManager gameManager;

    [Header("Back Arrow Transforms")]
    [Tooltip("The Back Arrow should start offscreen at the bottom, move to the lower-right corner, and then exit offscreen to the right")]
    [SerializeField] Vector3 OnscreenPosition;
    [SerializeField] Vector3 BelowOffscreenPosition;
    [SerializeField] Vector3 RightOffscreenPosition;
    [SerializeField] float arrowMovementSpeed;

    [Header("Delays")]
    [SerializeField] float arrowMoveInDelay;

    bool canMoveArrow = false;
    Vector3 arrowDestination;

    private void Start()
    {
        NotInteractable();
        transform.position = BelowOffscreenPosition;
    }

    private void Update()
    {
        float stepBackArrow = Time.deltaTime * arrowMovementSpeed;

        if (transform.localPosition != arrowDestination && canMoveArrow)
        {
            MoveArrowToDestination(stepBackArrow);
        }

        if (transform.localPosition == arrowDestination)
        {
            ArrowIsAtDestination();
        }
    }

    private void ArrowIsAtDestination()
    {
        canMoveArrow = false;
        if (transform.localPosition == RightOffscreenPosition) transform.localPosition = BelowOffscreenPosition;
    }

    private void MoveArrowToDestination(float stepBackArrow)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, arrowDestination, stepBackArrow);
    }

    public void CanMoveArrowIn()
    {
        StartCoroutine(WaitToBringInArrow());
    }

    IEnumerator WaitToBringInArrow()
    {
        yield return new WaitForSeconds(arrowMoveInDelay);

        Interactable();
        canMoveArrow = true;
        arrowDestination = OnscreenPosition;
    }

    void CanMoveArrowOut()
    {
        NotInteractable();
        canMoveArrow = true;
        arrowDestination = RightOffscreenPosition;
    }

    protected override void OnMouseDown()
    {
        if (interactable) { BackButtonClicked(); }
    }

    private void BackButtonClicked()
    {
        interactable = false;
        GetComponent<SpriteRenderer>().sprite = mouseDown;

        CanMoveArrowOut();
        //gameManager.BackButtonPressed();
    }
}
