using UnityEngine;

public abstract class Button : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite regular;
    public Sprite mouseOver;
    public Sprite mouseDown;
    public Sprite nonInteractive;

    protected bool interactable = false;

    public void Interactable()
    { 
        interactable = true;
        GetComponent<SpriteRenderer>().sprite = regular;
    }

    public void NotInteractable()
    {
        interactable = false;
        GetComponent<SpriteRenderer>().sprite = nonInteractive;
    }

    //For when the mouse is hovering over it
    protected virtual void OnMouseEnter()
    {
        if (interactable) { GetComponent<SpriteRenderer>().sprite = mouseOver; }
    }

    //For when the mouse is no longer hovering over it
    protected virtual void OnMouseExit()
    {
        if (interactable) { GetComponent<SpriteRenderer>().sprite = regular; }
    }

    //For when the mouse clicks on it
    protected abstract void OnMouseDown();
}
