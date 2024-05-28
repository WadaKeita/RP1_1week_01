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
    //[SerializeField] float shotTime = 0f;  
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
    #endregion

   
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();  //�G�̃��W�b�h�{�f�B���擾
        //�v���C���[���擾
        //player=GameObject.FindWithtag("Player").GetComponet<Rigidbody2D>()
        //�Q�[���}�l�[�W���[���擾
        //gameManager=GameObject.FindTag("GameManager").GetComponent<GameManager>()
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
    //�G�̓����蔻��
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     //�v���C���[�ƐڐG�����ꍇ
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         //���g��j��
    //         Destroy(gameObject);
    //         //�ڐG�����v���C���[��j��
    //         Destroy(collision.gameObject);
    //     }
    //     //�v���C���[�̒e�ƐڐG�����ꍇ
    //     else if (collision.gameObject.CompareTag("PlayerBullet"))
    //     {
    //         //���g��j��
    //         Destroy(gameObject);
    //         //�ڐG�����v���C���[�̒e��j��
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
               

                //�e�̔���----------------------
                enemyShotDelay += Time.deltaTime;
                TypeTime += Time.deltaTime;

                //�e�����˂����
                if (enemyShotDelay >= shotThrehold)
                {
                    EnemyShot(firePos);
                    //�e�̔��ˊԊu�̃��Z�b�g
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

                //�G�̈ړ�
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
