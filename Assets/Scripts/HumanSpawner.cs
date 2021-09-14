using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    public GameObject Human;
    public Vector3 spawnPos;
    void Start()
    {
        InvokeRepeating("spawnHuman", 2f, 10f);
    }

    void spawnHuman()
    {
        Instantiate(Human, spawnPos, Quaternion.identity);
    }
}
