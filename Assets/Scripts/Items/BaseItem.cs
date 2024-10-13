using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem
{
    protected int _id;
    protected string _name;
    protected string _description;
    protected int _price;
    protected float _value;

    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public int Price { get { return _price; } }
    public float Value { get { return _value; } }

    public virtual void SetInfo(int itemID)
    {
        if (DataManager.Instance.ItemDict.TryGetValue(itemID, out ItemSO data) == false)
        {
            Debug.LogError($"Item SetInfo Failed... / ID : {itemID}");
            Debug.LogError("Please Check Data!");
            return;
        }

        _id = data.ID;
        _name = data.Name;
        _description = data.Description;
        _price = data.Price;
        _value = data.Value;
    }

    public abstract bool Use();
}
