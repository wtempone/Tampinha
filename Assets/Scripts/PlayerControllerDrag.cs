using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerDrag : MonoBehaviour
{
    private GameObject arrow;
  //  private GameObject circle;
    // calcular distancia
    public float maxDistance = 3f;
    private float currentDistance;
    private float safeSpace;
    private float shootPower;
    private Vector3 shootDirection;
    private Vector3 tempPosInital;
    private Vector3 tempPosFinal;
    private void Awake()
    {
        arrow = GameObject.FindGameObjectWithTag("Arrow");
//        circle = GameObject.FindGameObjectWithTag("Circle");
    }

    private void OnMouseDrag()
    {
        tempPosInital = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 10, Input.mousePosition.z));

        currentDistance = Vector3.Distance(tempPosInital, transform.position);
        if (currentDistance <= maxDistance)
        {
            safeSpace = currentDistance;
        } else
        {
            safeSpace = maxDistance;
        }
        //renderiza controles
        RenderControls();
        // calcula força e direção
        shootPower = Mathf.Abs(safeSpace) * 10;
        Vector3 dimXY = tempPosInital - transform.position;
        float difference = dimXY.magnitude;
        tempPosFinal = transform.position + ((dimXY / difference) * currentDistance * -1);
        tempPosFinal = new Vector3(tempPosFinal.x, -5f, tempPosFinal.z);
        shootDirection = Vector3.Normalize(tempPosInital - transform.position);

    }

    private void OnMouseUp()
    {
        arrow.GetComponent<Renderer>().enabled = false;
        //circle.GetComponent<Renderer>().enabled = false;

        Vector3 push = shootDirection * shootPower * -1;
        GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
    }
    private void RenderControls()
    {
        arrow.GetComponent<Renderer>().enabled = true;
        //circle.GetComponent<Renderer>().enabled = true;

        if (currentDistance <= maxDistance)
        {
            arrow.transform.position = new Vector3(
                ((2 * transform.position.x) - tempPosInital.x),
                1.8f,
                ((2 * transform.position.z) - tempPosInital.z)
            );
        } else
        {
            Vector3 dimXY = tempPosInital - transform.position;
            float difference = dimXY.magnitude;
            arrow.transform.position = transform.position + ((dimXY / difference) * currentDistance * -1);
            arrow.transform.position = new Vector3(arrow.transform.position.x, -1.5f, arrow.transform.position.z);
        }

        //circle.transform.position = transform.position + new Vector3(0, 0, 0.4f);
        Vector3 dir = tempPosInital - transform.position;
        float rot;
        if (Vector3.Angle(dir, transform.position) > 90)
        {
            rot = Vector3.Angle(dir, transform.up);
        } else
        {
            rot = Vector3.Angle(dir, transform.up) * -1;
        }
        arrow.transform.eulerAngles = new Vector3(0, rot, 0);

        float scaleX = Mathf.Log(1 + safeSpace / 2, 2) * 2.2f;
        float scaleZ = Mathf.Log(1 + safeSpace / 2, 2) * 2.2f;

        arrow.transform.localScale = new Vector3(1 + scaleX, 0.001f, 1 + scaleZ);
        //circle.transform.localScale = new Vector3(1 + scaleX, 1 + scaleY, 0.001f);
    }

}
