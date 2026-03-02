using TMPro;
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
        [SerializeField] ParticleSystem streaksParticleSystem;
        ParticleSystem.MainModule streaksMainModule;
        ParticleSystem.ColorOverLifetimeModule streaksLifeTimeModule;

        [SerializeField] ParticleSystem smokeParticleSystem;
        ParticleSystem.MainModule smokeMainModule;
        ParticleSystem.ColorOverLifetimeModule smokeLifeTimeModule;

        [Header("Streaks Colors")]
        [SerializeField] Color streaksBlueColor;
        [SerializeField] Color streaksOrangeColor;
        [SerializeField] Color streaksPurpleColor;

        [Header("Streaks Over Lifetime Gradients")]
        [SerializeField] Gradient streaksBlueGradient;
        [SerializeField] Gradient streaksOrangeGradient;
        [SerializeField] Gradient streaksPurpleGradient;

        [Header("Smoke Colors")]
        [SerializeField] Color smokeBlueColor;
        [SerializeField] Color smokeOrangeColor;
        [SerializeField] Color smokePurpleColor;

        [Header("Smoke Over Lifetime Gradients")]
        [SerializeField] Gradient smokeBlueGradient;
        [SerializeField] Gradient smokeOrangeGradient;
        [SerializeField] Gradient smokePurpleGradient;

        private float elapsedTimeInSeconds = 0;

        private Color currentSpriteColor;
        private Color currentStreakColor;
        private Color currentSmokeColor;

        private Color nextSpriteColor;
        private Color nextStreakColor;
        private Color nextSmokeColor;

        private bool canChangeColor = false;

        private void Start()
        {
            backgroundColorSprite.color = darkBlueColor;
            currentSpriteColor = darkBlueColor;

            streaksMainModule = streaksParticleSystem.main;
            streaksMainModule.startColor = streaksBlueColor;
            streaksLifeTimeModule = streaksParticleSystem.colorOverLifetime;
            streaksLifeTimeModule.color = streaksBlueGradient;
            currentStreakColor = streaksBlueColor;

            smokeMainModule = smokeParticleSystem.main;
            smokeMainModule.startColor = smokeBlueColor;
            smokeLifeTimeModule = smokeParticleSystem.colorOverLifetime;
            smokeLifeTimeModule.color = smokeBlueGradient;
            currentSmokeColor = smokeBlueColor;
        }

        void Update()
        {
            if (canChangeColor)
            {
                elapsedTimeInSeconds += Time.deltaTime * 1.2f;
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
                    nextSpriteColor = darkOrangeColor;

                    streaksMainModule.startColor = streaksOrangeColor;
                    smokeMainModule.startColor = smokeOrangeColor;
                    streaksLifeTimeModule.color = streaksOrangeGradient;
                    smokeLifeTimeModule.color = smokeOrangeGradient;
                    break;
                case GlobalColorState.Orange:
                    nextSpriteColor = darkPurpleColor;

                    streaksMainModule.startColor = streaksPurpleColor;
                    smokeMainModule.startColor = smokePurpleColor;
                    streaksLifeTimeModule.color = streaksPurpleGradient;
                    smokeLifeTimeModule.color = smokePurpleGradient;
                    break;
                case GlobalColorState.Purple:
                    nextSpriteColor = darkBlueColor;

                    streaksMainModule.startColor = streaksBlueColor;
                    smokeMainModule.startColor = smokeBlueColor;
                    streaksLifeTimeModule.color = streaksBlueGradient;
                    smokeLifeTimeModule.color = smokeBlueGradient;
                    break;
            }

            currentSpriteColor = backgroundColorSprite.color;

            elapsedTimeInSeconds = 0;
            canChangeColor = true;
        }
    }
}