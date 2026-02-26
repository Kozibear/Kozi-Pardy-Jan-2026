using UnityEngine;

namespace KoziPardy.ColorManagement
{
    public class BackgroundColorChange : MonoBehaviour
    {
        [Header("Background Colors")]
        [SerializeField] Color darkBlueColor;
        [SerializeField] Color darkOrangeColor;
        [SerializeField] Color darkPurpleColor;

        [Header("Background Color Sprite")]
        [SerializeField] SpriteRenderer backgroundColorSprite;

        [Header("Background Particle System")]
        [SerializeField] ParticleSystem myParticleSystem;
        ParticleSystem.ColorOverLifetimeModule colorModule;

        [Header("Particle System Colors")]
        [SerializeField] Color lightBlueColor;
        [SerializeField] Color lightOrangeColor;
        [SerializeField] Color lightPurpleColor;

        private float elapsedTimeInSeconds = 0;

        private Color currentSpriteColor;
        private Color currentParticleSysColor;

        private Color nextSpriteColor;
        private Color nextParticleSysColor;

        private bool canChangeColor = false;

        private void Start()
        {
            backgroundColorSprite.color = darkBlueColor;
            currentSpriteColor = darkBlueColor;

            colorModule = myParticleSystem.colorOverLifetime;
            colorModule.color = lightBlueColor;
            currentParticleSysColor = lightBlueColor;
        }

        void Update()
        {
            if (canChangeColor)
            {
                elapsedTimeInSeconds += Time.deltaTime;
                float percentageComplete = elapsedTimeInSeconds / GlobalColorManager.desiredDurationInSeconds;

                GradualColorChange(percentageComplete, nextSpriteColor, nextParticleSysColor);

                if (percentageComplete >= 1) canChangeColor = false;
            }
        }

        public void GradualColorChange(float percentageComplete, Color newSpriteColor, Color newParticleSysColor)
        {
            backgroundColorSprite.color = Color.Lerp(currentSpriteColor, newSpriteColor, percentageComplete);
            colorModule.color = Color.Lerp(currentParticleSysColor, newParticleSysColor, percentageComplete);
        }

        public void StartGradualColorChange()
        {
            switch (GlobalColorManager.globalColorState)
            {
                case GlobalColorState.Blue:
                    nextSpriteColor = darkOrangeColor;
                    nextParticleSysColor = lightOrangeColor;
                    break;
                case GlobalColorState.Orange:
                    nextSpriteColor = darkPurpleColor;
                    nextParticleSysColor = lightPurpleColor;
                    break;
                case GlobalColorState.Purple:
                    nextSpriteColor = darkBlueColor;
                    nextParticleSysColor = lightBlueColor;
                    break;
            }

            currentSpriteColor = backgroundColorSprite.color;
            currentParticleSysColor = colorModule.color.color;

            elapsedTimeInSeconds = 0;
            canChangeColor = true;
        }
    }
}