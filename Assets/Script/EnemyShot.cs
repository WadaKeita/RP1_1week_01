using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    #region var-EnemyShot
    [Header("敵の弾関連")]
    [SerializeField] float enemyShotSpeed = 2f; //敵の弾の発射スピード
    [SerializeField] GameObject enemyBullet;   //敵のリジッドボディ
    [SerializeField] Transform firePos;         //敵の弾の発射ポイント
    [SerializeField] float shotThrehold = 1f;   //弾の発射間隔
    [SerializeField] float shotTime = 0f;  
    #endregion


    #region var-EnemyInternal
    [Header("敵の移動関連")]
    [SerializeField] float enemyMoveSpeed = 3f;  //敵の移動速度
    Rigidbody2D enemyRB; //敵のリジッドボディ
    float enemyShotDelayReset = 0, enemyShotDelay = 0;  //弾の発射間隔の初期値,発射間隔の時間
    enum EnemyMoveType { Shot, Attack } //敵の動きの種類
    #endregion

   
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();  //敵のリジッドボディを取得
        //プレイヤーを取得
        //player=GameObject.FindWithtag("Player").GetComponet<Rigidbody2D>()
        //ゲームマネージャーを取得
        //gameManager=GameObject.FindTag("GameManager").GetComponent<GameManager>()
    }

    #region FixUpdate
    private void FixedUpdate()
    {
        //敵の移動:引数　　敵の移動種類
       // EnemyMove(moveType);
    }
    #endregion

    #region enemyShot
    //敵の弾の発射
    void EnemyShot(Transform firePos)
    {
        //弾のコピーっを生成
        GameObject bulletClone = Instantiate(enemyBullet, firePos.position, Quaternion.identity);
        //弾のコピーのRigidBodyにベクトルを与える
        bulletClone.GetComponent<Rigidbody2D>().velocity = transform.right* enemyShotSpeed;
    }
    #endregion


    //敵を動かす関数-----------------------
    void EnemyMove()         //(int type_Move)
    {
        // switch(type_Move)
        // {
        //     case (int)EnemyMoveType.Shot://敵がたまを撃ってくる
        //
        //
        //
        //     break;
        //
        //     case (int)EnemyMoveType.Attack://敵が動いてくる
        //
            enemyRB.velocity = new Vector2(-enemyMoveSpeed, 0);
        //     //enemyRB.velocity = new Vector2(-enemyMoveSpeed+player.transform.position.x, player.transform.position.y-transform.position.y);
        //
        //     break;
        //
        //     default:
        //         //該当なしの場合
        //     break;
        //
    }

    //敵の当たり判定
   // private void OnTriggerEnter2D(Collider2D collision)
   // {
   //     //プレイヤーと接触した場合
   //     if (collision.gameObject.CompareTag("Player"))
   //     {
   //         //自身を破棄
   //         Destroy(gameObject);
   //         //接触したプレイヤーを破棄
   //         Destroy(collision.gameObject);
   //     }
   //     //プレイヤーの弾と接触した場合
   //     else if (collision.gameObject.CompareTag("PlayerBullet"))
   //     {
   //         //自身を破棄
   //         Destroy(gameObject);
   //         //接触したプレイヤーの弾を破棄
   //         Destroy(collision.gameObject);
   //
   //     }
   // }
    // Update is called once per frame
    void Update()
    {
       //敵の移動
       //EnemyMove();

        //弾の発射----------------------
        enemyShotDelay += Time.deltaTime;

        //
        if (enemyShotDelay <= shotThrehold)
        {
            return;
        }
        shotTime += 1;
        //弾が発射される
         if (shotTime % 2 == 0)
         {
             EnemyShot(firePos);
         }
       // if (Input.GetKey(KeyCode.Space))
       // {
       //     EnemyShot(firePos);
       // }
        //弾の発射間隔のリセット
        enemyShotDelay = enemyShotDelayReset;

    }
}
