using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisappear : MonoBehaviour
{
    public void DeactivatePlayer()
    {
        gameObject.SetActive(false);
    }
}
