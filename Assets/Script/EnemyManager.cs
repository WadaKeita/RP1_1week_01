using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject[] enemys; // �G���擾����p

    private bool isClear; // �N���A����

    //// EnemyManager��tag����擾���AIsClear�֐����Ăяo�����ƂŃN���A���������
    // enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
    // isClear = false;
    // enemyManager.GetComponent<EnemyManager>().IsClear();

    // Start is called before the first frame update
    void Start()
    {
        isClear = false;
    }

    public bool IsClear()
    {
        return isClear;
    }

    // Update is called once per frame
    void Update()
    {

        enemys = GameObject.FindGameObjectsWithTag("Enemy"); // tag����Enemy���擾

        if(enemys.Length == 0 && isClear == false) // Enemy�̐���0�̎��N���A
        {
            isClear = true;
            //Debug.Log("clear!");
        }
    }
}
