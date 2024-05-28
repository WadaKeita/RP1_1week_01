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

    //--------------------------------------------------------------------------------------
    int enemyHpMin = 0;     //敵の最小HP
    public static int enemyAttackDamage { set; get; } //敵本体がプレイヤーに与えるダメージ
    public static int enemyShotPower = 1;  //弾の強さ
    enum EnemyDamagetype { heal,damage} //敵のHPの計算方法
    int attackDamage = 1;
    //enum EnemyAttackDamage{Normal=1,PlayerFollowY=2,PlayerAttack=3}   //プレイヤーに与えるダメージ
    
    #endregion

    #region var-EnemyHp
    [Header("敵のHP")]
    [SerializeField] int enemyHp = 10;
    [Header("本体接触時に与えるダメージ")]
    [SerializeField] int enemyDamage = 1;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();  //敵のリジッドボディを取得
        //プレイヤーを取得
        //player=GameObject.FindWithtag("Player").GetComponet<Rigidbody2D>()
        //ゲームマネージャーを取得
        //gameManager=GameObject.FindTag("GameManager").GetComponent<GameManager>()

        //敵のHP
        enemyAttackDamage = attackDamage;
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
        if (transform.position.x + transform.localScale.x / 2 >= Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x)
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
        if (transform.position.x <= _LeftEdge.position.x)
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
    //  void OnTriggerEnter2D(Collider2D collision)
    // {
    //     //プレイヤーと接触した場合
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         //敵HPの管理
               //EnemyHpChanged((int)EnemyDmageType.damage,enemyHp,collision);
    //     }
    //     //プレイヤーの弾と接触した場合
    //     else if (collision.gameObject.CompareTag("PlayerBullet"))
    //     {
    //        //敵のHp管理
              //EnemyHpChanged((int)EnemyDmageType.damage,PlayerController.playerShotPower,collision);
    //
    //     }
    // }
    // Update is called once per frame

   //void EnemyHpChanged(int damageType,int volume,Collider2D collider)
   //{
   //    //ダメージの種類で分岐
   //    switch (damageType)
   //    {
   //        //回復の場合(特になし)
   //        case (int)EnemyDamagetype.heal:
   //            //敵のHP回復処理
   //
   //        break;
   //
   //        //ダメージの場合
   //        case (int)EnemyDamagetype.damage:
   //            //ダメージ分を減算
   //            enemyHp -= volume;
   //
   //            //接触オブジェクトがプレイヤーの弾の場合
   //            if (collider.gameObject.CompareTag("PlayerBullet"))
   //            {
   //                //オブジェクトを破棄
   //                Destroy(collider.gameObject);
   //            }
   //            //敵のHPが最小値以下になる場合
   //            if(enemyHp<=enemyHpMin)
   //            {
   //                //敵が消える処理
   //            }
   //            break;
   //
   //        //該当なし
   //        default:
   //            break;
   //    }
   //}

    void Update()
    {

      
        switch (type)
        {
            case EnemyMoveType.Shot:
               

                //弾の発射----------------------
                enemyShotDelay += 1.0f*Time.deltaTime;
                TypeTime += 1.0f*Time.deltaTime;

                //弾が発射される
                if (enemyShotDelay >= shotThrehold)
                {
                    EnemyShot(firePos);
                    //弾の発射間隔のリセット
                    enemyShotDelay = enemyShotDelayReset;
                }

                if (TypeTime >= 10)
                   {
                   type = EnemyMoveType.Attack;
                   TypeTime = 0;
                   }
            
            break;

            case EnemyMoveType.Attack:
                TypeTime += 1.0f * Time.deltaTime;

                //敵の移動
                EnemyMove();

                if (TypeTime >= 10)
                {
                 type = EnemyMoveType.Shot;
                 TypeTime = 0;
                    enemyRB.velocity = Vector3.zero;
                }


               break;


        }
    }
}
