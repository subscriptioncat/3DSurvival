using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public GameObject[] prefabs;

    private List<GameObject>[] pools;

    private void Awake() {
        PoolManager.instance = this;

        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++) {
            pools[i] = new List<GameObject>();
        }
    }

    // 기존에 생성했지만 사용하지 않는 객체가 있나 찾아주고, 없다면 만들어서 주는 함수
    public GameObject GetItemWithIndex(int index) {
        GameObject select = null;

        foreach (var item in pools[index]) {
            if(!item.activeSelf) {
                select = item;

                select.SetActive(true);
                break;
            }
        }

        if(!select) {
            select = Instantiate(prefabs[index]);
            pools[index].Add(select);
        }

        return select;
    }

    public GameObject GetItemByRandom() {
        return GetItemWithIndex(Random.Range(0, prefabs.Length));
    }
}
