using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class InventoryManager : MonoBehaviour
    {        
        [SerializeField]
        Transform _ContentFieldInventory;
        [SerializeField]
        PlayerInventory playerInventory;
        [SerializeField]
        GameObject inventory;
        [SerializeField]
        GameObject inventoryUI;
      
        [Header("Prefabs")]
        [SerializeField]
        InventorySlot itemPrefab = null;
        [SerializeField]
        OpenUIComponent UIToggle;

        Item item;
        Seed itemToPlant;

        bool playerIsChoosing = false;              
        private void Start()
        {
            Redraw();

            FarmManager.PlayerNeedToSelectAPlant += PlayerIsSelectingAPlant;

            if (playerInventory != null)
            {
                playerInventory = PlayerInventory.GetPlayerInventory();
                playerInventory.inventoryUpdated += Redraw;                
            }

            this.inventoryUI.SetActive(false);

        }  
        
        public void ItemSelected(Item item)
        {
            if (playerIsChoosing && item is Seed)
            {
                itemToPlant = item as Seed;             
                return;
            }
            
            this.item = item;          
        }      
        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < playerInventory.GetSize; i++)
            {
                var itemUI = Instantiate(itemPrefab, transform);
                itemUI.Setup(playerInventory, i, this);
            }          
        }

        void PlayerIsSelectingAPlant(string plantID)
        {            
            playerIsChoosing = true;
            itemToPlant = null;

            this.inventoryUI.SetActive(true);

            StartCoroutine(WaitTillPlantIsSelected());

            IEnumerator WaitTillPlantIsSelected()
            {
                itemToPlant = null;        

                yield return new WaitUntil(() => itemToPlant != null);
                this.inventoryUI.SetActive(false);
               
                FarmManager.PlantIsSelected(plantID, itemToPlant);
                
                playerIsChoosing = false;
            }                                    
        }
    
}
