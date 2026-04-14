using UnityEngine;

namespace KoziPardy.ColorManagement
{
    public class BoardSpriteColorChange : MonoBehaviour
    {
        [Header("Background Colors")]
        [SerializeField] Color blueColor;
        [SerializeField] Color orangeColor;
        [SerializeField] Color purpleColor;

        [Header("Background Color Sprite")]
        [SerializeField] SpriteRenderer backgroundColorSprite;

        private float elapsedTimeInSeconds = 0;

        private Color currentSpriteColor;
        private Color nextSpriteColor;

        private bool canChangeColor = false;

        private void Start()
        {
            backgroundColorSprite.color = blueColor;
            currentSpriteColor = blueColor;
        }

        void Update()
        {
            if (canChangeColor)
            {
                elapsedTimeInSeconds += Time.deltaTime;
                float percentageComplete = elapsedTimeInSeconds / GlobalColorManager.desiredDurationInSeconds;

                GradualColorChange(percentageComplete, nextSpriteColor);

                if (percentageComplete >= 1) canChangeColor = false;
            }
        }

        public void GradualColorChange(float percentageComplete, Color newSpriteColor)
        {
            backgroundColorSprite.color = Color.Lerp(currentSpriteColor, newSpriteColor, percentageComplete);
        }

        public void StartGradualColorChange()
        {
            switch (GlobalColorManager.globalColorState)
            {
                case GlobalColorState.Blue:
                    nextSpriteColor = orangeColor;
                    break;
                case GlobalColorState.Orange:
                    nextSpriteColor = purpleColor;
                    break;
                case GlobalColorState.Purple:
                    nextSpriteColor = blueColor;
                    break;
            }

            currentSpriteColor = backgroundColorSprite.color;

            elapsedTimeInSeconds = 0;
            canChangeColor = true;
        }
    }
}