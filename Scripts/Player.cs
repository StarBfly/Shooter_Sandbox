using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _chrController;

    [Header("Movement:")]
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float gravity = 9.81f;
    private float _velocityY = 0f;
    [SerializeField] private AudioSource _Step;

    [Header("Guns:")]
    [SerializeField] private GameObject _hitMark;
    private GameObject _crossHair;
    private GameObject _gunObject;
    private GameObject _handGunObject;
    private Gun _gun;
    private bool _gunLowered = false;
    private bool _armed = false;

    [Header("Inventory:")]
    [SerializeField] public bool mainKeyEquiped = false;

    void Start()
    {
        _chrController = GetComponent<CharacterController>();
        _gunObject = GameObject.Find("Gun");
        _gun = _gunObject.GetComponent<Gun>();
        _crossHair = GameObject.Find("CrossHair");
        _handGunObject = GameObject.Find("Hand Gun");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _handGunObject.SetActive(false);
        _crossHair.SetActive(false);
    }

    void Update()
    {
        VisualizeCursor();
        CalculateMovement();
        if (_armed)
        {
            LowerGun();
            if (!_gunLowered)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Fire();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _gun.OnReload();
                }
            }
        }
    }

    void CalculateMovement()
    {
        float currentSpeed = _speed;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (!_chrController.isGrounded)
        {
            _velocityY -= gravity;
        }
        Vector3 direction = new Vector3(horizontalInput, _velocityY, verticalInput);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= 2.0f;
        }

        Vector3 velocity = direction * currentSpeed;
        velocity = transform.TransformDirection(velocity);
        _chrController.Move(velocity * Time.deltaTime);
    }


    IEnumerator StepsSound()
    {
        _Step.Play();
        yield return new WaitForSeconds(1);
    }

    private void VisualizeCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Fire()
    {
        if (_gun.currentAmmo > 0)
        {
            Vector3 screenCentre = new Vector3(0.5f, 0.5f, 0f);
            Ray rayOrigin = Camera.main.ViewportPointToRay(screenCentre);
            RaycastHit hitInfo;
            _gun.OnShot();

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("HIT: " + hitInfo.transform.name);
                GameObject newHitMak = Instantiate(_hitMark, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                if (hitInfo.transform.tag == "Destructive crate")
                {
                    DestructiveCrate destructiveCrate = hitInfo.transform.GetComponent<DestructiveCrate>();
                    destructiveCrate.destructed = true;
                }
                if (hitInfo.transform.tag == "Mutant")
                {
                    Mutant mutant = hitInfo.transform.gameObject.GetComponent<Mutant>();
                    mutant.Damage();
                }
                StartCoroutine(RemoveHitMarks(newHitMak));
            }
        }
        else if (!_gun.reloading)
        {
            _gun.OnEmptyAmmo();
        }
    }

    IEnumerator RemoveHitMarks(GameObject hitMark)
    {
        yield return new WaitForSeconds(1f);
        Destroy(hitMark);
    }


    public void ArmOneself()
    {
        _handGunObject.SetActive(true);
        _crossHair.SetActive(true);
        _gunLowered = false;
        _armed = true;
    }


    private void LowerGun()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!_gunLowered)
            {
                _handGunObject.SetActive(false);
                _crossHair.SetActive(false);
                _gunLowered = true;
            }
            else
            {
                ArmOneself();
            }
        }
    
    }
}
