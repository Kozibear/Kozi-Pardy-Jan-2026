using UnityEngine;

public class CategoryHeaderStateControl : MonoBehaviour
{
    [SerializeField] int number = 0;
    [SerializeField] BoardClueColorChange boardClueColorChange;
    [SerializeField] TextColorFade pointValueText;

    public int GetNumber() { return number; }

    void Start()
    {
        pointValueText.InstantDarken();
    }

    public void InstantDarken()
    {   
        pointValueText.InstantDarken();
        boardClueColorChange.InstantColorDarken();
    }

    public void InstantLighten()
    {
        pointValueText.InstantBrighten();
        boardClueColorChange.InstantColorHighlight();
    }
}
