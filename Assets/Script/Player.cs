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

    // 衝突判定
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
            Debug.Log("衝突した");
        }
    }
    // 接触中判定
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == ("Ground"))
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
        // ---------- 移動 ----------
        moveVelocity = 0;

        // --- スティック操作 ---
        if (Input.GetAxis("L_Stick_H") != 0)
        {
            // 左右移動
            moveVelocity += moveSpeed * Input.GetAxis("Horizontal");

            // 移動時に地面から離れたらジャンプが出来ないようにする
            if (playerRb.velocity.y < 0)
            {
                isCanJump = false;
            }
        }
        else
        {
            // --- キーボード操作 ---
            // 右移動
            if (Input.GetKey(KeyCode.D))
            {
                moveVelocity += moveSpeed;

                // 移動時に地面から離れたらジャンプが出来ないようにする
                if (playerRb.velocity.y < 0)
                {
                    isCanJump = false;
                }
            }
            // 左移動
            if (Input.GetKey(KeyCode.A))
            {
                moveVelocity += -moveSpeed;

                // 移動時に地面から離れたらジャンプが出来ないようにする
                if (playerRb.velocity.y < 0)
                {
                    isCanJump = false;
                }
            }
        }

        playerRb.velocity = new Vector2(moveVelocity, playerRb.velocity.y);
        // ------------------------------

        // ---------- ジャンプ ----------
        if (isCanJump == true && Input.GetKeyDown(KeyCode.Space) || // キーボード
            isCanJump == true && Input.GetKeyDown(KeyCode.JoystickButton0) // コントローラー
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
