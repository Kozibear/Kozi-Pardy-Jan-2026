using TMPro;
using UnityEngine;

public class TextFade : Fade
{
    [Header("Text")]
    [SerializeField] TextMeshPro textMeshPro;

    protected override Color GetColor() { return textMeshPro.color; }

    protected override void SetColor(Color updatedColor) { textMeshPro.color = updatedColor; }
}
