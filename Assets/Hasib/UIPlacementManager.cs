using System.Collections.Generic;
using UnityEngine;

public class UIPlacementManager : MonoBehaviour
{
    public static UIPlacementManager Instance;

    private Dictionary<string, UISnapSpot> spots =
        new Dictionary<string, UISnapSpot>();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterSpot(UISnapSpot spot)
    {
        spots[spot.snapSpotID] = spot;
    }

    public void TryPlace(UISticker sticker)
    {
        if (!spots.TryGetValue(sticker.Data.snapSpotID, out var spot))
        {
            sticker.ReturnToStart();
            return;
        }

        if (spot.isOccupied)
        {
            sticker.ReturnToStart();
            return;
        }

        float dist = Vector2.Distance(
            sticker.Rect.anchoredPosition,
            spot.Rect.anchoredPosition
        );

        if (dist > sticker.Data.snapRadius)
        {
            sticker.ReturnToStart();
            return;
        }

        // SNAP — ONLY HERE
        spot.isOccupied = true;
        sticker.SnapTo(spot.Rect);
        spot.HideHint();
    }

    public void ShowHint(UISticker sticker)
    {
        if (!spots.TryGetValue(sticker.Data.snapSpotID, out var spot))
            return;

        spot.ShowHint(sticker.Data.silhouetteSprite);
    }
}