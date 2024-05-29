using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject player;
    public Rigidbody2D playerRb;

    GameObject enemyManager;
    //GameObject playerArea;

    private float jumpPower = 15;
    private float moveVelocity = 0;
    private float moveSpeed = 5;

    private bool isCanJump;
    private bool isClear;

    private int playerHP = 3;
    private bool isDead = false;

    // �Փ˔���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            playerHP--;
            if (playerHP <= 0)
            {
                isDead = true;
                playerRb.velocity = Vector3.zero;
                playerRb.gravityScale = 0;
                Debug.Log("dead");
            }
            Debug.Log("�Փ˂���");
        }
    }
    // �ڐG������
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == ("Ground"))
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
        if (collision.gameObject.tag == ("Ground"))
        {
            isCanJump = false;
        }
    }

    public bool PlayerIsDead()
    {
        return isDead;
    }

    private void PlayerMove()
    {
        // ---------- �ړ� ----------
        moveVelocity = 0;

        // --- �X�e�B�b�N���� ---
        if (Input.GetAxis("L_Stick_H") != 0)
        {
            // ���E�ړ�
            moveVelocity += moveSpeed * Input.GetAxis("Horizontal");

            // �ړ����ɒn�ʂ��痣�ꂽ��W�����v���o���Ȃ��悤�ɂ���
            if (playerRb.velocity.y < 0)
            {
                isCanJump = false;
            }
        }
        else
        {
            // --- �L�[�{�[�h���� ---
            // �E�ړ�
            if (Input.GetKey(KeyCode.D))
            {
                moveVelocity += moveSpeed;

                // �ړ����ɒn�ʂ��痣�ꂽ��W�����v���o���Ȃ��悤�ɂ���
                if (playerRb.velocity.y < 0)
                {
                    isCanJump = false;
                }
            }
            // ���ړ�
            if (Input.GetKey(KeyCode.A))
            {
                moveVelocity += -moveSpeed;

                // �ړ����ɒn�ʂ��痣�ꂽ��W�����v���o���Ȃ��悤�ɂ���
                if (playerRb.velocity.y < 0)
                {
                    isCanJump = false;
                }
            }
        }

        playerRb.velocity = new Vector2(moveVelocity, playerRb.velocity.y);
        // ------------------------------

        // ---------- �W�����v ----------
        if (isCanJump == true && Input.GetKeyDown(KeyCode.Space) || // �L�[�{�[�h
            isCanJump == true && Input.GetKeyDown(KeyCode.JoystickButton0) // �R���g���[���[
            )
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
            isCanJump = false;
        }
        // ------------------------------
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        playerRb = gameObject.GetComponent<Rigidbody2D>();

        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");

        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClear == false && isDead == false)
        {
            PlayerMove();
        }
        isClear = enemyManager.GetComponent<EnemyManager>().IsClear();
        //if (clear) { Debug.Log("clear!"); }
    }
}
