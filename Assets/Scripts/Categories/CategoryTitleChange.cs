using UnityEngine;

public class CategoryTitleChange : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite singleTitle;
    [SerializeField] Sprite doubleTitle;

    public void SetSingleTitle()
    {
        spriteRenderer.sprite = singleTitle;
    }

    public void SetDoubleTitle()
    {
        spriteRenderer.sprite = doubleTitle;
    }
}
