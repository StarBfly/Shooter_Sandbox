using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiveCrate : MonoBehaviour
{
    [SerializeField] private GameObject _crackedCrate;
    [SerializeField] private GameObject _woodenCrate;
    [SerializeField] public bool destructed;

    // Start is called before the first frame update
    void Start()
    {
        _crackedCrate.SetActive(false);
        _woodenCrate.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (destructed)
        {
            Destruct();
        }
    }

    public void Destruct()
    {
        BoxCollider boxCollider = transform.GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        _woodenCrate.SetActive(false);
        _crackedCrate.SetActive(true);

    }
}
