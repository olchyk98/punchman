using System;
using System.Collections.Generic;
using Gameplay;
using NUnit.Framework;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private static List<GameObject> players = new List<GameObject>();
    private Transform myTransform;

    private void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    public static void AddPlayer(GameObject aPlayerInstance)
    {
        players.Add(aPlayerInstance);
    }

    private void Update()
    {
        if (players.Count < 2) return;
        var player1 = players[0].transform.position;
        var player2 = players[1].transform.position;
        myTransform.position = Vector3.Lerp(player1, player2, 0.5f);
    }
}
