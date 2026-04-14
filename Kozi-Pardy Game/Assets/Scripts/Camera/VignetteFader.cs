using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace KoziPardy.Core
{
    public class VignetteFader : MonoBehaviour
    {
        [SerializeField] Volume volume;
        private Vignette myVignette;

        [Header("Fade Speeds")]
        [SerializeField] float fadeInSpeed = 1;
        [SerializeField] float fadeOutSpeed = 1;
        private float initialFadeInSpeed;
        private float initialFadeOutSpeed;

        [Header("Threshold")]
        [SerializeField] float intensityThreshold = 0.1f;
        private float currentIntensity = 0;

        private bool fadeIn = false;
        private bool fadeOut = false;


        void Start()
        {
            initialFadeInSpeed = fadeInSpeed;
            initialFadeOutSpeed = fadeOutSpeed;

            volume.profile.TryGet(out Vignette vignette);
            myVignette = vignette;

            if (GameSettings.naturalSceneSwitch == false)
            {
                myVignette.intensity.value = 0;
            }
        }

        void Update()
        {
            if (fadeIn) { CanFadeInVignette(); }

            if (fadeOut) { CanFadeOutVigentte(); }
        }

        private void CanFadeInVignette()
        {
            currentIntensity += Time.deltaTime * fadeInSpeed;
            myVignette.intensity.value = currentIntensity;

            if (myVignette.intensity.value >= intensityThreshold)
            {
                myVignette.intensity.value = intensityThreshold;
                fadeIn = false;
            }
        }

        private void CanFadeOutVigentte()
        {
            currentIntensity -= Time.deltaTime * fadeOutSpeed;
            myVignette.intensity.value = currentIntensity;

            if (myVignette.intensity.value <= 0)
            {
                myVignette.intensity.value = 0;
                fadeOut = false;
            }
        }

        public void StartVigentteFadeIn(float customSpeed)
        {
            if (customSpeed != 0) fadeInSpeed = customSpeed;
            else fadeInSpeed = initialFadeInSpeed;

            currentIntensity = myVignette.intensity.value;
            fadeIn = true;
            fadeOut = false;
        }

        public void StartVigentteFadeOut(float customSpeed)
        {
            if (customSpeed != 0) fadeOutSpeed = customSpeed;
            else fadeOutSpeed = initialFadeOutSpeed;

            currentIntensity = myVignette.intensity.value;
            fadeOut = true;
            fadeIn = false;
        }

        public void InstantVignetteFadeOut()
        {
            myVignette.intensity.value = 0;
        }
    }
}