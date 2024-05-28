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
    Rigidbody2D enemyRB; //�G�̃��W�b�h�{�f�B
    float enemyShotDelayReset = 0, enemyShotDelay = 0;  //�e�̔��ˊԊu�̏����l,���ˊԊu�̎���
    enum EnemyMoveType { Shot, Attack } //�G�̓����̎��
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

    #region FixUpdate
    private void FixedUpdate()
    {
        //�G�̈ړ�:�����@�@�G�̈ړ����
       // EnemyMove(moveType);
    }
    #endregion

    #region enemyShot
    //�G�̒e�̔���
    void EnemyShot(Transform firePos)
    {
        //�e�̃R�s�[���𐶐�
        GameObject bulletClone = Instantiate(enemyBullet, firePos.position, Quaternion.identity);
        //�e�̃R�s�[��RigidBody�Ƀx�N�g����^����
        bulletClone.GetComponent<Rigidbody2D>().velocity = transform.right* enemyShotSpeed;
    }
    #endregion


    //�G�𓮂����֐�-----------------------
    void EnemyMove()         //(int type_Move)
    {
        // switch(type_Move)
        // {
        //     case (int)EnemyMoveType.Shot://�G�����܂������Ă���
        //
        //
        //
        //     break;
        //
        //     case (int)EnemyMoveType.Attack://�G�������Ă���
        //
            enemyRB.velocity = new Vector2(-enemyMoveSpeed, 0);
        //     //enemyRB.velocity = new Vector2(-enemyMoveSpeed+player.transform.position.x, player.transform.position.y-transform.position.y);
        //
        //     break;
        //
        //     default:
        //         //�Y���Ȃ��̏ꍇ
        //     break;
        //
    }

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
       //�G�̈ړ�
       //EnemyMove();

        //�e�̔���----------------------
        enemyShotDelay += Time.deltaTime;

        //
        if (enemyShotDelay <= shotThrehold)
        {
            return;
        }
        shotTime += 1;
        //�e�����˂����
         if (shotTime % 2 == 0)
         {
             EnemyShot(firePos);
         }
       // if (Input.GetKey(KeyCode.Space))
       // {
       //     EnemyShot(firePos);
       // }
        //�e�̔��ˊԊu�̃��Z�b�g
        enemyShotDelay = enemyShotDelayReset;

    }
}
