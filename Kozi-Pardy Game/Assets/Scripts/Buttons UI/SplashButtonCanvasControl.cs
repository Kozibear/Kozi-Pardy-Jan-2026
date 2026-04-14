using UnityEngine;
using UnityEngine.UI;

namespace KoziPardy.Core
{
    public class SplashButtonCanvasControl : MonoBehaviour
    {
        [Header("UI Buttons")]
        [SerializeField] Button backButton;
        [SerializeField] Button rightSFXButton;
        [SerializeField] Button wrongSFXButton;

        [Header("Debug UI Buttons")]
        [SerializeField] GameObject showDebugOptionsButton;

        [Header("Splash Title Button")]
        [SerializeField] SplashTitleButton splashTitleButton;

        public void SetButtonsWhenClueisShowing()
        {
            backButton.gameObject.SetActive(true);
            rightSFXButton.gameObject.SetActive(true);
            wrongSFXButton.gameObject.SetActive(true);

            showDebugOptionsButton.SetActive(false);
            splashTitleButton.NotInteractable();
        }
    }
}