using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shotgun : Gun_attributes, IGun_interface
{
    private Gun_attributes gunAttributes;
    private Gun_movement gunMovement;
    private sound_manager soundManager;
   

    static public Shotgun instance;
    [SerializeField] int bulletNumber;

    private void Awake()
    {
        gunMovement = Object.FindObjectOfType<Gun_movement>();
        soundManager = Object.FindAnyObjectByType<sound_manager>();
    }

    private void OnEnable()
    {
        gunMovement.setValues(moveToDefSpeed, defRot);
        gunMovement.AdjustPosRotForGun_OnSwitch(defPos, defRot);
        canShoot = true;
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
        Shoot();
        ReloadCheck();

        if (!isReloading)
        {
            magTextUI.text = bulletInMag.ToString() + "/" + magSize.ToString();
        }
        else
        {
            magTextUI.text = "RELOADING " + bulletInMag.ToString() + "/" + magSize.ToString();
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

    public IEnumerator onReload()
    {
        for(int i = bulletInMag +1; i <= magSize;i++)
        {
          
                if (Input.GetMouseButton(0) && bulletInMag > 0)
                {
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(reloadTime);
                    soundManager.PlayShotgunShellLoad();
                   
                    bulletInMag = i;
                }
            
        }

        StartCoroutine( soundManager.PlayShotgunClick(0.4f));
        canShoot = true;
        isReloading = false;
    }

    public IEnumerator RateOfFire()
    {
        yield return new WaitForSeconds(1 / rateOfFire);
        canShoot = true;
    }

    public void Reload()
    {
        isReloading = true;
        canShoot = false;
        StartCoroutine(onReload());
    }

    public void ReloadCheck()
    {
        if (bulletInMag != magSize && Input.GetKeyDown(KeyCode.R)) { Reload(); }
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

                for(int i = 1; i<=bulletNumber; i++)
                {
                    Instantiate(bullet, bulletSpwnPivot.transform.position, transform.rotation);
                    gunMovement.Recoil(recoilAmountX, recoilAmountY);
                }

                gunMovement.PushBack(pushBackForce);


                soundManager.PlayShotgunShot();
                

                bulletInMag--;


                canShoot = false;
                StartCoroutine(soundManager.PlayShotgunClick(0.4f));
                StartCoroutine(RateOfFire());
            }


        }
    }
}
