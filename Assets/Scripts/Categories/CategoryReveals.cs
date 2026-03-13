using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace KoziPardy.Core
{
    public class CategoryReveals : MonoBehaviour, IFadeListener
    {
        [Header("Categories GameObjects")]
        [SerializeField] GameObject CategoryRevealPrefab;
        [SerializeField] GameObject CategorySpawnPoint;

        [Header("Categories Texts")]
        [SerializeField][TextArea] List<string> categoryNamesSingle;
        [SerializeField] List<Vector3> singleTextScales;
        [SerializeField][TextArea] List<string> categoryNamesDoubles;
        [SerializeField] List<Vector3> doubleTextScales;
        private List<string> currentCategoryNames;

        [Header("WaitTimes")]
        [SerializeField] float waitBeforeNextCategory;
        [SerializeField] float waitBeforeChangingScene;

        [Header("Black Preground")]
        [SerializeField] SpriteFade blackPreground;

        [Header("Board Silhouette")]
        [SerializeField] GameObject boardSilhouette;
        SpriteFade blackBoardSilhouette;
        SpriteFade whitePreground;

        [Header("Debug Buttons Canvas")]
        [SerializeField] GameObject debugButtonsCanvas;

        private void Start()
        {
            blackBoardSilhouette = boardSilhouette.transform.GetChild(0).GetComponent<SpriteFade>();
            whitePreground = boardSilhouette.transform.GetChild(1).GetComponent<SpriteFade>();

            currentCategoryNames = categoryNamesSingle;

            if (GameSettings.globalGameState == GlobalGameState.Double)
            {
                currentCategoryNames = categoryNamesDoubles;
            }
        }

        public void StartCategoryReveals()
        {
            StartCoroutine(WaitBetweenCategoryReveals());
        }

        private void SetPersistentObjects()
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(boardSilhouette);
        }

        IEnumerator WaitBetweenCategoryReveals()
        {
            foreach (string categoryName in currentCategoryNames)
            {
                GameObject newCategory = Instantiate(CategoryRevealPrefab, CategorySpawnPoint.transform.position, Quaternion.identity, CategorySpawnPoint.transform);

                newCategory.GetComponent<CategoryPlaque>().SetCategoryText(categoryName);
                newCategory.GetComponent<CategoryPlaque>().SetCenterDestination();

                yield return new WaitForSeconds(waitBeforeNextCategory);

                newCategory.GetComponent<CategoryPlaque>().SetOffscreenDestination();
            }

            debugButtonsCanvas.gameObject.SetActive(false);
            blackBoardSilhouette.FadeIn();
            whitePreground.FadeIn();
        }

        IEnumerator ChangeScene()
        {
            SetPersistentObjects();
            yield return new WaitForSeconds(waitBeforeChangingScene);

            yield return GameSettings.theOnlyGameManager.GetComponent<SceneLoadController>().LoadSceneCoroutine(1, GameSettings.globalGameState);

            blackBoardSilhouette.SetFadeSpeeds(1.5f);
            blackBoardSilhouette.FadeOut();

            whitePreground.SetFadeSpeeds(1f);
            whitePreground.FadeOut();
        }

        public void FadeInComplete()
        {
            StartCoroutine(ChangeScene());
        }

        public void FadeOutComplete()
        {
            Destroy(boardSilhouette);
            Destroy(gameObject);
        }
    }
}