using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag_And_Drop : MonoBehaviour
{
    [Tooltip("Tag used to mark draggable items (UI GameObjects in the Scroll View)")]
    public string draggableTag = "Draggables";

    [Tooltip("Optional: assign the scene's GraphicRaycaster (will be auto-found if null)")]
     GraphicRaycaster graphicRaycaster;

    private Transform selectedTransform;
    private bool stickerSilhoutteMatched=false;
    private RectTransform selectedRect;
    private Vector3 offset;
    private Vector2 uiOffset;
    private Camera cam;
    private bool isDragging;
    private Canvas dragCanvas;
    private ScrollRect parentScrollRect;
    private EventSystem eventSystem;
    public GameObject CurrentRoom;
    public StickerSO TestSticker;//add list of stickers SO here
    void OnEnable()
    {
        Sticker.OnStickerMatched += MatchingStickerSilhouette;
    }
    void OnDisable()
    {
        Sticker.OnStickerMatched -= MatchingStickerSilhouette;
    }
    void Awake()
    {
        cam = Camera.main;
        if (graphicRaycaster == null)
            graphicRaycaster = FindFirstObjectByType<GraphicRaycaster>();
        eventSystem = EventSystem.current;
    }

    void Update()
    {
        if (cam == null) cam = Camera.main;

        // UI-first check: on mouse down, try GraphicRaycaster (UI) before world checks
        if (Input.GetMouseButtonDown(0) && graphicRaycaster != null && eventSystem != null)
        {
            PointerEventData ped = new PointerEventData(eventSystem);
            ped.position = Input.mousePosition;
            var results = new System.Collections.Generic.List<RaycastResult>();
            graphicRaycaster.Raycast(ped, results);
            if (results.Count > 0)
            {
                foreach (var r in results)
                {
                    if (r.gameObject.CompareTag(draggableTag))
                    {
                        BeginDragUI(r);
                        break;
                    }
                }
            }
            if (isDragging) return; // if we started dragging a UI element, skip the world checks below
        }

        // On mouse down: try 3D raycast first, then 2D overlap point
        if (Input.GetMouseButtonDown(0))
        {
            if (cam == null) return;


           
            // 2D check (useful if your project uses 2D colliders)
            Vector3 worldPoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            Vector2 worldPoint2D = new Vector2(worldPoint.x, worldPoint.y);
            Collider2D col2D = Physics2D.OverlapPoint(worldPoint2D);
            if (col2D != null && col2D.transform.CompareTag(draggableTag))
            {
                BeginDrag2D(col2D.transform, worldPoint);
            }
        }

        // While holding the mouse button, move the selected object to follow the cursor
        if (Input.GetMouseButton(0) && isDragging && selectedTransform != null)
        {
            if (selectedRect != null) // UI dragging
            {
                Camera useCam = (dragCanvas != null && dragCanvas.renderMode != RenderMode.ScreenSpaceOverlay) ? dragCanvas.worldCamera : null;
                Vector2 localPoint;
                RectTransform parentRect = selectedRect.parent as RectTransform;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, Input.mousePosition, useCam, out localPoint);
                selectedRect.anchoredPosition = localPoint + uiOffset;
            }
            else // world-space 2D dragging
            {
                Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
                selectedTransform.position = new Vector3(mouseWorld.x + offset.x, mouseWorld.y + offset.y, selectedTransform.position.z);
            }
        }

        // On release: drop the object where it is
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            EndDrag();
        }
    }

   

    private void BeginDragUI(RaycastResult raycastResult)
    {
        raycastResult.gameObject.transform.SetParent(CurrentRoom.transform);
        selectedTransform = raycastResult.gameObject.transform;
        selectedRect = selectedTransform as RectTransform;
        isDragging = true;

        // store the canvas and compute local offset so the element doesn't snap
        dragCanvas = selectedTransform.GetComponentInParent<Canvas>();
        Camera useCam = (dragCanvas != null && dragCanvas.renderMode != RenderMode.ScreenSpaceOverlay) ? dragCanvas.worldCamera : null;
        Vector2 localPoint;
        RectTransform parentRect = selectedRect.parent as RectTransform;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, Input.mousePosition, useCam, out localPoint);
        uiOffset = selectedRect.anchoredPosition - localPoint;

        // disable parent ScrollRect while dragging to avoid scroll interference
        parentScrollRect = selectedTransform.GetComponentInParent<ScrollRect>();
        if (parentScrollRect != null) parentScrollRect.enabled = false;

        // bring to front so it appears above others while dragging
        selectedRect.SetAsLastSibling();
    }

    private void BeginDrag2D(Transform t, Vector3 mouseWorldPos)
    {
        selectedTransform = t;
        selectedRect = null;
        offset = selectedTransform.position - mouseWorldPos;
        isDragging = true;
        parentScrollRect = selectedTransform.GetComponentInParent<ScrollRect>();
        if (parentScrollRect != null) parentScrollRect.enabled = false;
    }

    private void EndDrag()
    {
        if(stickerSilhoutteMatched)
        {
            print("SNAPPPED");
            selectedTransform.localPosition = TestSticker.stickerSnapTransform.gameObject.GetComponent<RectTransform>().position;
           // selectedTransform.position=new Vector3(0,0,0); //set to snap point position
        }else
        {
              isDragging = false;
        if (parentScrollRect != null) parentScrollRect.enabled = true;
        selectedTransform = null;
        selectedRect = null;
        parentScrollRect = null;

        }
      
    }
    void MatchingStickerSilhouette()
    {

        stickerSilhoutteMatched=true;
    }
}