using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    #region var-EnemyShot
    [Header("�G�̒e�֘A")]
    [SerializeField] float enemyShotSpeed = 2f; //�G�̒e�̔��˃X�s�[�h
    [SerializeField] GameObject enemyBullet;   //�G�̃��W�b�h�{�f�B
    [SerializeField] Transform firePos;         //�G�̒e�̔��˃|�C���g
    [SerializeField] float shotThrehold = 1f;   //�e�̔��ˊԊu
    [SerializeField] float shotTime = 0f;  
    #endregion


    #region var-EnemyInternal
    [Header("�G�̈ړ��֘A")]
    [SerializeField] float enemyMoveSpeed = 3f;  //�G�̈ړ����x
    [Header("�U���^�C�v���ς�鎞�̃^�C�}�[")]
    [SerializeField] float TypeTime = 0f;  //���Ԃ��o������shot����Attack�ɐ؂�ւ���ϐ�
    //[SerializeField] float attackTypeTime = 0f;
   // [SerializeField]  bool isFinish = false;
    //[SerializeField]  bool isAttack = false;
    Rigidbody2D enemyRB; //�G�̃��W�b�h�{�f�B
    float enemyShotDelayReset = 0, enemyShotDelay = 0;  //�e�̔��ˊԊu�̏����l,���ˊԊu�̎���
    enum EnemyMoveType { Shot, Attack } //�G�̓����̎��
    EnemyMoveType type = EnemyMoveType.Shot;
    [SerializeField] private Transform _LeftEdge;
    [SerializeField] private Transform _RightEdge;
    private int direction = 1;

    //--------------------------------------------------------------------------------------
    int enemyHpMin = 0;     //�G�̍ŏ�HP
    public static int enemyAttackDamage { set; get; } //�G�{�̂��v���C���[�ɗ^����_���[�W
    public static int enemyShotPower = 1;  //�e�̋���
    enum EnemyDamagetype { heal,damage} //�G��HP�̌v�Z���@
    int attackDamage = 1;
    //enum EnemyAttackDamage{Normal=1,PlayerFollowY=2,PlayerAttack=3}   //�v���C���[�ɗ^����_���[�W
    
    #endregion

    #region var-EnemyHp
    [Header("�G��HP")]
    [SerializeField] int enemyHp = 10;
    [Header("�{�̐ڐG���ɗ^����_���[�W")]
    [SerializeField] int enemyDamage = 1;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();  //�G�̃��W�b�h�{�f�B���擾
        //�v���C���[���擾
        //player=GameObject.FindWithtag("Player").GetComponet<Rigidbody2D>()
        //�Q�[���}�l�[�W���[���擾
        //gameManager=GameObject.FindTag("GameManager").GetComponent<GameManager>()

        //�G��HP
        enemyAttackDamage = attackDamage;
    }

    #region enemyShot
    //�G�̒e�̔��˃R�s�[�Ƃ�
    void EnemyShot(Transform firePos)
    {
        //�e�̃R�s�[���𐶐�
        GameObject bulletClone = Instantiate(enemyBullet, firePos.position, Quaternion.identity);
        //�e�̃R�s�[��RigidBody�Ƀx�N�g����^����
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
    //�G�̓����蔻��
    //  void OnTriggerEnter2D(Collider2D collision)
    // {
    //     //�v���C���[�ƐڐG�����ꍇ
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         //�GHP�̊Ǘ�
               //EnemyHpChanged((int)EnemyDmageType.damage,enemyHp,collision);
    //     }
    //     //�v���C���[�̒e�ƐڐG�����ꍇ
    //     else if (collision.gameObject.CompareTag("PlayerBullet"))
    //     {
    //        //�G��Hp�Ǘ�
              //EnemyHpChanged((int)EnemyDmageType.damage,PlayerController.playerShotPower,collision);
    //
    //     }
    // }
    // Update is called once per frame

   //void EnemyHpChanged(int damageType,int volume,Collider2D collider)
   //{
   //    //�_���[�W�̎�ނŕ���
   //    switch (damageType)
   //    {
   //        //�񕜂̏ꍇ(���ɂȂ�)
   //        case (int)EnemyDamagetype.heal:
   //            //�G��HP�񕜏���
   //
   //        break;
   //
   //        //�_���[�W�̏ꍇ
   //        case (int)EnemyDamagetype.damage:
   //            //�_���[�W�������Z
   //            enemyHp -= volume;
   //
   //            //�ڐG�I�u�W�F�N�g���v���C���[�̒e�̏ꍇ
   //            if (collider.gameObject.CompareTag("PlayerBullet"))
   //            {
   //                //�I�u�W�F�N�g��j��
   //                Destroy(collider.gameObject);
   //            }
   //            //�G��HP���ŏ��l�ȉ��ɂȂ�ꍇ
   //            if(enemyHp<=enemyHpMin)
   //            {
   //                //�G�������鏈��
   //            }
   //            break;
   //
   //        //�Y���Ȃ�
   //        default:
   //            break;
   //    }
   //}

    void Update()
    {

      
        switch (type)
        {
            case EnemyMoveType.Shot:
               

                //�e�̔���----------------------
                enemyShotDelay += 1.0f*Time.deltaTime;
                TypeTime += 1.0f*Time.deltaTime;

                //�e�����˂����
                if (enemyShotDelay >= shotThrehold)
                {
                    EnemyShot(firePos);
                    //�e�̔��ˊԊu�̃��Z�b�g
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

                //�G�̈ړ�
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
