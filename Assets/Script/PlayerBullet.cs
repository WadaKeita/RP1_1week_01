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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerArea = GameObject.FindGameObjectWithTag("PlayerArea");

        // Areaと親子関係を持たせる
        this.transform.parent = playerArea.transform;
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
        // Enterキーで発射
        if (isShot == false && Input.GetKeyDown(KeyCode.Return) ||
            isShot == false && Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            rb.velocity = ShotDirection() * shotPower;
            isShot = true;

            // 親子関係を解除
            this.transform.parent = null;
        }
    }
}
