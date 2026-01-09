using UnityEngine;

[CreateAssetMenu(menuName = "Sticker")]
public class StickerSO : ScriptableObject
{
    public string stickerName;

    public Sprite stickerSprite;       // draggable sprite
    public Sprite silhouetteSprite;    // hint sprite
    public Transform stickerSnapTransform;
    public string snapSpotID;           // UNIQUE per level
    public float snapRadius = 60f;
    public bool showHintAtFirst; // UI pixels
}