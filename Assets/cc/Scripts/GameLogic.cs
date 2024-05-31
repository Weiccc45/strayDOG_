using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public Inventory playerInventory;
    public Sprite someSprite; // 用于示例的物品图标

    private InventoryUI inventoryUI;

    void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();

        // 创建一个新的物品并添加到背包中
        Item newItem = new Item();
        newItem.itemName = "Health Potion";
        newItem.icon = someSprite; // 设置物品图标
        newItem.quantity = 1;

        playerInventory.AddItem(newItem);
        inventoryUI.UpdateInventoryUI();
    }

    void Update()
    {
        // 检查 Tab 键输入以切换背包显示
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab key pressed");
            inventoryUI.ToggleInventory();
        }

        // 在某个条件下移除物品
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerInventory.RemoveItem("Health Potion");
            inventoryUI.UpdateInventoryUI();
        }
    }
}
