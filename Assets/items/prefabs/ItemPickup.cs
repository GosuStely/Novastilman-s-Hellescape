using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class ItemPickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject item;

    void Start() {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false) {
                    Instantiate(item, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    inventory.isFull[i] = true;
                    break;
                }
            }
        }
    }
}
