using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonCanvasControl : MonoBehaviour
{
    [SerializeField] List<Button> boardButtons;
    [SerializeField] Button backButton;

    void OnEnable()
    {
        //makeAllBoardButtonsNonInteractive();
        backButtonState(false);
    }

    public void ClueIsUpFront()
    {
        makeAllBoardButtonsNonInteractive();
        backButtonState(true);
    }

    public void backButtonState(bool state)
    {
        backButton.gameObject.SetActive(state);
    }

    public void makeAllBoardButtonsNonInteractive()
    {
        foreach (Button boardButton in boardButtons)
        {
            if (boardButton != null) boardButton.interactable = false;
        }
    }
}
