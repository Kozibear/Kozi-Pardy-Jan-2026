using UnityEngine;

public class BoardClueStateControl : MonoBehaviour
{
    [SerializeField] int number = 0;

    [SerializeField] BoardClueColorChange boardClueColorChange;

    private bool hasBeenClicked = false;

    public int GetNumber() { return number; }

    public void SetHasBeenClicked(bool value) {  hasBeenClicked = value; }

    public bool GetHasBeenClicked() { return hasBeenClicked; }

    public void InstantDarken()
    {
        boardClueColorChange.InstantColorDarken();
    }

    public void InstantLighten()
    {
        if (!hasBeenClicked)
        {
            boardClueColorChange.InstantColorHighlight();
        }
    }
}
