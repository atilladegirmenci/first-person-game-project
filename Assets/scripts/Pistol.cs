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
    private Gun_attributes gunAttributes;
    private Gun_movement gunMovement;
    private sound_manager soundManager;
    private Player_controller playerController;
    //[SerializeField] LineRenderer lineRenderer;

    static public Pistol instance;

    private void Awake()
    {
        gunMovement = Object.FindObjectOfType<Gun_movement>();
        soundManager = Object.FindAnyObjectByType<sound_manager>();
    }
    private void OnEnable()
    {
        canShoot = true;
        gunMovement.AdjustPosRotForGun_OnSwitch(defPos, defRot);
        gunMovement.setValues(moveToDefSpeed);
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
      


        if (!isReloading)
        {
            magTextUI.text = bulletInMag.ToString() + "/" + magSize.ToString();
        }
        else
        {
            magTextUI.text = "RELOADING";
        }


    }

    public void Shoot()
    {
       if(Input.GetMouseButtonDown(0) &&canShoot)
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

                soundManager.PlayPistolSound();

                bulletInMag--;


                canShoot = false;
                StartCoroutine(RateOfFire());
            }
           
        }

    }
   
    public void ReloadCheck()
    {
        if (bulletInMag <=0)
        {
            canShoot = false;
        }
        
        
        if (bulletInMag != magSize && Input.GetKeyDown(KeyCode.R)) { Reload(); }
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
        bulletInMag = magSize;
        canShoot = true;
        isReloading = false;
    }
    public IEnumerator RateOfFire()
    {
       yield return new WaitForSeconds(1/rateOfFire);
       canShoot = true;
    }

    //void DrawLine()
    //{


    //    Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward);
    //    RaycastHit hit;

    //    if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, Mathf.Infinity))
    //    {
    //        Debug.DrawLine(ray.origin, hit.point, Color.red);
         



    //        lineRenderer = GetComponent<LineRenderer>();
    //        lineRenderer.SetPosition(0, ray.origin);
    //        lineRenderer.SetPosition(1, hit.point);

    //    }

        
       
    //}
}
