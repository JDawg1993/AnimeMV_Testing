using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ServerCharacter : NetworkBehaviour
{
    //NetworkVariables
    public NetworkVariable<PlayerState> netPlayerState {get; }= new NetworkVariable<PlayerState>();
    private NetworkVariable<Vector3> netPosRotDirection = new NetworkVariable<Vector3>();

    [Header("References")]
    [SerializeField]
    ServerMovement m_Movement;
    public ServerMovement Movement => m_Movement;


    // Client Caches
    private PlayerState oldPlayerState = PlayerState.Idle;

    private void Awake() 
    {
        enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            enabled = true;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            enabled = false;
        }
    }

    private void Visuals()
    {
        if(oldPlayerState != netPlayerState.Value)
        {
            oldPlayerState = netPlayerState.Value;
        }
    }

    [ServerRpc]
    public void UpdatePosRotServerRpc(Vector3 motion)
    {
        netPosRotDirection.Value = motion;
    }

    [ServerRpc]
    public void UpdatePlayerStateServerRpc(PlayerState state)
    {
        netPlayerState.Value = state;
    }
}
