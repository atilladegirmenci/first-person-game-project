using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effects : MonoBehaviour
{
    private enum effectTypeEnum { muzzleFlash, blood ,bulletHole}
    [SerializeField] private effectTypeEnum effectType;
    void Start()
    {
        if(effectType == effectTypeEnum.muzzleFlash) { Destroy_(1); }
        else if (effectType == effectTypeEnum.blood) { Destroy_(5); }
        else if (effectType == effectTypeEnum.bulletHole) { Destroy_(30); }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Destroy_(float t)
    {
        Destroy(gameObject, t);  
    }
}
