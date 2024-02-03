using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_manager : MonoBehaviour
{
    [SerializeField] private AudioSource pistolSound;
    [SerializeField] private AudioSource ARSound;
    [SerializeField] private AudioSource shotgunClick;
    [SerializeField] private AudioSource shotgunShot;
    static public sound_manager instance;
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayPistolSound()
    {
        pistolSound.Play();
    }
    public void PlayARSound()
    {
        ARSound.Play();
    }
    public void PlayShotgunShot()
    {
        shotgunShot.Play();
    }
    public IEnumerator PlayShotgunClick()
    {
        yield return new WaitForSeconds(0.4f);
        shotgunClick.Play();
    }
}
