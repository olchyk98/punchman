using System;
using System.Collections.Generic;
using Gameplay;
using NUnit.Framework;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private static GameObject[] players = new GameObject[2];
    private Transform myTransform;

    private void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    public static void SetPlayer(int i, GameObject aPlayerInstance)
    {
        players[i] = aPlayerInstance;
    }

    private void Update()
    {
        if (players.Length < 2) return;
        var player1 = players[0].transform.position;
        var player2 = players[1].transform.position;
        var newPosition = Vector3.Lerp(player1, player2, 0.5f);
        newPosition.z = -10;
        myTransform.position = newPosition;
    }
}
