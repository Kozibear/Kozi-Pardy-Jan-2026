using KoziPardy.ColorManagement;
using UnityEngine;
using TMPro;

namespace KoziPardy.Core
{
    public class CategoryHeaderStateControl : MonoBehaviour
    {
        [Header("Cateogry Header Info")]
        [SerializeField] int categoryNumber = 0;
        [SerializeField] int activeCluesInThisCategory = 5;

        [Header("Cateogry Color Change")]
        [SerializeField] BoardClueColorChange categoryColorChange;

        [Header("Cateogry Name Text References")]
        [SerializeField] TextMeshPro nameText;
        [SerializeField] TextMeshPro nameTextShadow;
        [SerializeField] TextColorFade textColorFade;

        [Header("Category Name Text Content")]
        [SerializeField][TextArea] string singleTextString = "";
        [SerializeField] Vector3 singleTextScale = new Vector3(0, 0, 1);
        [SerializeField][TextArea] string doubleTextString = "";
        [SerializeField] Vector3 doubleTextScale = new Vector3(0, 0, 1);

        public int GetNumber() { return categoryNumber; }

        public void ReduceActiveCluesInThisCategory()
        {
            if (activeCluesInThisCategory > 0)
                activeCluesInThisCategory--;

            if (activeCluesInThisCategory <= 0)
            {
                InstantDarken();
                categoryColorChange.SetDepletedCategory(true);
            }
        }

        void Start()
        {
            nameText.text = singleTextString;
            nameTextShadow.text = singleTextString;
            transform.GetChild(0).transform.localScale = singleTextScale;

            if (GameSettings.globalGameState == GlobalGameState.Double)
            {
                nameText.text = doubleTextString;
                nameTextShadow.text = doubleTextString;
                transform.GetChild(0).transform.localScale = doubleTextScale;
            }

            if (GameSettings.WheelSpinGame) textColorFade.InstantDarken();
            categoryColorChange.SetStartingColor(GameSettings.WheelSpinGame);
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