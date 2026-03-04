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
    [SerializeField] GameObject clueImageSpawnPoint;
    [SerializeField] TextMeshPro clueText;
    [SerializeField] TextMeshPro clueTextShadow;

    private bool spawnedImage = false;

    public void CluePrep()
    {
        //for a normal clue
        if (clueSO.GetClueType() == 0)
        {
            normalClueTextParent.SetActive(true);
            clueText.text = clueSO.GetClueText();
            clueTextShadow.text = clueSO.GetClueText();
        }

        //For an image:
        if (clueSO.GetClueType() == 3)
        {
            clueImageSpawnPoint.SetActive(true);

            if (!spawnedImage)
            {
                //Instantiate(clueSO.GetImage(), clueImageSpawnPoint.transform.position, clueImageSpawnPoint.transform.rotation, clueImageSpawnPoint.transform);
                spawnedImage = true;
            }
        }

    }

    public void HideFrontText()
    {
        frontTextParent.SetActive(false);
    }

    public void ClueCleanup()
    {
        normalClueTextParent.SetActive(false);
        clueImageSpawnPoint.SetActive(false);
    }
}
