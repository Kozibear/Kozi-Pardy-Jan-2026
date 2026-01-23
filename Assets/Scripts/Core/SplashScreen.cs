using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [Header("Kozi Desk")]
    [SerializeField] GameObject koziDeskImage;
    [SerializeField] Vector3 koziDeskOffscreenPosition;
    [SerializeField] float DeskMoveSpeed;

    [Header("Kozi Pardy")]
    [SerializeField] GameObject koziPardyTitle;
    [SerializeField] Vector3 koziPardyOffscreenPosition;
    [SerializeField] float KoziPardyMoveSpeed;

    [Header("Background Image")]
    [SerializeField] GameObject backgroundImage;

    [Header("Category Reveals")]
    [SerializeField] CategoryReveals categoryReveals;


    bool moveOutStuff = false;

    public void OnStartButtonPressed()
    {
        moveOutStuff = true;
        categoryReveals.StartCategoryReveals();
        backgroundImage.GetComponent<SpriteFade>().FadeOut();
    }

    void Update()
    {
        float step1 = Time.deltaTime * DeskMoveSpeed;
        float step2 = Time.deltaTime * KoziPardyMoveSpeed;

        if (moveOutStuff)
        {
            MoveOutObjects(step1, step2);
        }

        if (moveOutStuff
            && koziDeskImage.transform.localPosition == koziDeskOffscreenPosition
            && koziPardyTitle.transform.localPosition == koziPardyOffscreenPosition
            && backgroundImage.GetComponent<SpriteRenderer>().color.a <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void MoveOutObjects(float step1, float step2)
    {
        koziDeskImage.transform.localPosition = Vector3.MoveTowards(koziDeskImage.transform.localPosition, koziDeskOffscreenPosition, step1);
        koziPardyTitle.transform.localPosition = Vector3.MoveTowards(koziPardyTitle.transform.localPosition, koziPardyOffscreenPosition, step2);
    }
}
