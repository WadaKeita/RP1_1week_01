using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerArea : MonoBehaviour
{
    GameObject playerArea;

    // 回転用の変数
    private Vector3 RotateAxis = Vector3.forward;
    private float SpeedFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerArea = GameObject.FindGameObjectWithTag("PlayerArea");
        playerArea.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // 回転させる
    void Rotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.JoystickButton4)
            || Input.GetAxis("L_R_Trigger") < 0
            )
        {
            // 指定オブジェクトを中心に回転する
            this.transform.RotateAround(
                this.transform.position,
                RotateAxis,
                360.0f / (1.0f / SpeedFactor) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.JoystickButton5)
            || Input.GetAxis("L_R_Trigger") > 0
            )
        {
            // 指定オブジェクトを中心に回転する
            this.transform.RotateAround(
                this.transform.position,
                RotateAxis,
                -(360.0f / (1.0f / SpeedFactor) * Time.deltaTime));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }
}
