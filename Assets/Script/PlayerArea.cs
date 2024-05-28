using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerArea : MonoBehaviour
{
    GameObject playerArea;

    // ��]�p�̕ϐ�
    private Vector3 RotateAxis = Vector3.forward;
    private float SpeedFactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerArea = GameObject.FindGameObjectWithTag("PlayerArea");
        playerArea.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // ��]������
    void Rotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // �w��I�u�W�F�N�g�𒆐S�ɉ�]����
            this.transform.RotateAround(
                this.transform.position,
                RotateAxis,
                360.0f / (1.0f / SpeedFactor) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
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
