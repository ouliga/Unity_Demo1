using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Bag_Sort
{
    public Inventory_Bag_Sort() { }
    public Inventory_Slot[] Sort_Bag_Fill_Empty(Inventory_Slot[] inventory_Bag)
    {
        Debug.Log("填充背包空格位置");
        if (inventory_Bag == null) return inventory_Bag;
        int length = inventory_Bag.Length;
        Inventory_Slot[] inventory_Bag_Sorted = new Inventory_Slot[length];
        List<Inventory_Slot> inventory_Bag_Show = new();
        List<Inventory_Slot> inventory_Bag_Hidden = new();
        int index = 0;
        for (int i = 0; i < length; i++)
        {
            if (inventory_Bag[i]._item != null)
            {
                if (inventory_Bag[i]._is_Hidden)
                {
                    inventory_Bag_Hidden.Add(inventory_Bag[i]);
                }
                else
                {
                    inventory_Bag_Show.Add(inventory_Bag[i]);
                }
                index++;
            }
        }

        int inventory_Bag_Show_Length = inventory_Bag_Show.Count;
        int inventory_Bag_Hidden_Length = inventory_Bag_Hidden.Count;
        for (int i = 0; i < inventory_Bag_Show_Length; i++)
        {
            inventory_Bag_Sorted[i] = inventory_Bag_Show[i];
        }
        for (int i = 0; i < inventory_Bag_Hidden_Length; i++)
        {
            inventory_Bag_Sorted[i + inventory_Bag_Show_Length] = inventory_Bag_Hidden[i];
        }

        for (int i = index; i < length; i++)
        {
            inventory_Bag_Sorted[i] = new Inventory_Slot();
        }
        return inventory_Bag_Sorted;
    }

    public Inventory_Slot[] Sort_Bag_By_ID(Inventory_Slot[] inventory_Bag)
    {
        Debug.Log("根据id整理背包");

        if (inventory_Bag == null) return inventory_Bag;
        int length = inventory_Bag.Length;

        //根据ID大小进行冒泡排序
        for (int i = 0; i < length - 1; i++)
        {
            for (int j = 0; j < length - 1; j++)
            {
                if (inventory_Bag[j]._item == null || inventory_Bag[j + 1]._item == null)
                {
                    break;
                }
                if (inventory_Bag[j]._is_Hidden || inventory_Bag[j + 1]._is_Hidden)
                {
                    break;
                }
                if (inventory_Bag[j]._item._id > inventory_Bag[j + 1]._item._id)
                {
                    Inventory_Slot temp = inventory_Bag[j + 1];
                    inventory_Bag[j + 1] = inventory_Bag[j];
                    inventory_Bag[j] = temp;
                }
            }
        }
        return inventory_Bag;

    }
    public Inventory_Slot[] Sort_Bag_By_Rare(int rare, Inventory_Slot[] inventory_Bag)
    {
        Debug.Log("根据稀有度整理背包");

        if (inventory_Bag == null) return inventory_Bag;
        int length = inventory_Bag.Length;
        Inventory_Slot[] inventory_Bag_Sorted = new Inventory_Slot[length];
        List<Inventory_Slot> inventory_Bag_Show = new();
        List<Inventory_Slot> inventory_Bag_Hidden = new();

        for (int i = 0; i < length; i++)
        {
            if (inventory_Bag[i]._item == null)
            {
                break;
            }
            if (rare == -1)
            {
                inventory_Bag[i]._is_Hidden = false;
                inventory_Bag_Show.Add(inventory_Bag[i]);
                continue;
            }
            if (inventory_Bag[i]._item._rare == rare)
            {
                inventory_Bag[i]._is_Hidden = false;
                inventory_Bag_Show.Add(inventory_Bag[i]);
            }
            else
            {
                inventory_Bag[i]._is_Hidden = true;
                inventory_Bag_Hidden.Add(inventory_Bag[i]);
            }
        }
        int inventory_Bag_Show_Length = inventory_Bag_Show.Count;
        int inventory_Bag_Hidden_Length = inventory_Bag_Hidden.Count;
        for (int i = 0; i < inventory_Bag_Show_Length; i++)
        {
            inventory_Bag_Sorted[i] = inventory_Bag_Show[i];
        }
        for (int i = 0; i < inventory_Bag_Hidden_Length; i++)
        {
            inventory_Bag_Sorted[i + inventory_Bag_Show_Length] = inventory_Bag_Hidden[i];
        }
        for (int i = inventory_Bag_Show_Length + inventory_Bag_Hidden_Length; i < length; i++)
        {
            inventory_Bag_Sorted[i] = new Inventory_Slot();
        }

        inventory_Bag_Sorted = Sort_Bag_By_ID(inventory_Bag_Sorted);

        return inventory_Bag_Sorted;

    }
}
