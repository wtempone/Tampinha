using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {

    public Transform PlayerFocus;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]

    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;

    public bool RotateAroundPlayer = true;

    public float RotationsSpeed = 5.0f;
    public float distanceGideLine = 5.0f;
    private bool Draggin = false;

	// Use this for initialization
	void Start () {
        _cameraOffset = transform.position - PlayerFocus.position;
    }

    // LateUpdate is called after Update methods
    void LateUpdate () {

        if (Input.GetMouseButtonDown(0))
            Draggin = true;
        if (Input.GetMouseButtonUp(0))
            Draggin = false;

        if (RotateAroundPlayer && Draggin)
            {
                Quaternion camTurnAngle =
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);

                _cameraOffset = camTurnAngle * _cameraOffset;
            }

            Vector3 newPos = PlayerFocus.position + _cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

            if (LookAtPlayer || RotateAroundPlayer)
                transform.LookAt(PlayerFocus);
	}
}
