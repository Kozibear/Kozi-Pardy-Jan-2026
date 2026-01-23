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

    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;

    [Header("Doubles Board Button")]
    [SerializeField] DoublesBoardButton doublesBoardButton;
    private bool doublesBoardAdditionalSetup = false;

    private bool canDeactivate = false;

    public void StartCategoryReveals()
    {
        StartCoroutine(WaitBetweenCategoryReveals());
    }

    void Update()
    {
        if (blackBackground.GetComponent<SpriteRenderer>().color.a <= 0 && CategorySpawnPoint.transform.childCount == 0 && canDeactivate)
        {
            gameObject.SetActive(false);
        }
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

        if (doublesBoardAdditionalSetup)
        {
            gameManager.UpdateBoardTitlesAndButtons();
            doublesBoardButton.gameObject.SetActive(false);
        }

        canDeactivate = true;
        blackBackground.GetComponent<SpriteFade>().FadeOut();
        gameManager.BoardBeforeWheelSpin();
    }

    public void ShowDoublesCategories()
    {
        categoryNames = categoryNamesDoubles;
        canDeactivate = false;
        doublesBoardAdditionalSetup = true;
        StartCoroutine(WaitBetweenCategoryReveals());
    }
}
