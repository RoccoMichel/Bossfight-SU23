using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [Header("QTE related")]
    public float increaseRate = 0.2f;
    public float decreaseRate = 0.4f;
    public void OnPlayerEnter()
    {
        GameController.instance.InstantiateQTE().Set(increaseRate, decreaseRate, new Action[] { Fire });
    }

    public void Fire()
    {
        if (boss == null) boss = FindAnyObjectByType<Boss>().GetComponent<Boss>();
        
        boss.Health -= 1;
    }
}
