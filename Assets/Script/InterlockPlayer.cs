using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterlockPlayer : MonoBehaviour
{
    GameObject player;

    private Vector3 prePosition;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = null;
        if (player == null)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
        }
        foreach (GameObject obj in players)
        {
            player = obj;
        }
        prePosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position != prePosition)
        {
            this.transform.position += player.transform.position - prePosition;

            prePosition = player.transform.position;
        }
    }
}
