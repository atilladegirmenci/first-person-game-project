using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun_interface 
{
    void Shoot();
    void Reload();
    void ADS();
    void ReloadCheck();
    void UpdateBullet(int bulletAmount);
    IEnumerator RateOfFire();
    IEnumerator onReload();

}
