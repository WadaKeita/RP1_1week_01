using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundTarget : MonoBehaviour
{
    private GameObject targetObject;

    private Vector3 RotateAxis = Vector3.forward;

    private float SpeedFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = null;
        if (players == null)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
        foreach (GameObject player in players)
        {
            targetObject = player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject == null) return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // 指定オブジェクトを中心に回転する
            this.transform.RotateAround(
                targetObject.transform.position,
                RotateAxis,
                360.0f / (1.0f / SpeedFactor) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // 指定オブジェクトを中心に回転する
            this.transform.RotateAround(
                targetObject.transform.position,
                RotateAxis,
                -(360.0f / (1.0f / SpeedFactor) * Time.deltaTime));

        }
    }
}
