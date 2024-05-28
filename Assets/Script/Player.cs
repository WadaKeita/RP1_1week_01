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

    // 衝突判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.tag == ("Player") && collision.gameObject.tag == ("Ground"))
        {
            isJump = false;
            Debug.Log("衝突した");
        }
    }

    // 接触中判定
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (this.tag == ("Player") && collision.gameObject.tag == ("Ground"))
        {
            ContactPoint2D[] contacts = collision.contacts; // 衝突している点の情報が複数格納されている
            Vector3 otherNormal = contacts[0].normal;   // 0番目の衝突情報から、衝突している点の方線を取得。
            Vector3 upVector = new Vector3(0, 1, 0);    // 上方向を示すベクトル。長さは1。
            float dotUN = Vector3.Dot(upVector, otherNormal);   // 上方向と法線の内積。二つのベクトルはともに長さが1なので、cosθの結果がdotUN変数に入る。
            float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;   // 内積値に逆三角関数arccosを掛けて角度を算出。それを度数法へ変換する。これで角度が算出できた。
            if (dotDeg <= 45)   // 二つのベクトルがなす角度が45度より小さければ再びジャンプ可能とする。
            {
                isCanJump = true;
            }
        }
    }

    // 離脱判定
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
