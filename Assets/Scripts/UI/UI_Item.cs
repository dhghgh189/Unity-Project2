using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour, IPointerDownHandler
{
    int _index;

    [SerializeField] Image imgIcon;

    public void Init(int index)
    {
        _index = index;
    }

    public void SetInfo(int ID)
    {
        if (DataManager.Instance.ItemDict.TryGetValue(ID, out ItemSO data) == false)
        {
            Debug.LogWarning($"UI_Item SetInfo Failed... / ID : {ID}");
            Debug.LogWarning("Please Check Data!");
            gameObject.SetActive(false);
            return;
        }

        imgIcon.sprite = data.Icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.Instance.Player == null || GameManager.Instance.Wave.CurState != Enums.WaveState.Progress)
            return;

        GameManager.Instance.Player.UseItem(_index);
    }
}
