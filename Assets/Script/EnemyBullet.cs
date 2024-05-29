using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
