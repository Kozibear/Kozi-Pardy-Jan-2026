using UnityEngine;

namespace KoziPardy.Core
{
    public class FinalClueControl : MonoBehaviour
    {
        [SerializeField] SplashButtonCanvasControl splashButtonCanvasControl;
        [SerializeField] VignetteFader vignetteFader;

        private BoardClueMovement childBoardClueMovement;

        public void BringInFinalClue()
        {

        }

        public void MoveOutClue()
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
    }
}