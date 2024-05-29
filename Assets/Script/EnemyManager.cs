using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject[] enemys; // 敵を取得する用

    private bool isClear; // クリア判定

    //// EnemyManagerをtagから取得し、IsClear関数を呼び出すことでクリア判定を取れる
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

        enemys = GameObject.FindGameObjectsWithTag("Enemy"); // tagからEnemyを取得

        if(enemys.Length == 0 && isClear == false) // Enemyの数が0の時クリア
        {
            isClear = true;
            //Debug.Log("clear!");
        }
    }
}
