using UnityEngine;
using UnityEngine.EventSystems;

public class UIStickerDrag : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private UISticker sticker;
    private RectTransform rect;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    [SerializeField] RectTransform defaultParent;
    void Awake()
    {
        sticker = GetComponent<UISticker>();
        rect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void SetDefaultParent(RectTransform parent)
    {
        defaultParent = parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (sticker.IsPlaced) return;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;
        transform.SetParent(defaultParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (sticker.IsPlaced) return;

        rect.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (sticker.IsPlaced) return;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        UIPlacementManager.Instance.TryPlace(sticker);
    }
}