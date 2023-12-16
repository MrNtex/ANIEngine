using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] spawnableObjects;
    public Vector2 spawnPoint = new Vector2(0,0);
    public void SpawnObject(int index)
    {
        Instantiate(spawnableObjects[index], spawnPoint, Quaternion.identity);
    }
}
