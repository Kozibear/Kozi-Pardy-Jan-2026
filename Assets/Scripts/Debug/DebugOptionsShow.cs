using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class DebugOptionsShow : MonoBehaviour
{
    [SerializeField] List<GameObject> debugButtons = null;

    private bool canShowButtons = true;
    public void ToggleButtons()
    {
        if (debugButtons == null) return;

        foreach (GameObject debugbutton in debugButtons)
        {
            if (canShowButtons) debugbutton.SetActive(true);
            else debugbutton.SetActive(false);
        }

        canShowButtons = !canShowButtons;
    }
}
