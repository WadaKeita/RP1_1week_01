using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D rb;

    private float jumpPower = 10;
    private float movePower = 0;
    private float moveSpeed = 3;

    private bool isCanJump;
    private bool isJump;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJump = false;
        Debug.Log("�Փ˂���");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // �Փ˂��Ă���_�̏�񂪕����i�[����Ă���
        ContactPoint2D[] contacts = collision.contacts;
        // 0�Ԗڂ̏Փˏ�񂩂�A�Փ˂��Ă���_�̕������擾�B
        Vector3 otherNormal = contacts[0].normal;
        // ������������x�N�g���B������1�B
        Vector3 upVector = new Vector3(0, 1, 0);
        // ������Ɩ@���̓��ρB��̃x�N�g���͂Ƃ��ɒ�����1�Ȃ̂ŁAcos�Ƃ̌��ʂ�dotUN�ϐ��ɓ���B
        float dotUN = Vector3.Dot(upVector, otherNormal);
        // ���ϒl�ɋt�O�p�֐�arccos���|���Ċp�x���Z�o�B�����x���@�֕ϊ�����B����Ŋp�x���Z�o�ł����B
        float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;
        // ��̃x�N�g�����Ȃ��p�x��45�x��菬������΍ĂуW�����v�\�Ƃ���B
        if (dotDeg <= 45)
        {
            isCanJump = true;
            Debug.Log("�n�ʂƏՓ˂���");
        }

        Debug.Log("�ڑ���");
        //isCanJump = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (isJump == true)
        {
            isCanJump = false;
        }
        Debug.Log("���E����");
        //isCanJump = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        movePower = 0;
        if (Input.GetKey(KeyCode.D))
        {
            movePower += moveSpeed;
            if (rb.velocity.y < 0)
            {
                isCanJump = false;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            movePower += -moveSpeed;
            if(rb.velocity.y < 0)
            {
                isCanJump = false;
            }
        }
        rb.velocity = new Vector2(movePower, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isCanJump == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isCanJump = false;
            isJump = true;
        }

    }
}
