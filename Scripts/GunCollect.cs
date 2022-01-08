using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunCollect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text _pickUpText;
    [SerializeField] private AudioSource _pickUp;
    private Player _player;
    private GameObject _parentGun;
    private Gun _gun;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _parentGun = GameObject.Find("GunCollect");
        _gun = GameObject.Find("Gun").GetComponent<Gun>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            // transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y + 0.3f, transform.position.z);
            _pickUp.Play();
            _gun.Equip();
            _pickUpText.enabled = false;
            _player.ArmOneself();
            Destroy(_parentGun, 2.0f);
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
