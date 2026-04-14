using KoziPardy.Core;
using UnityEngine;

public class BackButtonInvisible : ButtonSprite
{
    [Header("Scripts")]
    [SerializeField] ClueScreen clueScreen;
    [SerializeField] GameBoardManager gameManager;

    void Start()
    {
        NotInteractable();
    }

    protected override void OnMouseDown()
    {
        if (interactable) { BackButtonClicked(); }
    }

    private void BackButtonClicked()
    {
        NotInteractable();
        //gameManager.BackButtonPressed();
    }
}
