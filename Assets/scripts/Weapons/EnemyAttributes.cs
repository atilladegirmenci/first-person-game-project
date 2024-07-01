using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttributes : MonoBehaviour
{

    public bool isAlive;
    public float maxHealth;
    [SerializeField] public Canvas canvas;
    [SerializeField] public ParticleSystem bloodEffect;
    [SerializeField] public float health;
    [SerializeField] public Image healthBar;
}
