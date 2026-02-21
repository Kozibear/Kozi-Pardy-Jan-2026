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

    [Header("Fade Listener")]
    [SerializeField] GameObject fadeListenerImplementer = null;
    IFadeListener fadeListener = null;

    private bool fadeInBool = false;
    private bool fadeOutBool = false;

    private int pulses = 0;

    void Start()
    {
        tempColor = GetColor();

        if (fadeListenerImplementer != null) fadeListener = fadeListenerImplementer.GetComponent<IFadeListener>();
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

        if (tempColor.a <= fadeOutThreshold)
        {
            fadeOutBool = false;
            if (fadeListener != null)
            {
                fadeListenerImplementer.GetComponent<IFadeListener>().FadeOutComplete();
            }

            if (pulses <= 1) pulses = 0;
            else if (pulses > 1)
            {
                pulses--;
                fadeInBool = true;
            }
        }
    }

    private void FadeInGraphic()
    {
        tempColor.a += Time.deltaTime * fadeInSpeed;
        SetColor(tempColor);

        if (tempColor.a >= fadeInThreshold)
        {
            fadeInBool = false;
            if (fadeListener != null)
            {
                fadeListenerImplementer.GetComponent<IFadeListener>().FadeInComplete();
            }

            if (pulses > 0) fadeOutBool = true;
        }
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

    public void Pulse(int pulsesAmount)
    {
        tempColor = GetColor();
        fadeInBool = true;
        pulses = pulsesAmount;
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
