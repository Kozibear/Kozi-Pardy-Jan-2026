using UnityEngine;

namespace KoziPardy.GameState
{
    public class SplashTitleButton : ButtonSprite
    {
        [Header("Category Reveals")]
        [SerializeField] CategoryReveals categoryReveals;

        [Header("Black Background")]
        [SerializeField] SpriteFade blackForeground;

        [Header("Final Clue")]
        [SerializeField] FinalClueControl finalClueControl;

        private void Start() { Interactable(); }

        protected override void OnMouseDown()
        {
            if (interactable)
            {
                if (GameStateManager.globalGameState == GlobalGameState.Final)
                {
                    finalClueControl.BringInFinalClue();
                }
                else
                {
                    categoryReveals.StartCategoryReveals();
                }

                GetComponent<SpriteFade>().FadeOut();
                blackForeground.FadeIn();

                NotInteractable();
            }
        }
    }
}