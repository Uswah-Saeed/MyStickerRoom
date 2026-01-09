using UnityEngine;

public class UISticker : MonoBehaviour
{
    public StickerSO Data;

    public RectTransform Rect { get; private set; }

    private Transform startParent;
    private Vector2 startPos;
    private bool isPlaced;

    void Awake()
    {
        Rect = GetComponent<RectTransform>();
        startParent = Rect.parent;
    }

    void Start()
    {
        startPos = Rect.anchoredPosition;

        if (Data.showHintAtFirst)
        {
            UIPlacementManager.Instance.ShowHint(this);
        }
    }

    public void SnapTo(RectTransform target)
    {
        isPlaced = true;

        // IMPORTANT: parent first
        Rect.SetParent(target);
        Rect.anchoredPosition = Vector2.zero;
    }

    public void ReturnToStart()
    {
        // IMPORTANT: parent first
       // Rect.SetParent(startParent);
        Rect.anchoredPosition = transform.localPosition;
    }

    public bool IsPlaced => isPlaced;
}