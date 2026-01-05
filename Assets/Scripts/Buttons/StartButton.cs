using UnityEngine;

public class StartButton : Button
{
    [Header("Scripts")]
    [SerializeField] SplashScreen splashScreen;

    private void Start() { Interactable(); }

    protected override void OnMouseDown()
    {
        if (interactable)
        {
            interactable = false;
            GetComponent<SpriteRenderer>().sprite = mouseDown;

            splashScreen.OnStartButtonPressed();
        }
    }
}
