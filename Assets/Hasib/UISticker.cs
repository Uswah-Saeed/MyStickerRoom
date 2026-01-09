using UnityEngine;

public class UISticker : MonoBehaviour
{
    private StickerSO Data;

    public RectTransform Rect { get; private set; }
    private int startSiblingIndex;
    private Transform startParent;
    private bool isPlaced;

    void OnEnable()
    {
        UIPlacementManager.OnStickerPlaced += UpdateSiblingIndex;
    }

    void OnDisable()
    {
        UIPlacementManager.OnStickerPlaced -= UpdateSiblingIndex;
    }
    void Awake()
    {
        Rect = GetComponent<RectTransform>();
        startParent = Rect.parent; // Content
        startSiblingIndex = Rect.GetSiblingIndex();
    }

    public void SetScriptableData(StickerSO data)
    {
        Data = data;
    }

    public StickerSO GetScriptableData()
    {
        return Data;
    }

    void Start()
    {
        if (Data.showHintAtFirst)
        {
            UIPlacementManager.Instance.ShowHint(this);
        }
    }

    public void SnapTo(RectTransform target)
    {
        isPlaced = true;

        Rect.SetParent(target, false);
        Rect.anchoredPosition = Vector2.zero;
    }

    public void ReturnToStart()
    {
        isPlaced = false;

        // ONLY this. LayoutGroup will place it correctly.
        Rect.SetParent(startParent, false);
        Rect.SetSiblingIndex(startSiblingIndex);
    }
    
    public void UpdateSiblingIndex()
    {
        if (isPlaced)
            return;
        startSiblingIndex = Rect.GetSiblingIndex();
    }

    public bool IsPlaced => isPlaced;
}