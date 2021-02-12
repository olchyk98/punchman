using System;
using UnityEngine;

[Serializable]
public struct Attack
{ 
    public Vector2 punchDirection; // Left to right.
    public float damage;
    public float cooldown;
    public float animationCooldown;
    public string playerAnimationTrigger;
}
