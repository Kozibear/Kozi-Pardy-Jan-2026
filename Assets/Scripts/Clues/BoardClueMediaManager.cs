using TMPro;
using UnityEngine;

namespace KoziPardy.Core
{
    public class BoardClueMediaManager : MonoBehaviour
    {
        [Header("Scriptable Objects")]
        [SerializeField] ClueSO singleClueSO;
        [SerializeField] ClueSO doubleClueSO;
        ClueSO currentClueSO;

        [Header("Front Text")]
        [SerializeField] GameObject frontTextParent;
        [SerializeField] TextMeshPro frontText;
        [SerializeField] TextMeshPro frontTextShadow;
        [SerializeField] string frontTextStringSingle;
        [SerializeField] string frontTextStringDouble;
        private string currentFrontTextString;

        [Header("Clue Media")]
        [SerializeField] GameObject normalClueTextParent;
        [SerializeField] GameObject clueImageSpawnPoint;
        [SerializeField] TextMeshPro clueText;
        [SerializeField] TextMeshPro clueTextShadow;

        private bool spawnedImage = false;

        private void Start()
        {
            currentClueSO = singleClueSO;
            currentFrontTextString = frontTextStringSingle;

            if (GameSettings.globalGameState == GlobalGameState.Double)
            {
                currentClueSO = doubleClueSO;
                currentFrontTextString = frontTextStringDouble;
            }

            frontText.text = currentFrontTextString;
            frontTextShadow.text = currentFrontTextString;
        }

        public void CluePrep()
        {
            if (currentClueSO == null) return;

            //for a normal clue
            if (currentClueSO.GetClueType() == 0)
            {
                normalClueTextParent.SetActive(true);
                clueText.text = currentClueSO.GetClueText();
                clueTextShadow.text = currentClueSO.GetClueText();
            }

            //For an image:
            if (currentClueSO.GetClueType() == 3)
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
}