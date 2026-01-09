using UnityEngine;
using UnityEngine.UI;

public class UISnapSpot : MonoBehaviour
{
    public string snapSpotID;
    public bool isOccupied;

    public Image silhouetteImage;
    public RectTransform Rect => (RectTransform)transform;

    void Awake()
    {
        silhouetteImage.enabled = false;
    }

    void Start()
    {
       
        UIPlacementManager.Instance.RegisterSpot(this);
    }

    public void ShowHint(Sprite sprite)
    {
        silhouetteImage.sprite = sprite;
        silhouetteImage.enabled = true;
    }

    public void HideHint()
    {
        silhouetteImage.enabled = false;
    }
}