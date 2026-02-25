using UnityEngine;

public class BoardClueColorChange : MonoBehaviour
{
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

    [Header("The Cube's Material")]
    [SerializeField] Material clueCubeMaterial;

    private Color currentColor;
    private bool canChangeColor = true;

    private void Start()
    {
        clueCubeMaterial.color = blueDarkened;
        currentColor = clueCubeMaterial.color;
    }

    void Update()
    {
        if (canChangeColor)
        {
            //if the game is running at 30 fps, deltatime adds 1/30th of a second; if it's running at 60 fps it add 1/60 of a second, etc.
            //this means that no matter what the framerate is, after one second, time.DeltaTime will have added exactly one second of time
            elapsedTimeInSeconds += Time.deltaTime;
            float percentageComplete = elapsedTimeInSeconds / desiredDurationInSeconds;

            GradualColorChange(percentageComplete);
        }
    }

    public void GradualColorChange(float percentageComplete)
    {
        //clueCubeMaterial.color = Color.Lerp(currentColor, blueHighlight, percentageComplete);
    }

    public void StartGradualColorChange()
    {
        elapsedTimeInSeconds = 0;
        currentColor = clueCubeMaterial.color;
        canChangeColor = true;
    }

    public void InstantColorChange()
    {

    }
}
