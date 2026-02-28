using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CategoryPlaque : MonoBehaviour
{
    [Header("Cover")]
    [SerializeField] GameObject categoryCover;

    [Header("Texts")]
    [SerializeField] TextMeshPro categoryText;
    [SerializeField] TextMeshPro categoryTextShadow;

    [Header("Movement")]
    [SerializeField] Vector3 centerScreenPosition;
    [SerializeField] Vector3 offScreenPosition;
    Vector3 currentDestination;

    [SerializeField] float moveSpeed;

    private bool canMove = false;
    private bool canDestroy = false;

    void Update()
    {
        float step = Time.deltaTime * moveSpeed;

        if (canMove)
        {
            if (transform.localPosition != currentDestination)
            {
                MoveToDestination(step);
            }
            else
            {
                ArrivedAtDestination();
            }
        }
    }

    private void ArrivedAtDestination()
    {
        canMove = false;
        if (canDestroy) { Destroy(gameObject); }
        categoryCover.GetComponent<SpriteFade>().FadeOut();
    }

    private void MoveToDestination(float step)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentDestination, step);
    }

    public void SetCategoryText(string categoryName)
    {
        categoryText.text = categoryName;
        categoryTextShadow.text = categoryName;
    }

    public void SetCenterDestination()
    {
        canMove = true;
        currentDestination = centerScreenPosition;
    }

    public void SetOffscreenDestination()
    {
        canMove = true;
        currentDestination = offScreenPosition;
        canDestroy = true;
    }
}
