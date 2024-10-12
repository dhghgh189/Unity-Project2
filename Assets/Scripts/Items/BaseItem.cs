using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem
{
    protected int _id;
    protected string _name;
    protected string _description;
    protected int _price;

    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public int Price { get { return _price; } }

    public abstract void Use();
}
