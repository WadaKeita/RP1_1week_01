using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerArea : MonoBehaviour
{
    GameObject playerArea;
    public GameObject playerBulletPrefab;

    // ��]�p�̕ϐ�
    private Vector3 RotateAxis = Vector3.forward;
    private float SpeedFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerArea = GameObject.FindGameObjectWithTag("PlayerArea");
        playerArea.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("EnemyBullet"))
        {
            Instantiate(playerBulletPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }

    }

    // �ڐG������
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == ("Ground"))
        {
        }
    }

    // ���E����
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
        }
    }


    // ��]������
    void Rotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.JoystickButton4)
            || Input.GetAxis("L_R_Trigger") < 0
            )
        {
            // �w��I�u�W�F�N�g�𒆐S�ɉ�]����
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
            // �w��I�u�W�F�N�g�𒆐S�ɉ�]����
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
