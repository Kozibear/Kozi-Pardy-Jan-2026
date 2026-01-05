using TMPro;
using UnityEngine;

public class TextColorFade : ColorFade
{
    [Header("Text")]
    [SerializeField] TextMeshPro textMeshPro;

    protected override Color GetColor() { return textMeshPro.color; }

    protected override void SetColor(Color updatedColor) { textMeshPro.color = updatedColor; }
}
