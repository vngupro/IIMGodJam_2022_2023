using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class EnemyAgent : ScriptableObject
{
    public float health = 1.0f;
    public float damage = 10.0f;
    public float maxDamage = 20.0f;
    public float speed = 10.0f;
    public float maxSpeed = 20.0f;
    public AnimationCurve speedCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float rotationSpeed = 100.0f;
    public float detectionRadius = 10.0f;

    public Sprite sprite;
    public Sprite spriteAttack;
    public Sprite spriteTakeDamage;
    public Sprite spriteOnDeath;

    public virtual void OnDestroy()
    {
        //Feedback
    }
}