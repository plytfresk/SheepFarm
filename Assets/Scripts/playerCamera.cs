using UnityEngine;
using Cinemachine;

public class FollowCameraCinemachine : MonoBehaviour
{
    public Transform player;
    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera.Follow = player;
    }
}