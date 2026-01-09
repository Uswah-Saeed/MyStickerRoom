using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<RoomSO> rooms = new List<RoomSO>();
    int currentRoom = 0;
    [SerializeField] GameObject stickerPrefab;
    [SerializeField] RectTransform stickerParent;
    [SerializeField] RectTransform defaultRectTransform;
    List<GameObject> stickers = new List<GameObject>();
    private void Start()
    {
        if (rooms.Count == 0 || currentRoom >= rooms.Count)
            return;

        GenerateRoom();
        
    }

    void GenerateRoom()
    {
        foreach (StickerSO stickerScriptable in rooms[currentRoom].roomStickers )
        {
            GameObject sticker = Instantiate(stickerPrefab, stickerParent.transform);
            sticker.GetComponent<Image>().sprite = stickerScriptable.stickerSprite;
            UISticker uiSticker = sticker.GetComponent<UISticker>();
            uiSticker.SetScriptableData(stickerScriptable);
            UIStickerDrag uiStickerDrag = sticker.GetComponent<UIStickerDrag>();
            uiStickerDrag.SetDefaultParent(defaultRectTransform);
            stickers.Add(sticker);
        }

        currentRoom++;

    }
}
