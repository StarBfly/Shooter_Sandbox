using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [SerializeField] private Text _pickUpText;
    [SerializeField] private AudioSource _pickUp;
    private Player _player;
    private GameObject _parentKey;
    private Inventory _inventory;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _parentKey = GameObject.Find("MainKey");
        _inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        if (_inventory == null)
        {
            Debug.Log("No inventory set");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            // transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y + 0.3f, transform.position.z);
            _pickUp.Play();
            _pickUpText.enabled = false;
            _player.mainKeyEquiped = true;
            _inventory.EquipMainKey();
            Destroy(_parentKey, 2.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _pickUpText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _pickUpText.enabled = false;
    }
}
