using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private float verticleVelocity;

    public Vector3 Movement => Vector3.up * verticleVelocity;

    private void update()
    {
        if(verticleVelocity < 0f && controller.isGrounded)
        {
            verticleVelocity = Physics.gravity.y * Time.deltaTime;
        }
        
        else
        {
            verticleVelocity +=Physics.gravity.y * Time.deltaTime;
        }
    
    }
    
}
