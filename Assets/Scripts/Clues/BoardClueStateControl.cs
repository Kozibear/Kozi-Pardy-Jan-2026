using KoziPardy.ColorManagement;
using UnityEngine;

public class BoardClueStateControl : MonoBehaviour
{
    [SerializeField] int number = 0;

    [SerializeField] BoardClueColorChange boardClueColorChange;

    [SerializeField] TextColorFade pointValueText;

    [SerializeField] CategoryHeaderStateControl thisCluesCategory;

    private bool hasBeenClicked = false;

    public int GetNumber() { return number; }

    public void SetHasBeenClicked(bool value) {  hasBeenClicked = value; }

    public bool GetHasBeenClicked() { return hasBeenClicked; }

    private void Start()
    {
        pointValueText.InstantDarkenToExactColor();
    }

    public void StartGradualColorChange()
    {
        boardClueColorChange.StartGradualColorChange();
    }

    public void InstantDarken()
    {
        if (!hasBeenClicked)
        {
            pointValueText.InstantDarkenToExactColor();
            boardClueColorChange.InstantColorChange(BoardClueColorChange.ColorValue.Dark);
            thisCluesCategory.InstantDarken();
        }
    }

    public void InstantLighten()
    {
        if (!hasBeenClicked)
        {
            pointValueText.InstantBrightenToExactColor();
            boardClueColorChange.InstantColorChange(BoardClueColorChange.ColorValue.Bright);
            thisCluesCategory.InstantLighten();
        }
    }
}
