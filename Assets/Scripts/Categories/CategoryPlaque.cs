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

    [Header("Whole GameObject Movement")]
    [SerializeField] Vector3 centerScreenPosition;
    [SerializeField] Vector3 offScreenPosition;
    Vector3 currentDestination;

    [SerializeField] float moveSpeed;

    [Header("Whole GameObject Movement")]
    [SerializeField] float moveShellSpeed = 1;

    private bool canMove = false;
    private bool canMoveShell = false;
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

        if (canMoveShell)
        {
            MoveShell();
        }
    }

    private void ArrivedAtDestination()
    {
        canMoveShell = true;
        canMove = false;
        if (canDestroy) { Destroy(gameObject); }
        categoryCover.GetComponent<SpriteFade>().FadeOut();
    }

    private void MoveToDestination(float step)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentDestination, step);
    }

    private void MoveShell()
    {
        transform.GetChild(0).transform.Translate(Vector3.left * Time.deltaTime * moveShellSpeed);
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
