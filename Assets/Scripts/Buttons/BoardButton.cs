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

    //private ParticleSystem particleSys;

    private bool hasBeenClicked = false;
    private bool postClickState = false;
    private bool wasJustInteractable = false;

    private void Start()
    {
        InteractableFadeOut();
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = singleValue;
        //particleSys = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    public int GetNumber() { return buttonNumber; }

    public bool GetHasBeenClicked() { return hasBeenClicked; }

    public bool WasJustInteractable() { return wasJustInteractable; }

    public bool GetInteractable() { return interactable; }

    protected override void OnMouseEnter()
    {
        if (interactable)
        {
            GetComponent<SpriteRenderer>().sprite = mouseOver;
            //particleSys.Play();
        }
    }

    protected override void OnMouseExit()
    {
        if (interactable) { GetComponent<SpriteRenderer>().sprite = regular; }
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
        if (!hasBeenClicked || postClickState) //This is to prevent the button from being further, unecessarily darkened
        {
            interactable = false;
            postClickState = false;

            buttonColorFade.Darken();
            numberColorFade.Darken();
        }

        wasJustInteractable = false;
        categoryTitleColorFade.Darken();
        categoryBackgroundColorFade.Darken();
    }

    //We make all the buttons on the board non-interactive, but don't change their appearance
    //In previous versions, we made an exception and immediately darekened the button had just been clicked
    public void SoftNotInteractable()
    {
        interactable = false;
        GetComponent<SpriteRenderer>().sprite = regular;

        //if (postClickState)
        //{
        //    buttonColorFade.InstantDarken();
        //    numberColorFade.InstantDarken();
        //    postClickState = false;
        //}
    }

    public void DoubleBoardReset()
    {
        hasBeenClicked = false;

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = doubleValue;
    }

}
