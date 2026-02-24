using TMPro;
using UnityEngine;

public class BoardClueMediaManager : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField] ClueSO clueSO;

    [Header("Front Text")]
    [SerializeField] GameObject frontTextParent;

    [Header("Clue Media")]
    [SerializeField] GameObject normalClueTextParent;
    [SerializeField] TextMeshPro clueText;
    [SerializeField] TextMeshPro clueTextShadow;

    public void CluePrep()
    {
        if (clueSO.GetClueType() == 0)
        {
            normalClueTextParent.SetActive(true);
            clueText.text = clueSO.GetClueText();
            clueTextShadow.text = clueSO.GetClueText();
        }
    }

    public void HideFrontText()
    {
        frontTextParent.SetActive(false);
    }

    public void ClueCleanup()
    {
        normalClueTextParent.SetActive(false);
    }
}
