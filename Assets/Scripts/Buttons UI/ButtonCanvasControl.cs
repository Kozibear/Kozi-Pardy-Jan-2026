using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using KoziPardy.ColorManagement;

public class ButtonCanvasControl : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] List<Button> boardButtons;
    [SerializeField] Button backButton;
    [SerializeField] GameObject gameManagerObject;

    [Header("Board Clues")]
    [SerializeField] List<BoardClueStateControl> boardClueStateControls;


    public void ActivateSpecificClues(List<int> buttonsToActivate)
    {
        foreach (int i in buttonsToActivate)
        {
            if (!boardClueStateControls[i].GetHasBeenClicked())
            {
                boardButtons[i].gameObject.SetActive(true);
            }
        }
    }
    
    public void ClueHasBeenSelected()
    {
        setAllBoardButtonsState(false);
    }

    public void ClueIsUpFront(int buttonToWhiteList)
    {
        backButtonState(true);
        gameManagerObject.GetComponent<GlobalColorManager>().DarkenAllOtherBoardButtons(buttonToWhiteList);
    }

    public void BackButtonBeenSelected()
    {
        backButtonState(false);
        gameManagerObject.GetComponent<GlobalColorManager>().ChangeGlobalColor();
    }

    public void ClueIsBackHome()
    {
        gameManagerObject.GetComponent<GameManager>().BoardBeforeWheelSpin();
    }

    public void backButtonState(bool state)
    {
        backButton.gameObject.SetActive(state);
    }

    public void setAllBoardButtonsState(bool state)
    {
        foreach (Button boardButton in boardButtons)
        {
            if (boardButton != null) boardButton.gameObject.SetActive(state);
        }
    }
}
