using UnityEngine;

public class SpriteColorFade : ColorFade
{
    [Header("Sprite")]
    [SerializeField] SpriteRenderer spriteRenderer;

    protected override Color GetColor() { return spriteRenderer.color; }

    protected override void SetColor(Color updatedColor) { spriteRenderer.color = updatedColor; }
}
