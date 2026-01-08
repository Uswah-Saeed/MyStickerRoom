using UnityEngine;
using UnityEngine.EventSystems;
public class Drag_And_Drop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Selected: " + name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("mouse up");

    }

}