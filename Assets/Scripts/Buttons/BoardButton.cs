using UnityEngine;

public class BoardButton : Button
{
    [Header("Scripts")]
    [SerializeField] GameManager gameManager;

    [Header("Button Number")]
    [SerializeField] int buttonNumber;

    [Header("Button Monetary Value")]
    [SerializeField] Sprite singleValue;
    [SerializeField] Sprite doubleValue;

    [Header("This Button's Color Fades")]
    [SerializeField] SpriteColorFade buttonColorFade;
    [SerializeField] SpriteColorFade numberColorFade;

    [Header("The Category's Color Fades")]
    [SerializeField] SpriteColorFade categoryTitleColorFade;
    [SerializeField] SpriteColorFade categoryBackgroundColorFade;

    private bool hasBeenClicked = false;
    private bool postClickState = false;
    private bool wasJustInteractable = false;

    private void Start()
    {
        InteractableFadeOut();
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = singleValue;
    }

    public int GetNumber() { return buttonNumber; }

    public bool GetHasBeenClicked() { return hasBeenClicked; }

    public bool WasJustInteractable() { return wasJustInteractable; }

    public void InteractableFadeIn()
    {
        if (!hasBeenClicked)
        {
            interactable = true;
            wasJustInteractable = true;

            buttonColorFade.Brighten();
            numberColorFade.Brighten();

            categoryTitleColorFade.Brighten();
            categoryBackgroundColorFade.Brighten();
        }
    }

    public void InteractableFadeOut()
    {
        if (!hasBeenClicked) //This is to prevent the button from being further, unecessarily darkened
        {
            interactable = false;

            buttonColorFade.Darken();
            numberColorFade.Darken();
        }

        wasJustInteractable = false;
        categoryTitleColorFade.Darken();
        categoryBackgroundColorFade.Darken();
    }

    public void SoftNotInteractable()
    {
        interactable = false;
        GetComponent<SpriteRenderer>().sprite = regular;

        if (postClickState)
        {
            buttonColorFade.InstantDarken();
            numberColorFade.InstantDarken();
            postClickState = false;
        }
    }

    public void DoubleBoardReset()
    {
        hasBeenClicked = false;

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = doubleValue;
    }

    protected override void OnMouseDown()
    {
        if (interactable)
        {
            postClickState = true;
            hasBeenClicked = true;

            interactable = false;
            GetComponent<SpriteRenderer>().sprite = mouseDown;

            gameManager.OnClueSelected(buttonNumber);
        }
    }

    public bool GetInteractable() { return interactable; }
}
