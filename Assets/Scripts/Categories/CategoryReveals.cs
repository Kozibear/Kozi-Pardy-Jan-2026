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
    [SerializeField] SpriteFade blackBoardSilhouette;
    [SerializeField] SpriteFade whitePreground;


    private bool canDeactivate = false;

    public void StartCategoryReveals()
    {
        StartCoroutine(WaitBetweenCategoryReveals());
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
        SceneManager.LoadScene(1);
    }


    public void ShowDoublesCategories()
    {
        categoryNames = categoryNamesDoubles;
        canDeactivate = false;
        StartCoroutine(WaitBetweenCategoryReveals());
    }

    public void FadeInComplete()
    {
        StartCoroutine(ChangeScene());
    }

    public void FadeOutComplete() { }
}
