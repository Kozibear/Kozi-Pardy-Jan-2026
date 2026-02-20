using UnityEngine;

public abstract class Fade : MonoBehaviour
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

    void Start()
    {
        tempColor = GetColor();
    }

    void Update()
    {
        if (fadeInBool) { FadeInGraphic(); }

        if (fadeOutBool) { FadeOutGraphic(); }
    }

    private void FadeOutGraphic()
    {
        tempColor.a -= Time.deltaTime * fadeOutSpeed;
        SetColor(tempColor);

        if (tempColor.a <= fadeOutThreshold) fadeOutBool = false;
    }

    private void FadeInGraphic()
    {
        tempColor.a += Time.deltaTime * fadeInSpeed;
        SetColor(tempColor);

        if (tempColor.a >= fadeInThreshold) fadeInBool = false;
    }

    public void FadeIn()
    {
        tempColor = GetColor();
        fadeInBool = true;
    }

    public void FadeOut()
    {
        tempColor = GetColor();
        fadeOutBool = true;
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
