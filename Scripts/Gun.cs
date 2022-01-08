using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private float _fireSpeed = 1f;
    private GameObject _flashPoint;
    [SerializeField]
    private AudioSource _gunShotAudio;
    [SerializeField]
    private AudioSource _gunReloadAudio;
    [SerializeField]
    private AudioSource _gunEmptyAudio;
    [SerializeField]
    private int _maxAmmo = 15;
    [SerializeField]
    public int currentAmmo = 15;
    public bool reloading = false;
    private UIManager _UIManager;
    private bool _equiped = false;

    void Start()
    {
        _flashPoint = GameObject.Find("FlashPoint");
        if (_flashPoint == null)
        {
            Debug.Log("No FlashPoint assigned");
        }
        _gunShotAudio = GetComponent<AudioSource>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_UIManager == null)
        {
            Debug.Log("UIManager is missing.");
        }
    }

    public void Equip()
    {
        _equiped = true;
        _UIManager.UpdateAmmo(currentAmmo);
    }

    public void OnShot()
    {
        GameObject newFlash = Instantiate(_muzzleFlash, _flashPoint.transform.position, Quaternion.identity);
        StartCoroutine(RemoveMuzzleFlash(newFlash));
        _gunShotAudio.Play();
        currentAmmo--;
        _UIManager.UpdateAmmo(currentAmmo);
    }

    IEnumerator RemoveMuzzleFlash(GameObject flash)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(flash);
    }


    public void OnReload()
    {
        _gunReloadAudio.Play();
        StartCoroutine(Reload());
    }


    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(1.5f);
        currentAmmo = _maxAmmo;
        _UIManager.UpdateAmmo(currentAmmo);
        reloading = false;
    }


    public void OnEmptyAmmo()
    {
        _gunEmptyAudio.Play();
    }
}
