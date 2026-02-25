using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonCanvasControl : MonoBehaviour
{
    [SerializeField] List<Button> boardButtons;
    [SerializeField] Button backButton;

    public void ClueIsUpFront()
    {
        setAllBoardButtonsState(false);
        backButtonState(true);
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
