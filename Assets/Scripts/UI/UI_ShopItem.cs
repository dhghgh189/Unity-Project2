using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem : MonoBehaviour, IPointerDownHandler
{
    int _id;
    int _price;
    string _className;
    [SerializeField] TextMeshProUGUI txtItemInfo; 
    [SerializeField] Image imgIcon;

    public void SetInfo(int ID)
    {
        if (DataManager.Instance.ItemDict.TryGetValue(ID, out ItemSO data) == false)
        {
            Debug.LogWarning($"UI_Item SetInfo Failed... / ID : {ID}");
            Debug.LogWarning("Please Check Data!");
            gameObject.SetActive(false);
            return;
        }

        _id = ID;
        _price = data.Price;
        _className = data.ClassName;

        txtItemInfo.text = $"{data.Name}\n({data.Price})";
        imgIcon.sprite = data.Icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.Player == null)
            return;

        if (GameManager.Instance.Player.Coin < _price)
            return;

        if (GameManager.Instance.Player.Inventory.Count >= Define.INVENTORY_SIZE)
            return;

        Type type = Type.GetType(_className);
        BaseItem item = Activator.CreateInstance(type) as BaseItem;

        if (item == null)
            return;

        GameManager.Instance.Player.Coin -= _price;
        item.SetInfo(_id);
        GameManager.Instance.Player.AddItem(item);
    }
}
