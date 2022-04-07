using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportationPose;
    private GameObject player;
    private Transform vrCamera;

    private void Start()
    {
        vrCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void SetPosAndRot()
    {
        SetPlayerPosition(teleportationPose);
        AlignVRCameraDirectionWithTarget(teleportationPose);
    }

    private void SetPlayerPosition(Transform targetPos)
    {
        float distX = player.transform.position.x - vrCamera.transform.position.x;
        float distZ = player.transform.position.z - vrCamera.transform.position.z;
        player.transform.position = new Vector3(targetPos.transform.position.x + distX, targetPos.transform.position.y, targetPos.transform.position.z + distZ);
    }

    private void AlignVRCameraDirectionWithTarget(Transform targetTransform)
    {
        float angle = teleportationPose.rotation.eulerAngles.y - vrCamera.rotation.eulerAngles.y;
        player.transform.RotateAround(new Vector3(vrCamera.position.x, 0, vrCamera.position.z), Vector3.up, angle);
    }
}
