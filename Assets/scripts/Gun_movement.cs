using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using static UnityEngine.GraphicsBuffer;

public class Gun_movement : MonoBehaviour
{
    public Camera playerCamera;
    private Vector3 tempPos;
    private Quaternion tempRot;
    private float rotationX;
    private float rotationY;
    private float currentRecXPos;
    private float currentRecYPos;
    [SerializeField] bool isADS;

    [SerializeField] private Quaternion defaultRot;
    [SerializeField] private Vector3 defaultPos;
    
    [SerializeField] private float moveToDefSpeed;
    [SerializeField] private float rotateToDefSpeed;
    [SerializeField] private float rotationXLimit;
    [SerializeField] private float rotationYLimit;
    static public Gun_movement instance;

    void Start()
    {
        instance = this;
        
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
        StartCoroutine(moveToDefault()) ;
        GunRotation();  
    }
    public void setValues(float _moveToDefSpeed)
    {
        moveToDefSpeed = _moveToDefSpeed;
        
    }
    private void GunRotation()
    {

        rotationX += -Input.GetAxis("Mouse Y")*0.1f;
        rotationY += Input.GetAxis("Mouse X")*0.1f;
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);
        rotationY = Mathf.Clamp(rotationY, -rotationYLimit, rotationYLimit);

       
        if(isMouseMoving())
        {
            transform.localRotation = Quaternion.Euler( rotationX -2 ,rotationY -2 , rotationY +5.5f );
            
        }
        
    }

    public void Recoil(float x, float y)
    {
        currentRecXPos = ((Random.value - .5f) / 2) * x;
        currentRecYPos = ((Random.value - .5f) / 2) * y;
       
        transform.localRotation = Quaternion.Euler(-Mathf.Abs(currentRecYPos) -2, -Mathf.Abs(currentRecXPos) -2, 5.5f);
    }

    public void PushBack(float pushBackForce)
    {

        transform.Translate(new Vector3(0,0,-pushBackForce)* Time.deltaTime);
    }
    public void AdjustPosRotForGun_OnSwitch(Vector3 pos, Quaternion rot)
    {
        transform.localPosition = pos;
        transform.localRotation = rot;
        defaultRot = transform.localRotation;
        defaultPos = transform.localPosition;
       
    }
    public void AdjustPosRotForADS(Vector3 ADSpos, Quaternion ADSrot)
    {
        
      
        int i = 0;
        i++;

        if(i%2==1) 
        {
            tempPos = transform.localPosition;
            tempRot = transform.localRotation;

            defaultRot = ADSrot;
            defaultPos = ADSpos;
        }
       else
        {
            defaultPos = tempPos;
            defaultRot = tempRot;
        }
    }


    public IEnumerator moveToDefault()
    {
        
        if (transform.localRotation != defaultRot && !isMouseMoving())
        {

            yield return new WaitForSeconds(0.1f);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, defaultRot, rotateToDefSpeed * Time.deltaTime);
            
           
            
        }
        if(transform.localPosition != defaultPos)
        {
          
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,defaultPos,moveToDefSpeed*Time.deltaTime);
        }
    }

    private bool isMouseMoving()
    {
        if (Input.GetAxis("Mouse X") <= -0.1 || Input.GetAxis("Mouse X") >= 0.1 || Input.GetAxis("Mouse Y") <= -0.1 || Input.GetAxis("Mouse Y") >= 0.1)
        { return true; }
        else { return false; }


       
    }
}
