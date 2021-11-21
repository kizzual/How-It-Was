using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Cell;
    public List<GameObject> currentInventory;
    public GameObject panel;
    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        
    }
        
    public bool InventoryCellCheck()
    {
        var tmp = Cell.Count;
        if(tmp <= 0)
        {
            return true;
        }
        return false;
    }

}
