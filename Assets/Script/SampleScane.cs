using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScane : MonoBehaviour
{
    GameObject enemyManager;
    private bool isClear;
    public string nextSceneName;
    //Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
        isClear = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyManager.GetComponent<EnemyManager>().IsClear()==true && isClear==false)
        {
            FadeManager.Instance.LoadScene(nextSceneName, 1f);
            isClear=true;
        }
        
    }
}
