using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun_interface 
{
   

    void Shoot();
    void Reload();

    void ReloadCheck();
    IEnumerator RateOfFire();

    IEnumerator onReload();


}
