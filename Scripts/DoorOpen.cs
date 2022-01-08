using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] public bool unlocked;
    private float _yEulerAngel;

    // Start is called before the first frame update
    void Start()
    {
        _yEulerAngel = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (unlocked && _yEulerAngel > 90.0f)
        {
            _yEulerAngel--;
            transform.localEulerAngles = new Vector3(0, _yEulerAngel, 0);
        }
    }

}
