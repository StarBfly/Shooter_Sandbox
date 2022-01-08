using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 1.0f;
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float eulerAngelX = transform.eulerAngles.x;
        float eulerAngelY = transform.eulerAngles.y + (mouseX * _mouseSensitivity);
        float eulerAngelZ = transform.eulerAngles.z;
        transform.localEulerAngles = new Vector3(eulerAngelX, eulerAngelY, eulerAngelZ);
    }
}
