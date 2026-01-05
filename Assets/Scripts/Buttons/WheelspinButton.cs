using System.Collections.Generic;
using UnityEngine;

public class WheelspinButton : Button
{
    [Header("Scripts")]
    [SerializeField] WheelSpin wheelSpin;
    [SerializeField] GameManager gameManager;

    [Header("Pulsing")]
    [SerializeField] Color minPulse;
    [SerializeField] Color maxPulse;
    [SerializeField] float pulseSpeed;

    [Header("ColorFades")]
    [SerializeField] TextColorFade wheelSpinText;
    [SerializeField] SpriteColorFade wheelSpinButton;
    [SerializeField] SpriteColorFade wheelSpinButtonDesk;

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
            if (pulsingUp)
            {
                tempColor.r += Time.deltaTime * pulseSpeed;
                tempColor.g += Time.deltaTime * pulseSpeed;
                tempColor.b += Time.deltaTime * pulseSpeed;

                gameObject.GetComponent<SpriteRenderer>().color = tempColor;

                if (tempColor.r >= maxPulse.r)
                {
                    pulsingUp = false;
                }
            }
            else
            {
                tempColor.r -= Time.deltaTime * pulseSpeed/1.1f;
                tempColor.g -= Time.deltaTime * pulseSpeed/1.1f;
                tempColor.b -= Time.deltaTime * pulseSpeed/1.1f;

                gameObject.GetComponent<SpriteRenderer>().color = tempColor;

                if (tempColor.r <= minPulse.r)
                {
                    pulsingUp = true;
                }
            }
        }
    }

    protected override void OnMouseDown()
    {
        if (interactable)
        {
            NotInteractable();
            gameManager.ShowWheel();

            tempColor = gameObject.GetComponent<SpriteRenderer>().color;
            pulsingUp = true;

            wheelSpinText.Darken();
            wheelSpinButton.Darken();
            wheelSpinButtonDesk.Darken();
        }
    }

    public void BrightenAndInteractable()
    {
        Interactable();
        wheelSpinText.InstantBrighten();
        wheelSpinButtonDesk.InstantBrighten();
    }
}
