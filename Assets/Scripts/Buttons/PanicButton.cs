using System.Collections.Generic;
using UnityEngine;

public class PanicButton : Button
{
    [SerializeField] List<BoardButton> boardButtons;

    private void Start()
    {
        NotInteractable();
    }

    protected override void OnMouseDown()
    {
        if (interactable) { PanicButtonPressed(); }
    }

    private void PanicButtonPressed()
    {
        foreach (BoardButton boardbutton in boardButtons)
        {
            if (!boardbutton.GetInteractable()) boardbutton.InteractableFadeIn();
        }
        NotInteractable();
    }
}
