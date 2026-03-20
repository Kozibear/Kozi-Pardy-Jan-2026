using UnityEngine;

namespace KoziPardy.Core
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
                if (GameSettings.globalGameState == GlobalGameState.Final)
                {
                    finalClueControl.BringInFinalClue();
                }
                else
                {
                    categoryReveals.StartCategoryReveals();
                    blackForeground.FadeIn();
                }

                GetComponent<SpriteFade>().FadeOut();
                NotInteractable();
            }
        }
    }
}