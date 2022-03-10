using Saving;
using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event Action inventoryUpdated;

    [SerializeField] Item testItem;

    [SerializeField] int inventorySize = 24;

    [SerializeField] Item[] slots;

    [SerializeField] AudioSource audioSource;

    String[] itemdata;

    public int GetSize { get => slots.Length; }

    private void Start() {
        GameDataInventory data = SaveSystem.LoadInventory();
        itemdata = data.itemIDs;
        //SaveSystem.DeleteFiles();

        for (int i = 0; i < data.itemIDs.Length; i++) {

            if(itemdata[i] != null) {
                slots[i] = Item.GetFromID(data.itemIDs[i]);
            }
        }

        //inventoryUpdated();
    }

    public static PlayerInventory GetPlayerInventory()
    {
        var player = GameObject.FindWithTag("Player");
        return player.GetComponent<PlayerInventory>();
    }

    public bool AddToFirstEmptySlot(Item itemToAdd)
    {
        int i = FindSlot(itemToAdd);

        if (i < 0)
        {
            return false;
        }

        slots[i] = itemToAdd;
        if (inventoryUpdated != null)
        {
            audioSource.Play();
            inventoryUpdated();
        }

        return true;
    }

    public bool HasItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (object.ReferenceEquals(slots[i], item))
            {
                return true;
            }
        }

        return false;
    }

    public Item GetItemInSlot(int slot)
    {
        return slots[slot];
    }

    public void RemoveFromSlot(int slot)
    {
        slots[slot] = null;
        if (inventoryUpdated != null)
        {
            inventoryUpdated();
        }
    }

    public bool AddItemToSlot(int slot, Item itemToAdd)
    {
        if (slots[slot] != null)
        {
            return AddToFirstEmptySlot(itemToAdd);
        }

        slots[slot] = itemToAdd;
        if (inventoryUpdated != null)
        {
            inventoryUpdated();
        }

        return true;
    }

    //PRIVATE
    private void Awake()
    {
        slots = new Item[inventorySize];       
    }

    private int FindSlot(Item itemToAdd)
    {
        return FindEmptySlot();
    }

    private int FindEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    private void OnApplicationQuit() {

        SaveSystem.SaveInventory(this);
    }
}

