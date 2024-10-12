using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public int Price;
    public Sprite Icon;
    public string ClassName;
}
