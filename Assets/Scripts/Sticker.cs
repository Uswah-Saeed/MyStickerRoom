using System;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    public ScriptableObject stickerData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static event Action OnStickerMatched;
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log("Sticker entered trigger");
    //       if (other.CompareTag("Draggables"))
    //     {
    //         Debug.Log("Sticker is in snap point enterrr");
            
    //     }
    // }
    void OnTriggerStay2D(Collider2D other)
    {
            Debug.Log("Sticker");

        if (other.CompareTag("Draggables"))
        {
            Debug.Log("Sticker is in snap point");
            OnStickerMatched?.Invoke();
        }
    }
}
