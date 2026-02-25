using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonCanvasControl : MonoBehaviour
{
    [SerializeField] List<Button> boardButtons;
    [SerializeField] Button backButton;
    [SerializeField] GameManager gameManager;

    public void ClueHasBeenSelected()
    {
        setAllBoardButtonsState(false);
    }

    public void ClueIsUpFront()
    {
        backButtonState(true);
        //code for darkening all the buttons
    }

    public void ClueIsBackHome()
    {
        gameManager.BoardBeforeWheelSpin();
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
