using Unity.Netcode;
using Unity.Netcode.Components;
using Cinemachine;
using UnityEngine;
using UnityEngine.Assertions;
using System;

// [RequireComponent(typeof(NetworkObject))]
// [RequireComponent(typeof(NetworkTransform))]
public class CameraController : NetworkBehaviour
{

    
    void Start()
    {
        if (IsClient && IsOwner)
        {
            FollowCamera.Instance.FollowPlayer(this.transform.Find("CameraRoot"));
        }
    }
    
    // private CinemachineFreeLook m_MainCamera;

    // void Start()
    // {
    //     AttachCamera();
    // }

    // private void AttachCamera()
    // {
    //     m_MainCamera = GameObject.FindObjectOfType<CinemachineFreeLook>();
    //     Assert.IsNotNull(m_MainCamera, "CameraController.AttachCamera: Couldn't find gameplay camera");

    //     if(m_MainCamera)
    //     {
    //         m_MainCamera.Follow = this.transform;
    //         m_MainCamera.LookAt = this.transform.Find("CameraRoot");
    //     }
    // }
}
