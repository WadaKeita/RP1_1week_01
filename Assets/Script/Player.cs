using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject player;
    public Rigidbody2D playerRb;

    //GameObject playerArea;

    private float jumpPower = 10;
    private float moveVelocity = 0;
    private float moveSpeed = 3;

    private bool isCanJump;
    private bool isJump;

    // �Փ˔���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.tag == ("Player") && collision.gameObject.tag == ("Ground"))
        {
            isJump = false;
            Debug.Log("�Փ˂���");
        }
    }

    // �ڐG������
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (this.tag == ("Player") && collision.gameObject.tag == ("Ground"))
        {
            ContactPoint2D[] contacts = collision.contacts; // �Փ˂��Ă���_�̏�񂪕����i�[����Ă���
            Vector3 otherNormal = contacts[0].normal;   // 0�Ԗڂ̏Փˏ�񂩂�A�Փ˂��Ă���_�̕������擾�B
            Vector3 upVector = new Vector3(0, 1, 0);    // ������������x�N�g���B������1�B
            float dotUN = Vector3.Dot(upVector, otherNormal);   // ������Ɩ@���̓��ρB��̃x�N�g���͂Ƃ��ɒ�����1�Ȃ̂ŁAcos�Ƃ̌��ʂ�dotUN�ϐ��ɓ���B
            float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;   // ���ϒl�ɋt�O�p�֐�arccos���|���Ċp�x���Z�o�B�����x���@�֕ϊ�����B����Ŋp�x���Z�o�ł����B
            if (dotDeg <= 45)   // ��̃x�N�g�����Ȃ��p�x��45�x��菬������΍ĂуW�����v�\�Ƃ���B
            {
                isCanJump = true;
            }
        }
    }

    // ���E����
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (this.tag == ("Player") && collision.gameObject.tag == ("Ground"))
        {
            if (isJump == true)
            {
                isCanJump = false;
            }
        }
    }

    private void PlayerMove()
    {
        moveVelocity = 0;
        if (Input.GetKey(KeyCode.D))
        {
            moveVelocity += moveSpeed;
            if (playerRb.velocity.y < 0)
            {
                isCanJump = false;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVelocity += -moveSpeed;
            if (playerRb.velocity.y < 0)
            {
                isCanJump = false;
            }
        }
        playerRb.velocity = new Vector2(moveVelocity, playerRb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isCanJump == true)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
            isCanJump = false;
            isJump = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        //playerArea.transform.position = player.transform.position;

    }
}
