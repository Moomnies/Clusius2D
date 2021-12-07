using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] InventoryIcon icon;
        [SerializeField] Button button;       

        int index;
        PlayerInventory inventory;
        InventoryManager inventoryManager;
        

        public void Setup(PlayerInventory inventory, int index, InventoryManager gameManager)
        {
            this.inventoryManager = gameManager;
            this.inventory = inventory;
            this.index = index;  
            
            icon.SetItem(inventory.GetItemInSlot(index), button);
            button.onClick.AddListener(BeenClicked);
        }

        public void BeenClicked()
        {
            inventoryManager.ItemSelected(inventory.GetItemInSlot(index));
        }
    } 

