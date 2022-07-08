using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttatck : MonoBehaviour
{
public Transform attachPoint;
public Transform Weapon;

void Start()
{
    Weapon.parent = attachPoint;
    Weapon.position = attachPoint.position;
    Weapon.rotation = attachPoint.rotation;
}
}
