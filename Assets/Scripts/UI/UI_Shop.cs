using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] UI_ShopItem[] uiShopItems;

    private void Awake()
    {
        for (int i = 0; i < uiShopItems.Length; i++)
        {
            if (i < DataManager.Instance.ItemDict.Count)
            {
                uiShopItems[i].SetInfo(DataManager.Instance.ItemDict[i].ID);
            }
            else
            {
                uiShopItems[i].gameObject.SetActive(false);
            }
        }
    }

    public void ExitShop()
    {
        GameManager.Instance.StartWave();
        gameObject.SetActive(false);
    }
}
