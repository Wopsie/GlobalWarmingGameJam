using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(KeyboardInput))]
public class PlayerMovement : MonoBehaviour {

    KeyboardInput input;

    CharacterController controller;

    [Range(0,1)]
    [SerializeField]float maxMovementSpeed;
    [Range(0,1)]
    [SerializeField]
    float modifierCatchUpSpeed = 1;
    [SerializeField]
    float movementSpeedCceleration;

    float movementModifier = 1; 

    float StopMargin;
    float currentHorizontalVectorSpeed;
    float currentVerticalVectorSpeed;

    Vector3 movementDirection = new Vector3(0,0,0);


	// Use this for initialization
	void Start () {
        StopMargin = maxMovementSpeed / 15;
        input = GetComponent<KeyboardInput>();
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessMovement();
	}

    private void ProcessMovement(){
        if (input.up)
        {
            AddVerticalForce(movementSpeedCceleration);
        }
        if (input.down)
        {
            AddVerticalForce(-movementSpeedCceleration);
        }
        if (input.left)
        {
            AddHorizontalForce(-movementSpeedCceleration);
        }
        if (input.right)
        {
            AddHorizontalForce(movementSpeedCceleration);
        }
        if (!input.left && !input.right)
        {
            DeAccelerateHorizontal(movementSpeedCceleration);
        }
        if (!input.up && !input.down)
        {
            DeAccelerateVertical(movementSpeedCceleration);
        }

        controller.Move(CreateMovementVector(currentHorizontalVectorSpeed, currentVerticalVectorSpeed) * movementModifier);
    }

    private void StabilizeMovementModifier()
    {
        if (movementModifier < 1)
        {
            movementModifier += (movementSpeedCceleration * modifierCatchUpSpeed) * Time.deltaTime;
        }
        else if (movementModifier > 1)
        {
            movementModifier -= (movementSpeedCceleration * modifierCatchUpSpeed) * Time.deltaTime;
        }
    }

    private void AddHorizontalForce(float force){
        currentHorizontalVectorSpeed += force * Time.deltaTime;
        currentHorizontalVectorSpeed = ClampMovementSpeed(currentHorizontalVectorSpeed);
        Debug.Log(currentHorizontalVectorSpeed);
    }

    private void AddVerticalForce(float force)
    {
        currentVerticalVectorSpeed += force * Time.deltaTime;
        currentVerticalVectorSpeed = ClampMovementSpeed(currentVerticalVectorSpeed);
        Debug.Log(currentVerticalVectorSpeed);
    }

    private Vector3 CreateMovementVector(float x, float y)
    {
        return new Vector3(x,y,0);
    }

    private void DeAccelerateHorizontal(float force)
    {
        if (currentHorizontalVectorSpeed > StopMargin)
        {
            currentHorizontalVectorSpeed -= force * Time.deltaTime;
        }
        else if (currentHorizontalVectorSpeed < StopMargin)
        {
            currentHorizontalVectorSpeed += force * Time.deltaTime;
        }
        if (currentHorizontalVectorSpeed >= -StopMargin && currentHorizontalVectorSpeed <= StopMargin)
        {
            currentHorizontalVectorSpeed = 0;
        }
    }
    private void DeAccelerateVertical(float force)
    {
        if (currentVerticalVectorSpeed > StopMargin)
        {
            currentVerticalVectorSpeed -= force * Time.deltaTime;
        }
        else if (currentVerticalVectorSpeed < -StopMargin)
        {
            currentVerticalVectorSpeed += force * Time.deltaTime;
        }
        if (currentVerticalVectorSpeed >= -StopMargin && currentVerticalVectorSpeed <= StopMargin)
        {
            currentVerticalVectorSpeed = 0;
        }
    }

    private float ClampMovementSpeed(float movementSpeed){
        return Mathf.Clamp(movementSpeed, -maxMovementSpeed, maxMovementSpeed);
    }

    public void ModifySpeed(string speed)
    {
        if (speed == "slow")
        {
            movementModifier = .5f;
        }
        else if (speed == "normal")
        {
            movementModifier = 1f;
        }
        else if(speed == "fast")
        {
            movementModifier = 1.5f;
        }
    }

}
