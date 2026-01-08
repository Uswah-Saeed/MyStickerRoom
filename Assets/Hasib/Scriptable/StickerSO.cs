using UnityEngine;
[CreateAssetMenu(
    fileName = "NewStickerData",
    menuName = "Add Sticker/Sticker",
    order = 1)]
public class StickerSO : ScriptableObject
    
{
   public string stickerName;
   public Sprite stickerImage;
   public Transform stickerSnapTransform;
   public AnimationClip stickerPlacementClip;
   public AnimationClip stickerClipLoop;
}
