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

        public void MoveOutNormalClue()
        {
            if (childBoardClueMovement != null)
            {
                childBoardClueMovement.MoveOutClueFinal();
                vignetteFader.StartVigentteFadeOut();
            }
        }

        public void SetBoardClueChild(GameObject childObject)
        {
            childObject.transform.parent = transform;
            childBoardClueMovement = childObject.GetComponent<BoardClueMovement>();
            splashButtonCanvasControl.SetButtonsWhenClueisShowing();

        }

        public void BringInFinalClue()
        {
            finalClueMovement.MoveFinalClueIn();
            vignetteFader.StartVigentteFadeIn();
        }

        public void FinalClueIsInPlace()
        {
            splashButtonCanvasControl.SetButtonsWhenClueisShowing();
        }

        public void FinalClueIsMovingOut()
        {
            vignetteFader.StartVigentteFadeOut();
        }

        public void FinalClueIsMovedOut()
        {
            splashTitleButton.Interactable();
        }

    }
}