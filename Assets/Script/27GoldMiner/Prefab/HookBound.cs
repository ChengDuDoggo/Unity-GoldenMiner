using UnityEngine;

public class HookBound : MonoBehaviour
{
    private RectTransform rectTransform;
    private BoxCollider2D boxCollider2D;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = rectTransform.rect.size;
    }
}