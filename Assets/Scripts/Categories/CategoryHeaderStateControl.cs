using KoziPardy.ColorManagement;
using UnityEngine;

public class CategoryHeaderStateControl : MonoBehaviour
{
    [SerializeField] int number = 0;
    [SerializeField] BoardClueColorChange categoryColorChange;
    [SerializeField] TextColorFade categoryNameText;

    public int GetNumber() { return number; }

    void Start()
    {
        categoryNameText.InstantDarken();
    }

    public void InstantDarken()
    {   
        categoryNameText.InstantDarken();
        categoryColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Dark, false, false);
    }

    public void InstantLighten()
    {
        categoryNameText.InstantBrighten();
        categoryColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Bright, false, false);
    }

    public void GradualLightenIfFinal()
    {
        categoryColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Bright, true, false);
        categoryNameText.Brighten();
    }

    public void DarkenJustCategoryName()
    {
        categoryNameText.InstantDarken();
    }
}
