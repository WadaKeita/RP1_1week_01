using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    Rigidbody2D rb;

    // クリア判定を取る用
    GameObject enemyManager;
    bool isClear = false;
    GameObject player;
    bool isPlayerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //プレイヤーを取得
        player = GameObject.FindWithTag("Player");
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");

        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<Player>().PlayerIsDead() == true)
        {
            isPlayerDead = true;
            rb.velocity = Vector3.zero;
        }

        if (isClear == false && isPlayerDead == false)
        {

            if (transform.position.x - transform.localScale.x / 2 >= Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x)
            {
                Destroy(gameObject);
            }
            if (transform.position.x + transform.localScale.x / 2 <= Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x)
            {
                Destroy(gameObject);
            }
        }

        isClear = enemyManager.GetComponent<EnemyManager>().IsClear();
    }
}
