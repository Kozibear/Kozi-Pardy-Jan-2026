using UnityEngine;

namespace KoziPardy.Core
{
    public class FinalClueControl : MonoBehaviour
    {
        [Header("FinalClueMovement")]
        [SerializeField] FinalClueMovement finalClueMovement;

        [Header("Other References")]
        [SerializeField] SplashTitleButton splashTitleButton;
        [SerializeField] SplashButtonCanvasControl splashButtonCanvasControl;
        [SerializeField] VignetteFader vignetteFader;

        private BoardClueMovement childBoardClueMovement;

        public void SetBoardClueChild(GameObject childObject)
        {
            childObject.transform.parent = transform;
            childBoardClueMovement = childObject.GetComponent<BoardClueMovement>();
            splashButtonCanvasControl.SetButtonsWhenClueisShowing();

        }

        public void MoveOutNormalClue()
        {
            if (childBoardClueMovement != null)
            {
                childBoardClueMovement.MoveOutClueFinal();
                vignetteFader.StartVigentteFadeOut(0);
            }
        }

        public void BringInFinalClue()
        {
            finalClueMovement.MoveFinalClueIn();
            FadeInVignette(0.4f);
        }

        public void FinalClueIsInPlace()
        {
            splashButtonCanvasControl.SetButtonsWhenClueisShowing();
        }

        public void FinalClueBackButtonPressed()
        {
            if (!finalClueMovement.GetHasBeenRotated())
            {
                finalClueMovement.PullFinalClueBack();
            }
            else
            {
                finalClueMovement.MoveFinalClueOut();
                FadeOutVignette(0.4f);
            }

        }

        public void FadeInVignette(float speed) { vignetteFader.StartVigentteFadeIn(speed); }

        public void FadeOutVignette(float speed) { vignetteFader.StartVigentteFadeOut(speed); }


        public void ClueIsMovingOut()
        {
            splashTitleButton.gameObject.GetComponent<SpriteFade>().FadeIn();
        }

        public void FinalClueIsMovedOut()
        {
            splashTitleButton.Interactable();
        }

    }
}