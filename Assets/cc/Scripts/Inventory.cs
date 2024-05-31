using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Item> items = new List<Item>();

    public void AddItem(Item newItem)
    {
        bool itemExists = false;
        foreach (Item item in items)
        {
            if (item.itemName == newItem.itemName)
            {
                item.quantity += newItem.quantity;
                itemExists = true;
                break;
            }
        }

        if (!itemExists)
        {
            items.Add(newItem);
        }
    }

    public void RemoveItem(string itemName)
    {
        Item itemToRemove = null;
        foreach (Item item in items)
        {
            if (item.itemName == itemName)
            {
                itemToRemove = item;
                break;
            }
        }

        if (itemToRemove != null)
        {
            items.Remove(itemToRemove);
        }
    }
}
