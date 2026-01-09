using System;
using TMPro;
using UnityEngine;

public class PlacementStats : MonoBehaviour
{
    public TextMeshProUGUI StickersPlaced;
    int stickersTotal;
    int stickersPlaced = 0;


    void OnEnable()
    {
        stickersTotal = 20; //maybe the room can have the info of it's total stickers
        stickersPlaced = 0;
        StickersPlaced.text = stickersPlaced + " / " + stickersTotal;
        UIPlacementManager.OnStickerPlaced += UpdateStickerPlaced;

    }
    void UpdateStickerPlaced()
    {
        stickersPlaced++;
        StickersPlaced.text = stickersPlaced + " / " + stickersTotal;
    }
    void OnDestroy()
    {
        UIPlacementManager.OnStickerPlaced -= UpdateStickerPlaced;
    }
}
