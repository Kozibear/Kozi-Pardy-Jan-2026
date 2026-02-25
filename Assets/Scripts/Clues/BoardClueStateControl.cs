using UnityEngine;

public class BoardClueStateControl : MonoBehaviour
{
    [SerializeField] int number = 0;

    private bool hasBeenClicked = false;

    public void ClueHasBeenClicked() {  hasBeenClicked = true; }

    public bool GetHasBeenClicked() { return hasBeenClicked; }
    public int GetNumber() { return number; }

}
