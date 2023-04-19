using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerMovement : NetworkBehaviour
{   
    [Header("References")]
    [SerializeField]
    private InputReader m_InputReader;

    [SerializeField]
    ServerCharacter m_ServerCharacter;

    [SerializeField]
    CharacterController m_CharacterController;

    private Transform m_MainCameraTransform;

    private PlayerState m_PlayerState;

    float m_ForceSpeed = 6f;
    float m_RotationDamping;

    private Vector3 oldPosRotPosition = Vector3.zero;

    private void Awake() 
    {
        enabled = false;

    }

    public override void OnNetworkSpawn()
    {
        
        if(IsServer)
        {
            enabled = true;
            m_CharacterController = GetComponent<CharacterController>();
        }
        m_MainCameraTransform = Camera.main.transform;
    }

    public override void OnNetworkDespawn()
    {
        if(IsServer)
        {
            enabled = false;
        }
    }

    private void FixedUpdate() {
        PerformMove();
    }

    private void PerformMove()
    {   
        if(m_PlayerState == PlayerState.Idle)
        {
            return;
        }    
        Vector3 motion = CalculateMovement();

        if(m_PlayerState == PlayerState.Walking)
        {
            if(m_InputReader.MovementValue == Vector2.zero)
            {
                m_ServerCharacter.UpdatePlayerStateServerRpc(PlayerState.Idle);
                return;
            }

            m_ServerCharacter.UpdatePlayerStateServerRpc(PlayerState.Walking);
            var desiredAmount = m_ForceSpeed * Time.fixedDeltaTime;
            Move(motion * desiredAmount);
            FaceDirection(motion);
        }
    }

    private void Move(Vector3 motion)
    {
        m_CharacterController.Move(motion);

        if (oldPosRotPosition != motion)
        {
            oldPosRotPosition = motion;
            m_ServerCharacter.UpdatePosRotServerRpc(motion);
        }
    }
    

    private Vector3 CalculateMovement()
    {
        Vector3 forward = m_MainCameraTransform.transform.forward;
        Vector3 right = m_MainCameraTransform.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * m_InputReader.MovementValue.y + 
            right * m_InputReader.MovementValue.x;
    }

    private void FaceDirection(Vector3 movement)
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation, 
            Quaternion.LookRotation(movement), 
            m_RotationDamping * Time.fixedDeltaTime);
    }
}
