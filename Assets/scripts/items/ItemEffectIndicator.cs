using UnityEngine;
using System;
using System.Data;
using System.Collections.Generic;

public class ItemEffectIndicator : MonoBehaviour
{
    public GameObject[] itemObjects;
    public List<int> selectedItems=new List<int>();

    public static ItemEffectIndicator itemEffectIndicator;

    void Awake() {
        if (itemEffectIndicator == null) {
            itemEffectIndicator = this;
        }
        else if (itemEffectIndicator != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        DataTable itemsDataTable = SQLHandler.sqlHandler.RunCommand("SELECT * FROM selecteditems");
        for (int i=0; i < itemsDataTable.Rows.Count; i++) {
            if (itemsDataTable.Rows[i]["itemid"] == DBNull.Value) {
                selectedItems.Add(-1);
            }
            else {
                int itemID = Convert.ToInt32(itemsDataTable.Rows[i]["itemid"]);
                selectedItems.Add(itemID);
            }
        }
    }
}
