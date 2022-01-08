using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackDoor : MonoBehaviour
{
    [SerializeField] private Text _lockedText;
    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_player.mainKeyEquiped)
        {
            _lockedText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _lockedText.enabled = false;
    }
}
