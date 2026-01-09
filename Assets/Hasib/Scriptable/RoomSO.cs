using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Room")]
public class RoomSO : ScriptableObject
{
    public int roomID;
    public List<RoomSO> roomStickers;
}
