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

        [Header("Old Game Board Clue Stuff")]
        [SerializeField] GameObject oldBoardClueParent;
        private BoardClueMovement childBoardClueMovement;

        private bool finalClueAllowed = false;

        public void SetBoardClueChild(GameObject childObject)
        {
            splashTitleButton.NotInteractable();

            childObject.transform.parent = oldBoardClueParent.transform;
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

            splashTitleButton.Interactable();
        }

        public void BringInFinalClue()
        {
            finalClueAllowed = true;
            finalClueMovement.MoveFinalClueIn();
            FadeInVignette(0.4f);
        }

        public void FinalClueIsInPlace()
        {
            if (!finalClueAllowed) return;

            splashButtonCanvasControl.SetButtonsWhenClueisShowing();
        }

        public void FinalClueBackButtonPressed()
        {
            if (!finalClueAllowed) return;

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
            if (!finalClueAllowed) return;

            splashTitleButton.gameObject.GetComponent<SpriteFade>().FadeIn();
        }

        public void FinalClueIsMovedOut()
        {
            if (!finalClueAllowed) return;

            splashTitleButton.Interactable();
        }

    }
}