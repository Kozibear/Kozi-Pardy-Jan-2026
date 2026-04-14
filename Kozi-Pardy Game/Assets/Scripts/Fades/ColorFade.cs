using System;
using UnityEngine;

public abstract class ColorFade : MonoBehaviour
{
    [Header("Color")]
    [SerializeField] Color currentColor;
    [SerializeField] Color destinationColor;

    [Header("Fade Speeds")]
    [SerializeField] float fadeInSpeed = 1;
    [SerializeField] float fadeOutSpeed = 1;
    float percentageComplete = 0;

    [Header("Fade Thresholds")]
    public float fadeInThreshold = 1;
    public float fadeOutThreshold = 0;

    private bool fadeInBool = false;
    private bool fadeOutBool = false;
    private bool fadeToExactColorBool = false;
    private bool inTheMiddleOfFading = false;

    [Header("Exact Colors")]
    [SerializeField] Color exactDarkenColor;
    [SerializeField] Color exactBrightenColor;

    void Start()
    {
        currentColor = GetColor();
    }

    void Update()
    {
        if (fadeInBool)
        {
            FadeInColor();
        }

        if (fadeOutBool && currentColor.r > fadeOutThreshold)
        {
            FadeOutColor();
        }

        if (fadeToExactColorBool)
        {
            FadeToExactColor();
        }
    }

    private void FadeOutColor()
    {
        currentColor.r -= Time.deltaTime * fadeOutSpeed;
        currentColor.g -= Time.deltaTime * fadeOutSpeed;
        currentColor.b -= Time.deltaTime * fadeOutSpeed;

        SetColor(currentColor);

        if (currentColor.r <= fadeOutThreshold)
        {
            Color color = new Color(fadeOutThreshold, fadeOutThreshold, fadeOutThreshold, 1f);
            SetColor(color);

            inTheMiddleOfFading = false;
            fadeOutBool = false;
        }
    }

    private void FadeInColor()
    {
        currentColor.r += Time.deltaTime * fadeInSpeed;
        currentColor.g += Time.deltaTime * fadeInSpeed;
        currentColor.b += Time.deltaTime * fadeInSpeed;
        SetColor(currentColor);

        if (currentColor.r >= fadeInThreshold)
        {
            Color color = new Color(fadeInThreshold, fadeInThreshold, fadeInThreshold, 1f);
            SetColor(color);

            inTheMiddleOfFading = false;
            fadeInBool = false;
        }
    }

    private void FadeToExactColor()
    {
        percentageComplete += Time.deltaTime * fadeInSpeed;
        Color newColor = Color.Lerp(currentColor, destinationColor, percentageComplete);
        SetColor(newColor);

        if (currentColor.r >= fadeInThreshold)
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
            currentColor = GetColor();
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

    public void GradualBrightenToExactColor()
    {
        if (!inTheMiddleOfFading)
        {
            inTheMiddleOfFading = true;
            currentColor = GetColor();
            destinationColor = exactBrightenColor;
            fadeToExactColorBool = true;
        }
    }

    public void Darken()
    {
        if (!inTheMiddleOfFading)
        {
            inTheMiddleOfFading = true;
            currentColor = GetColor();
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