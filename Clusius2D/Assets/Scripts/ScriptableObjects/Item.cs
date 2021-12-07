using System;
using System.Collections.Generic;
using UnityEngine;

    public class Item : ScriptableObject, ISerializationCallbackReceiver
    {
        [Header("Item Data")]
        [SerializeField] string itemID;
        [SerializeField] bool _Stackable;

        [Header("In Game Date")]
        [SerializeField] string itemName;
        [SerializeField] [TextArea] string itemDiscription;

        [Header("Assign before use")]
        [SerializeField] Sprite itemIcon;
        [SerializeField] Sprite itemBorder;

        static Dictionary<string, Item> itemLookupCache;

        public Sprite ItemBorder { get => itemBorder; }
        public Sprite ItemIcon { get => itemIcon; }
        public string ItemID { get => itemID; }

        public static Item GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, Item>();
                var itemList = Resources.LoadAll<Item>("");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicateID for objects: {0} and {1}", itemLookupCache[item.itemID], item));
                        continue;
                    }

                    itemLookupCache[item.itemID] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }

        //PRIVATE  
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (string.IsNullOrWhiteSpace(itemID))
            {
                itemID = Guid.NewGuid().ToString();
            }
        }
    }


  
