﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinBalloons : MonoBehaviour
{
    Transform cam;
    ParticleSystem.MainModule main;
    void Start()
    {
        cam=Camera.main.transform;
        main = GetComponent<ParticleSystem>().main;

    }

    void Update()
    {
        transform.localPosition = new Vector3(cam.localPosition.x,cam.localPosition.y,cam.localPosition.z+5);
        transform.localEulerAngles= cam.localEulerAngles;
        float fov = cam.gameObject.GetComponent<Camera>().fieldOfView;
        main.startSizeMultiplier = fov/70f;

    }
}
