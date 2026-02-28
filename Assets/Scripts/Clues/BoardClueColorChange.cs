using UnityEngine;

namespace KoziPardy.ColorManagement
{
    public class BoardClueColorChange : MonoBehaviour
    {
        public enum ColorValue { Bright, Dark };

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

        [Header("The Cube's Mesh Renderer")]
        [SerializeField] MeshRenderer meshRenderer;

        private float elapsedTimeInSeconds = 0;
        private float desiredDurationForOldClue = 0.6f;
        float percentageComplete = 0;

        private Color currentColor;
        private Color nextColor;
        private bool canChangeColor = false;
        private bool oldClueFading = false;

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

                if (!oldClueFading) percentageComplete = elapsedTimeInSeconds / GlobalColorManager.desiredDurationInSeconds;
                else percentageComplete = elapsedTimeInSeconds / desiredDurationForOldClue;

                GradualColorChange(percentageComplete, nextColor);

                if (percentageComplete >= 1)
                {
                    canChangeColor = false;
                    oldClueFading = false;
                }
            }
        }

        public void GradualColorChange(float percentageComplete, Color newColor)
        {
            meshRenderer.material.color = Color.Lerp(currentColor, newColor, percentageComplete);
        }

        public void StartNexColorShift()
        {
            switch (GlobalColorManager.globalColorState)
            {
                case GlobalColorState.Blue:
                    nextColor = orangeDarkened;
                    break;
                case GlobalColorState.Orange:
                    nextColor = purpleDarkened;
                    break;
                case GlobalColorState.Purple:
                    nextColor = blueDarkened;
                    break;
            }

            currentColor = meshRenderer.material.color;

            elapsedTimeInSeconds = 0;
            canChangeColor = true;
        }

        public void ColorBrightenDarken(ColorValue colorValue, bool gradual, bool isOld)
        {
            switch (GlobalColorManager.globalColorState, colorValue)
            {
                case (GlobalColorState.Blue, ColorValue.Bright):
                    if (gradual) nextColor = blueHighlight;
                    else meshRenderer.material.color = blueHighlight;
                    break;
                case (GlobalColorState.Blue, ColorValue.Dark):
                    if (gradual) nextColor = blueDarkened;
                    else meshRenderer.material.color = blueDarkened;
                    break;
                case (GlobalColorState.Orange, ColorValue.Bright):
                    if (gradual) nextColor = orangeHighlight;
                    else meshRenderer.material.color = orangeHighlight;
                    break;
                case (GlobalColorState.Orange, ColorValue.Dark):
                    if (gradual) nextColor = orangeDarkened;
                    else meshRenderer.material.color = orangeDarkened;
                    break;
                case (GlobalColorState.Purple, ColorValue.Bright):
                    if (gradual) nextColor = purpleHighlight;
                    else meshRenderer.material.color = purpleHighlight;
                    break;
                case (GlobalColorState.Purple, ColorValue.Dark):
                    if (gradual) nextColor = purpleDarkened;
                    else meshRenderer.material.color = purpleDarkened;
                    break;
            }

            currentColor = meshRenderer.material.color;

            if (isOld) oldClueFading = true;

            if (gradual)
            {
                elapsedTimeInSeconds = 0;
                canChangeColor = true;
            }
        }
    }
}