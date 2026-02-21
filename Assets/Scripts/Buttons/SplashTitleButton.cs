using UnityEngine;

public class SplashTitleButton : Button
{
    [Header("CategoryReveals")]
    [SerializeField] CategoryReveals categoryReveals;

    [Header("CategoryReveals")]
    [SerializeField] SpriteFade blackForeground;

    private void Start() { Interactable(); }

    protected override void OnMouseDown()
    {
        if (interactable)
        {
            categoryReveals.StartCategoryReveals();
            GetComponent<SpriteFade>().FadeOut();
            blackForeground.FadeIn();

            NotInteractable();
        }
    }
}
