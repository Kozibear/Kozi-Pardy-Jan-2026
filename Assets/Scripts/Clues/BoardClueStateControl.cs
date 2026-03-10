using KoziPardy.ColorManagement;
using KoziPardy.GameState;
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

    public void StartNextColorShift()
    {
        boardClueColorChange.StartNexColorShift();
    }

    public void InstantDarken()
    {
        if (!hasBeenClicked)
        {
            pointValueText.InstantDarkenToExactColor();
            boardClueColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Dark, false, false);
            thisCluesCategory.InstantDarken(); //<-- must implement this again
        }
    }

    public void InstantLighten()
    {
        if (!hasBeenClicked)
        {
            pointValueText.InstantBrightenToExactColor();
            boardClueColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Bright, false, false);
            thisCluesCategory.InstantLighten();
        }
    }

    public void GradualDarkenIfOld()
    {
        if (hasBeenClicked)
        {
            boardClueColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Dark, true, true);
        }
    }

    public void GradualBrightenIfOld()
    {
        if (hasBeenClicked)
        {
            boardClueColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Bright, true, true);
        }
    }

    public void GradualBrightenIfFinal()
    {
        pointValueText.GradualBrightenToExactColor();
        thisCluesCategory.GradualLightenIfFinal();
        boardClueColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Bright, true, false);
    }
}
