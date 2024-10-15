using System;
using System.Collections.Generic;

public class Necessity
{
    private Inventory _inventory;

    private int _maxNeededItems;

    public Necessity(int maxNeededItems)
    {
        _inventory = new Inventory();
        _maxNeededItems = maxNeededItems;
    }

    public List<Item> GetNecessities()
    {
        List<Item> items = new List<Item>();

        var itemTypes = Enum.GetValues(typeof(ItemType));

        int itemsCount = 0;

        foreach (ItemType itemType in itemTypes)
        {
            int count = UnityEngine.Random.Range(0, 4);

            if (count > 0)
            {
                itemsCount += count;

                if (itemsCount > _maxNeededItems) break;

                Item item = _inventory.GetItem(itemType);

                item.Count = count;
                items.Add(item);
            }
        }

        if (items.Count <= 0)
        {
            Item item = _inventory.GetItem(ItemType.Painkillers);
            item.Count = 1;
            items.Add(item);
        }

        return items;
    }
}
