using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private GameObject _mainKey;
    // Start is called before the first frame update
    void Start()
    {
        _mainKey = GameObject.Find("KeyInventoryText");
        if (_mainKey == null)
        {
            Debug.Log("No main key inventory sprite");
        }
        _mainKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EquipMainKey()
    {
        _mainKey.SetActive(true);
    }
}
