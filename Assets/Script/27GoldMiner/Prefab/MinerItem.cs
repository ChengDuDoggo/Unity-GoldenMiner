using UnityEngine;
public enum ItemSpecie
{
    BigGold,
    BrokenCup,
    FourLeafClover,
    Snot,
    Diamond,
    Rock,
    Pig,
}
public class MinerItem : MonoBehaviour
{
    [HideInInspector]
    public int ItemValue = 0;//道具价值
    [HideInInspector]
    public float ItemWeight = 50.0f;//道具重量（越重值越小）
    [HideInInspector]
    public bool IsPig = false;
    public ItemSpecie ItemSpecie;
    private RectTransform rectTransform;
    private BoxCollider2D boxCollider2D;
    private Vector2 screenSize;
    private void Start()
    {
        InitItem();
    }
    private void Update()
    {
        if (IsPig)
        {
            LittlePigRun();
        }
    }
    private void InitItem()
    {
        rectTransform = GetComponent<RectTransform>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = rectTransform.rect.size;
        screenSize = transform.parent.parent.GetComponent<RectTransform>().rect.size;
        switch (ItemSpecie)
        {
            case ItemSpecie.BigGold:
                ItemValue = 500;
                ItemWeight = 10.0f;
                break;
            case ItemSpecie.BrokenCup:
                ItemValue = 250;
                ItemWeight = 40.0f;
                break;
            case ItemSpecie.FourLeafClover:
                ItemValue = 12000;
                ItemWeight = 5.0f;
                break;
            case ItemSpecie.Snot:
                ItemValue = 8000;
                ItemWeight = 15.0f;
                break;
            case ItemSpecie.Diamond:
                ItemValue = 1500;
                ItemWeight = 60.0f;
                break;
            case ItemSpecie.Rock:
                ItemValue = 1;
                ItemWeight = 20.0f;
                break;
            case ItemSpecie.Pig:
                ItemValue = 100;
                ItemWeight = 30.0f;
                IsPig = true;
                break;
            default:
                ItemValue = 0;
                ItemWeight = 50.0f;
                break;
        }
    }
    /// <summary>
    /// 小猪快跑
    /// </summary>
    private void LittlePigRun()
    {
        if (rectTransform.rotation.y == 0)
        {
            rectTransform.anchoredPosition += new Vector2(-50.0f * Time.deltaTime, 0);
            if (rectTransform.anchoredPosition.x < -screenSize.x / 2)
            {
                rectTransform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
        else
        {
            rectTransform.anchoredPosition += new Vector2(50.0f * Time.deltaTime, 0);
            if (rectTransform.anchoredPosition.x > screenSize.x / 2)
            {
                rectTransform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
}