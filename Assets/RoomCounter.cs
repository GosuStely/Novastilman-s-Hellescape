using UnityEngine;
using TMPro;

public class RoomCounter : MonoBehaviour
{
    public TextMeshProUGUI RoomCounterText;
    private int count = 1;
    void Update()
    {
        count = ((int)gameObject.transform.position.x / 80) + 1;
        RoomCounterText.text = "Room Number : " + count;
    }
}
