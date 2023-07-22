using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControleur : MonoBehaviour
{

    [SerializeField] private GameObject personnageFollow;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = personnageFollow.transform.position;
        pos.y = 11;
        pos.z -= 11;
        transform.position = pos;
    }
}
