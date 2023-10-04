using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    // 맵에 존재할 최대 자원 수
    public int limitSize;
    public int curSize;
    private float rangeX = 40;
    private float rangeY = 35;
    void Update() {
        Spawn();
    }

    void Spawn() {
        if(curSize < limitSize) {
            GameObject go;

            go = PoolManager.instance.GetItemByRandom();

            go.transform.parent = gameObject.transform;
            go.transform.position = new Vector3(Random.Range(-rangeX, rangeX), 10, Random.Range(-rangeY, rangeY) - 5);

            curSize++;
        }
        
    }
}
