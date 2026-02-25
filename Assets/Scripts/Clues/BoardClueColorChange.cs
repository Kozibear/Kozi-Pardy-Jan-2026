using UnityEngine;

public class BoardClueColorChange : MonoBehaviour
{
    public enum ColorChoices { BlueHighlight, OrangeHighlight, PurpleHighlight, BlueDarkened, OrangeDarkened, PurpleDarkened };

    [Header("Highlight Colors")]
    [SerializeField] Color blueHighlight;
    [SerializeField] Color orangeHighlight;
    [SerializeField] Color purpleHighlight;

    [Header("Darkened Colors")]
    [SerializeField] Color blueDarkened;
    [SerializeField] Color orangeDarkened;
    [SerializeField] Color purpleDarkened;

    [Header("Other Colors")]
    [SerializeField] Color resetWhite;

    [Header("Speed")]
    [SerializeField] float desiredDurationInSeconds = 2;
    private float elapsedTimeInSeconds = 0;

    [Header("The Cube's Mesh Renderer")]
    [SerializeField] MeshRenderer meshRenderer;

    private Color currentColor;
    private Color nextColor;
    private bool canChangeColor = false;

    private void Start()
    {
        currentColor = blueDarkened;
        meshRenderer.material.color = currentColor;
    }

    void Update()
    {
        if (canChangeColor)
        {
            //if the game is running at 30 fps, deltatime adds 1/30th of a second; if it's running at 60 fps it add 1/60 of a second, etc.
            //this means that no matter what the framerate is, after one second, time.DeltaTime will have added exactly one second of time
            elapsedTimeInSeconds += Time.deltaTime;
            float percentageComplete = elapsedTimeInSeconds / desiredDurationInSeconds;

            GradualColorChange(percentageComplete, nextColor);
        }
    }

    public void GradualColorChange(float percentageComplete, Color newColor)
    {
        meshRenderer.material.color = Color.Lerp(currentColor, newColor, percentageComplete);
    }

    public void StartGradualColorChange(ColorChoices colorChoice)
    {
        switch (colorChoice)
        {
            case ColorChoices.BlueHighlight:
                nextColor = blueHighlight;
                break;
            case ColorChoices.OrangeHighlight:
                nextColor = orangeHighlight;
                break;
            case ColorChoices.PurpleHighlight:
                nextColor = purpleHighlight;
                break;
            case ColorChoices.BlueDarkened:
                nextColor = blueDarkened;
                break;
            case ColorChoices.OrangeDarkened:
                nextColor = orangeDarkened;
                break;
            case ColorChoices.PurpleDarkened:
                nextColor = purpleDarkened;
                break;
        }

        currentColor = meshRenderer.material.color;

        elapsedTimeInSeconds = 0;
        canChangeColor = true;
    }

    public void InstantColorDarken()
    {
        if (currentColor == blueHighlight)
        {
            meshRenderer.material.color = blueDarkened;
        }
        else if (currentColor == orangeHighlight)
        {
            meshRenderer.material.color = orangeDarkened;
        }
        else if (currentColor == purpleHighlight)
        {
            meshRenderer.material.color = purpleDarkened;
        }

        currentColor = meshRenderer.material.color;
    }

    public void InstantColorHighlight()
    {
        if (currentColor == blueDarkened)
        {
            meshRenderer.material.color = blueHighlight;
        }
        else if (currentColor == orangeDarkened)
        {
            meshRenderer.material.color = orangeHighlight;
        }
        else if (currentColor == purpleDarkened)
        {
            meshRenderer.material.color = purpleHighlight;
        }

        currentColor = meshRenderer.material.color;
    }
}
