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
        Debug.Log("衝突した");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 衝突している点の情報が複数格納されている
        ContactPoint2D[] contacts = collision.contacts;
        // 0番目の衝突情報から、衝突している点の方線を取得。
        Vector3 otherNormal = contacts[0].normal;
        // 上方向を示すベクトル。長さは1。
        Vector3 upVector = new Vector3(0, 1, 0);
        // 上方向と法線の内積。二つのベクトルはともに長さが1なので、cosθの結果がdotUN変数に入る。
        float dotUN = Vector3.Dot(upVector, otherNormal);
        // 内積値に逆三角関数arccosを掛けて角度を算出。それを度数法へ変換する。これで角度が算出できた。
        float dotDeg = Mathf.Acos(dotUN) * Mathf.Rad2Deg;
        // 二つのベクトルがなす角度が45度より小さければ再びジャンプ可能とする。
        if (dotDeg <= 45)
        {
            isCanJump = true;
            Debug.Log("地面と衝突した");
        }

        Debug.Log("接続中");
        //isCanJump = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (isJump == true)
        {
            isCanJump = false;
        }
        Debug.Log("離脱した");
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
