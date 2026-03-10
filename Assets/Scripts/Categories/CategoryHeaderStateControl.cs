using KoziPardy.ColorManagement;
using UnityEngine;
using TMPro;

namespace KoziPardy.GameState
{
    public class CategoryHeaderStateControl : MonoBehaviour
    {
        [Header("Cateogry Header Info")]
        [SerializeField] int number = 0;

        [Header("Cateogry Color Change")]
        [SerializeField] BoardClueColorChange categoryColorChange;

        [Header("Cateogry Name Text References")]
        [SerializeField] TextMeshPro nameText;
        [SerializeField] TextMeshPro nameTextShadow;
        [SerializeField] TextColorFade textColorFade;

        [Header("Category Name Text Content")]
        [SerializeField] string singleTextString = "";
        [SerializeField] string doubleTextString = "";

        public int GetNumber() { return number; }

        void Start()
        {
            nameText.text = singleTextString;
            nameTextShadow.text = singleTextString;

            if (GameStateManager.globalGameState == GlobalGameState.Double)
            {
                nameText.text = doubleTextString;
                nameTextShadow.text = doubleTextString;
            }

            textColorFade.InstantDarken();
        }

        public void InstantDarken()
        {
            textColorFade.InstantDarken();
            categoryColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Dark, false, false);
        }

        public void InstantLighten()
        {
            textColorFade.InstantBrighten();
            categoryColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Bright, false, false);
        }

        public void GradualLightenIfFinal()
        {
            categoryColorChange.ColorBrightenDarken(BoardClueColorChange.ColorValue.Bright, true, false);
            textColorFade.Brighten();
        }

        public void DarkenJustCategoryName()
        {
            textColorFade.InstantDarken();
        }
    }
}