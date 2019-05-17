using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerMouseDrag : MonoBehaviour
{
    private float OffSet = -0.8f;
    private Vector3 tempPos;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, OffSet);
    }

    void Update()
    {
        tempPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = new Vector3(tempPos.x, tempPos.y, OffSet);
    }
}
