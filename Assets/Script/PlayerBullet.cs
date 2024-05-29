using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBullet : MonoBehaviour
{
    Rigidbody2D rb;

    private bool isShot = false;
    private float shotPower = 5;

    private GameObject playerArea;


    // �N���A��������p
    GameObject enemyManager;
    bool isClear = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerArea = GameObject.FindGameObjectWithTag("PlayerArea");

        // Area�Ɛe�q�֌W����������
        this.transform.parent = playerArea.transform;


        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager");

        isClear = false;
    }

    private Vector3 ShotDirection()
    {
        Vector3 direction = Vector3.zero;
        if (playerArea.transform.position != this.transform.position)
        {
            direction = this.transform.position - playerArea.transform.position;
            direction = direction.normalized;
        }
        return direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isClear == false )
        {
            // Enter�L�[�Ŕ���
            if (isShot == false && Input.GetKeyDown(KeyCode.Return) ||
            isShot == false && Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                rb.velocity = ShotDirection() * shotPower;
                isShot = true;

                // �e�q�֌W������
                this.transform.parent = null;
            }

            // ��ʊO�֏o����폜���鏈��
            if (isShot == true)
            {
                if (transform.position.x - transform.localScale.x / 2 >= Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x)
                {
                    Destroy(gameObject);
                }
                if (transform.position.x + transform.localScale.x / 2 <= Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x)
                {
                    Destroy(gameObject);
                }
                if (transform.position.y - transform.localScale.y / 2 >= Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y)
                {
                    Destroy(gameObject);
                }
                if (transform.position.y + transform.localScale.y / 2 <= Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y)
                {
                    Destroy(gameObject);
                }
            }
        }

        isClear = enemyManager.GetComponent<EnemyManager>().IsClear();
    }
}
