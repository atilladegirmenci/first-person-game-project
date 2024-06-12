using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

public class Pistol : Gun_attributes, IGun_interface
{
    
    private Gun_movement gunMovement;
    static public Pistol instance;

    private void Awake()
    {
        gunMovement = Object.FindObjectOfType<Gun_movement>();   
    }
    private void OnEnable()
    {
        canShoot = true;
        gunMovement.AdjustPosRotForGun_OnSwitch(defPos, defRot);
        gunMovement.SetValues(moveToDefSpeed,defRot);
    }
    void Start()
    {
        instance = this;
        canShoot = true;
        bulletInMag = magSize;
    }
   
    void Update()
    {
        ADS();
        if (Input.GetMouseButtonDown(0)) { Shoot(); }
        if (Input.GetKeyDown(KeyCode.R)) { ReloadCheck(); }
        
        UIManager.instance.BulletCountText(isReloading, bulletInMag, magSize);
    }

    public void Shoot()
    {
       if(canShoot && bulletInMag >0)
        {
         
            if(isReloading)
            {
                Reload();
            }
            else
            {
                var newEffect = Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation);
                newEffect.transform.parent = gameObject.transform;

                Instantiate(bullet, bulletSpwnPivot.transform.position, transform.rotation);

                gunMovement.PushBack(pushBackForce);
                gunMovement.Recoil(recoilAmountX, recoilAmountY);

                sound_manager.instance.PlayPistolSound();

                bulletInMag--;


                canShoot = false;
                StartCoroutine(RateOfFire());
            }
           
        }

    }
    public void ADS()
    {
        if (Input.GetMouseButtonDown(1))
        {
            gunMovement.AdjustPosRotForADS(ADSPos, ADSRot);
        }
        if (Input.GetMouseButtonUp(1))
        {
            gunMovement.AdjustPosRotForADS(defPos, defRot);
        }
    }
    public void ReloadCheck()
    {
        if (bulletInMag != magSize) { Reload(); }
    }
    public void Reload()
    {
        isReloading = true;
        canShoot = false;
        StartCoroutine(onReload());
    }

    public IEnumerator onReload()
    {
        
        yield return new WaitForSeconds(reloadTime);
        sound_manager.instance.PlayReloadPistol();
        bulletInMag = magSize;
        canShoot = true;
        isReloading = false;
    }
    public IEnumerator RateOfFire()
    {
       yield return new WaitForSeconds(1/rateOfFire);
       canShoot = true;
    }
}
