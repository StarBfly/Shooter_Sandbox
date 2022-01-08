using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontDoor : MonoBehaviour
{
    [SerializeField] private Text _hasKeyText;
    [SerializeField] private Text _noKeyText;
    [SerializeField] private AudioSource _unlockSound;
    private Player _player;
    private DoorOpen _doorOpen;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _doorOpen = GameObject.Find("Main Gate Door").GetComponent<DoorOpen>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_doorOpen.unlocked)
        {
            if (_player.mainKeyEquiped)
            {
                _hasKeyText.enabled = true;
            }
            else
            {
                _noKeyText.enabled = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            if (_player.mainKeyEquiped && Input.GetKey(KeyCode.E))
            {
                _doorOpen.unlocked = true;
                _unlockSound.Play();
                _hasKeyText.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _hasKeyText.enabled = false;
        _noKeyText.enabled = false;
    }
}
