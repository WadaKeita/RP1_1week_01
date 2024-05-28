using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBullet : MonoBehaviour
{
    Rigidbody2D rb;

    private bool isShot = false;
    private float shotPower = 5;

    private GameObject playerArea;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerArea = GameObject.FindGameObjectWithTag("PlayerArea");

        // Areaと親子関係を持たせる
        this.transform.parent = playerArea.transform;
    }

    private Vector3 ShotDirection()
    {
        Vector3 direction = Vector3.zero;
        if (playerArea.transform.position != this.transform.position)
        {
            direction = this.transform.position - playerArea.transform.position;
            direction = direction.normalized;
        }
        return direction;
    }

    // プレイヤーと連動して動く
    //void InterLockPlayer()
    //{

    //    if (player.transform.position != prePosition)
    //    {
    //        this.transform.position += player.transform.position - prePosition;
    //        prePosition = player.transform.position;
    //    }
    //}

    // プレイヤーを中心に回転させる
    //void Rotation()
    //{
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        // 指定オブジェクトを中心に回転する
    //        this.transform.RotateAround(
    //            player.transform.position,
    //            RotateAxis,
    //            360.0f / (1.0f / SpeedFactor) * Time.deltaTime);
    //    }
    //    if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        // 指定オブジェクトを中心に回転する
    //        this.transform.RotateAround(
    //            player.transform.position,
    //            RotateAxis,
    //            -(360.0f / (1.0f / SpeedFactor) * Time.deltaTime));
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        // Enterキーで発射
        if (isShot == false && Input.GetKeyDown(KeyCode.Return) ||
            isShot == false && Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            rb.velocity = ShotDirection() * shotPower;
            isShot = true;

            // 親子関係を解除
            this.transform.parent = null;
        }
    }
}
