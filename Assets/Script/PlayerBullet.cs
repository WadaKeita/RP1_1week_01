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

        // Area�Ɛe�q�֌W����������
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

    // �v���C���[�ƘA�����ē���
    //void InterLockPlayer()
    //{

    //    if (player.transform.position != prePosition)
    //    {
    //        this.transform.position += player.transform.position - prePosition;
    //        prePosition = player.transform.position;
    //    }
    //}

    // �v���C���[�𒆐S�ɉ�]������
    //void Rotation()
    //{
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        // �w��I�u�W�F�N�g�𒆐S�ɉ�]����
    //        this.transform.RotateAround(
    //            player.transform.position,
    //            RotateAxis,
    //            360.0f / (1.0f / SpeedFactor) * Time.deltaTime);
    //    }
    //    if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        // �w��I�u�W�F�N�g�𒆐S�ɉ�]����
    //        this.transform.RotateAround(
    //            player.transform.position,
    //            RotateAxis,
    //            -(360.0f / (1.0f / SpeedFactor) * Time.deltaTime));
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        // Enter�L�[�Ŕ���
        if (isShot == false && Input.GetKeyDown(KeyCode.Return) ||
            isShot == false && Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            rb.velocity = ShotDirection() * shotPower;
            isShot = true;

            // �e�q�֌W������
            this.transform.parent = null;
        }
    }
}
