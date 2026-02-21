using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CategoryReveals : MonoBehaviour, IFadeListener
{
    [Header("Categories")]
    [SerializeField] GameObject CategoryRevealPrefab;
    [SerializeField] GameObject CategorySpawnPoint;
    [SerializeField][TextArea] List<string> categoryNames;
    [SerializeField][TextArea] List<string> categoryNamesDoubles;

    [Header("WaitTimes")]
    [SerializeField] float waitBeforeNextCategory;
    [SerializeField] float waitBeforeChangingScene;

    [Header("Black Preground")]
    [SerializeField] SpriteFade blackPreground;

    [Header("Board Silhouette")]
    [SerializeField] GameObject boardSilhouette;
    SpriteFade blackBoardSilhouette;
    SpriteFade whitePreground;

    private void Start()
    {
        blackBoardSilhouette = boardSilhouette.transform.GetChild(0).GetComponent<SpriteFade>();
        whitePreground = boardSilhouette.transform.GetChild(1).GetComponent<SpriteFade>();
    }

    public void StartCategoryReveals()
    {
        SetPersistentObjects();
        StartCoroutine(WaitBetweenCategoryReveals());
    }

    private void SetPersistentObjects()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(boardSilhouette);
    }

    IEnumerator WaitBetweenCategoryReveals()
    {
        foreach (string categoryName in categoryNames)
        {
            GameObject newCategory = Instantiate(CategoryRevealPrefab, CategorySpawnPoint.transform.position, Quaternion.identity, CategorySpawnPoint.transform);

            newCategory.GetComponent<CategoryPlaque>().SetCategoryText(categoryName);
            newCategory.GetComponent<CategoryPlaque>().SetCenterDestination();

            yield return new WaitForSeconds(waitBeforeNextCategory);

            newCategory.GetComponent<CategoryPlaque>().SetOffscreenDestination();
        }

        blackBoardSilhouette.FadeIn();
        whitePreground.FadeIn();
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(waitBeforeChangingScene);
        yield return SceneManager.LoadSceneAsync(1);

        blackBoardSilhouette.SetFadeSpeeds(1.5f);
        blackBoardSilhouette.FadeOut();

        whitePreground.SetFadeSpeeds(1f);
        whitePreground.FadeOut();
    }


    public void ShowDoublesCategories()
    {
        categoryNames = categoryNamesDoubles;
        StartCoroutine(WaitBetweenCategoryReveals());
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
