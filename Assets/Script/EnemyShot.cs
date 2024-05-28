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
    //[SerializeField] float shotTime = 0f;  
    #endregion


    #region var-EnemyInternal
    [Header("敵の移動関連")]
    [SerializeField] float enemyMoveSpeed = 3f;  //敵の移動速度
    [Header("攻撃タイプが変わる時のタイマー")]
    [SerializeField] float TypeTime = 0f;  //時間が経ったらshotからAttackに切り替える変数
    //[SerializeField] float attackTypeTime = 0f;
   // [SerializeField]  bool isFinish = false;
    //[SerializeField]  bool isAttack = false;
    Rigidbody2D enemyRB; //敵のリジッドボディ
    float enemyShotDelayReset = 0, enemyShotDelay = 0;  //弾の発射間隔の初期値,発射間隔の時間
    enum EnemyMoveType { Shot, Attack } //敵の動きの種類
    EnemyMoveType type = EnemyMoveType.Shot;
    [SerializeField] private Transform _LeftEdge;
    [SerializeField] private Transform _RightEdge;
    private int direction = 1;
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

    #region enemyShot
    //敵の弾の発射コピーとか
    void EnemyShot(Transform firePos)
    {
        //弾のコピーっを生成
        GameObject bulletClone = Instantiate(enemyBullet, firePos.position, Quaternion.identity);
        //弾のコピーのRigidBodyにベクトルを与える
        bulletClone.GetComponent<Rigidbody2D>().velocity = transform.right* enemyShotSpeed;
    }
    #endregion

    void EnemyMove()         //(int type_Move)
    {

        //enemyRB.velocity = transform.right * enemyMoveSpeed;
        if (transform.position.x >= Camera.main.ViewportToWorldPoint(new Vector2(1,0)).x)
        {
            if (transform.rotation.z < 0)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
        }
        if (transform.position.x <= Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x)
        {
            if (transform.rotation.z < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
        }

        enemyRB.velocity = transform.right * enemyMoveSpeed * direction;

    }

    //
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

      
        switch (type)
        {
            case EnemyMoveType.Shot:
               

                //弾の発射----------------------
                enemyShotDelay += Time.deltaTime;
                TypeTime += Time.deltaTime;

                //弾が発射される
                if (enemyShotDelay >= shotThrehold)
                {
                    EnemyShot(firePos);
                    //弾の発射間隔のリセット
                    enemyShotDelay = enemyShotDelayReset;
                }

                if (TypeTime >= 8)
                   {
                   type = EnemyMoveType.Attack;
                   TypeTime = 0;
                   }
            
            break;

            case EnemyMoveType.Attack:
                TypeTime += Time.deltaTime;

                //敵の移動
                EnemyMove();

                if (TypeTime >= 7)
                {
                 type = EnemyMoveType.Shot;
                 TypeTime = 0;
                    enemyRB.velocity = Vector3.zero;
                }


               break;


        }
    }
}
