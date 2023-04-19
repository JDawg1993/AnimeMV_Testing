using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Cinemachine;
using UnityEngine;

public class FollowCamera : SingletonNetwork<FollowCamera>
{
    private CinemachineFreeLook m_FreeLook;
    public GameObject prefab;


    public override void Awake()
    {
        base.Awake();
        m_FreeLook = GetComponent<CinemachineFreeLook>();
    }

    public void FollowPlayer(Transform transform)
    {
        // not all scenes have a cinemachine virtual camera so return in that's the case
        if (m_FreeLook == null) return;
        m_FreeLook.Follow = transform;
        m_FreeLook.LookAt = transform;
    }
    // Return the prefab to the ObjectPool
}