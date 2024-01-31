using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Assault_rifle : Gun_attributes,IGun_interface
{

    private Gun_movement gunMovement;
    private sound_manager soundManager;
    private Gun_attributes gunAttributes;
    private Player_controller playerController;

    
    static public Assault_rifle instance;

    private void Awake()
    {
        gunMovement = Object.FindObjectOfType<Gun_movement>();
        soundManager = Object.FindAnyObjectByType<sound_manager>();
    }
    private void OnEnable()
    {
        gunMovement.setValues(moveToDefSpeed);
        gunMovement.AdjustPosRotForGun_OnSwitch(defPos, defRot);
        canShoot = true;
    }
    void Start()
    {

        instance = this;
        playerController = Object.FindAnyObjectByType<Player_controller>();
        canShoot = true;
        bulletInMag = magSize;
       
    }
   

    void Update()
    {
       
        Shoot();
        ReloadCheck();
        
        if(!isReloading)
        {
            magTextUI.text = bulletInMag.ToString() + "/" + magSize.ToString();
        }
        else
        {
            magTextUI.text = "RELOADING";
        }
        
    }
   

    public void Reload()
    {
        isReloading = true;
        canShoot = false;
        StartCoroutine(onReload());
       
    }

    public void Shoot()
    {

        if (Input.GetMouseButton(0) && canShoot && bulletInMag > 0)
        {
            if (isReloading)
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

                soundManager.PlayARSound();

                bulletInMag--;



                canShoot = false;
                StartCoroutine(RateOfFire());
            }
           
           
        }
            
    }

    

    public IEnumerator onReload() 
    {
        yield return new WaitForSeconds(reloadTime);
        bulletInMag = magSize;
        canShoot = true;
        isReloading = false;
    }
    public void ReloadCheck()
    {
       
        if (bulletInMag != magSize && Input.GetKeyDown(KeyCode.R)) { Reload(); }
    }

   
    public IEnumerator RateOfFire()
    {

       
        yield return new WaitForSeconds(1 / rateOfFire);
        canShoot = true;
    }

   
}