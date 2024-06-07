using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataItemView : MonoBehaviour
{
    public delegate void OnDataItemClick(string id);
    public event OnDataItemClick onDataItemClick;

    [SerializeField] public Text ItemName;
    private string itemId;


    public void Init (string itemID, string itemName)
    {
        ItemName.text = itemName;
        this.itemId = itemID;
    }

    public void DataItemButtonClick()
    {
        onDataItemClick?.Invoke(itemId);
    }
}
