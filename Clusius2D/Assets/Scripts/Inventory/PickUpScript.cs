using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [SerializeField]
    Item item;

    [SerializeField]
    int amount;

    [SerializeField]
    Collider2D objectCollider;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);

            if (objectCollider == touchedCollider)
            {
                if (touch.phase == TouchPhase.Began)
                {               
                    PlayerInventory inventory = PlayerInventory.GetPlayerInventory();
                    
                    bool itemAddedToInventory = inventory.AddToFirstEmptySlot(item);                    

                    if (itemAddedToInventory)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
