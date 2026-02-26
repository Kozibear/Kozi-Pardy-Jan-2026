using System;
using UnityEngine;

public abstract class ColorFade : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] Color tempColor;

    [Header("Fade Speeds")]
    [SerializeField] float fadeInSpeed = 1;
    [SerializeField] float fadeOutSpeed = 1;

    [Header("Fade Thresholds")]
    public float fadeInThreshold = 1;
    public float fadeOutThreshold = 0;

    private bool fadeInBool = false;
    private bool fadeOutBool = false;
    private bool inTheMiddleOfFading = false;

    [Header("Exact Colors")]
    [SerializeField] Color exactDarkenColor;
    [SerializeField] Color exactBrightenColor;

    void Start()
    {
        tempColor = GetColor();
    }

    void Update()
    {
        if (fadeInBool)
        {
            FadeInColor();
        }

        if (fadeOutBool && tempColor.r > fadeOutThreshold)
        {
            FadeOutColor();
        }
    }

    private void FadeOutColor()
    {
        tempColor.r -= Time.deltaTime * fadeOutSpeed;
        tempColor.g -= Time.deltaTime * fadeOutSpeed;
        tempColor.b -= Time.deltaTime * fadeOutSpeed;

        SetColor(tempColor);

        if (tempColor.r <= fadeOutThreshold)
        {
            Color color = new Color(fadeOutThreshold, fadeOutThreshold, fadeOutThreshold, 1f);
            SetColor(color);

            inTheMiddleOfFading = false;
            fadeOutBool = false;
        }
    }

    private void FadeInColor()
    {
        tempColor.r += Time.deltaTime * fadeInSpeed;
        tempColor.g += Time.deltaTime * fadeInSpeed;
        tempColor.b += Time.deltaTime * fadeInSpeed;

        SetColor(tempColor);

        if (tempColor.r >= fadeInThreshold)
        {
            Color color = new Color(fadeInThreshold, fadeInThreshold, fadeInThreshold, 1f);
            SetColor(color);

            inTheMiddleOfFading = false;
            fadeInBool = false;
        }
    }

    public void Brighten()
    {
        if (!inTheMiddleOfFading)
        {
            inTheMiddleOfFading = true;
            tempColor = GetColor();
            fadeInBool = true;
        }
    }

    public void InstantBrighten()
    {
        if (!inTheMiddleOfFading)
        {
            Color color = new Color(fadeInThreshold, fadeInThreshold, fadeInThreshold, 1f);
            SetColor(color);
        }
    }

    public void InstantBrightenToExactColor()
    {
        if (!inTheMiddleOfFading)
        {
            SetColor(exactBrightenColor);
        }
    }

    public void Darken()
    {
        if (!inTheMiddleOfFading)
        {
            inTheMiddleOfFading = true;
            tempColor = GetColor();
            fadeOutBool = true;
        }
    }

    public void InstantDarken()
    {
        if (!inTheMiddleOfFading)
        {
            Color color = new Color(fadeOutThreshold, fadeOutThreshold, fadeOutThreshold, 1f);
            SetColor(color);
        }
    }

    public void InstantDarkenToExactColor()
    {
        if (!inTheMiddleOfFading)
        {
            SetColor(exactDarkenColor);
        }
    }

    public void SetFadeInThreshold(float newThreshold)
    {
        fadeInThreshold = newThreshold;
    }

    public void SetFadeSpeeds(float newSpeed)
    {
        fadeInSpeed = newSpeed;
        fadeOutSpeed = newSpeed;
    }

    protected abstract Color GetColor();

    protected abstract void SetColor(Color updatedColor);
}
