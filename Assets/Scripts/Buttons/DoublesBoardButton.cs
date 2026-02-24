using UnityEngine;

public class DoublesBoardButton : ButtonSprite
{
    [Header("Scripts")]
    [SerializeField] GameManager gameManager;
    [SerializeField] SpriteFade blackBackground;
    [SerializeField] CategoryReveals categoryReveals;

    [Header("Pulsing")]
    [SerializeField] Color minPulse;
    [SerializeField] Color maxPulse;
    [SerializeField] float pulseSpeed;

    [Header("ColorFades")]
    [SerializeField] TextColorFade doublesBoardText;
    [SerializeField] SpriteColorFade doublesBoardButton;
    [SerializeField] SpriteColorFade doublesBoardButtonDesk;

    private bool pulsingUp = false;
    private Color tempColor;

    private void Awake()
    {
        NotInteractable();
        tempColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (interactable)
        {
            if (pulsingUp) { PulseUpButton(); }
            else { PulseDownButton(); }
        }
    }

    private void PulseDownButton()
    {
        tempColor.r -= Time.deltaTime * pulseSpeed / 1.1f;
        tempColor.g -= Time.deltaTime * pulseSpeed / 1.1f;
        tempColor.b -= Time.deltaTime * pulseSpeed / 1.1f;

        gameObject.GetComponent<SpriteRenderer>().color = tempColor;

        if (tempColor.r <= minPulse.r) { pulsingUp = true; }
    }

    private void PulseUpButton()
    {
        tempColor.r += Time.deltaTime * pulseSpeed;
        tempColor.g += Time.deltaTime * pulseSpeed;
        tempColor.b += Time.deltaTime * pulseSpeed;

        gameObject.GetComponent<SpriteRenderer>().color = tempColor;

        if (tempColor.r >= maxPulse.r) { pulsingUp = false; }
    }

    protected override void OnMouseDown()
    {
        if (interactable)
        {
            NotInteractable();
            
            tempColor = gameObject.GetComponent<SpriteRenderer>().color;
            pulsingUp = true;

            doublesBoardText.Darken();
            doublesBoardButton.Darken();
            doublesBoardButtonDesk.Darken();

            blackBackground.gameObject.SetActive(true);
            blackBackground.SetFadeSpeeds(1.75f);
            blackBackground.SetFadeInThreshold(1);
            blackBackground.FadeIn();

            categoryReveals.gameObject.SetActive(true);
            categoryReveals.ShowDoublesCategories();
        }
    }

    public void BrightenAndInteractable()
    {
        Interactable();
        doublesBoardText.InstantBrighten();
        doublesBoardButtonDesk.InstantBrighten();
    }
}
