using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class InventoryIcon : MonoBehaviour
    {     
        public void SetItem(Item item, Button button)
        {
            var iconImage = GetComponent<Image>();
            if (item == null)
            {
                iconImage.enabled = false;
                button.enabled = false;
            }
            else
            {
                button.enabled = true;
                iconImage.enabled = true;
                iconImage.sprite = item.ItemIcon;
                transform.parent.GetComponent<Image>().sprite = item.ItemBorder;
            }
        }
    }

