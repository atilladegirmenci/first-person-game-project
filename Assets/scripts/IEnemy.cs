using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy 
{
    IEnumerator Die();
    void GetHeadDamage(float damage);
    
    void GetBodyDamage(float damage);
}
