using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class CategoryReveals : MonoBehaviour
{
    [Header("Categories")]
    [SerializeField] GameObject CategoryRevealPrefab;
    [SerializeField] GameObject CategorySpawnPoint;
    [SerializeField][TextArea] List<string> categoryNames;
    [SerializeField][TextArea] List<string> categoryNamesDoubles;
    [SerializeField] float waitBeforeNextCategory;

    [Header("Black Background")]
    [SerializeField] GameObject blackBackground;

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

    }

    public void ShowDoublesCategories()
    {
        categoryNames = categoryNamesDoubles;
        canDeactivate = false;
        StartCoroutine(WaitBetweenCategoryReveals());
    }
}
