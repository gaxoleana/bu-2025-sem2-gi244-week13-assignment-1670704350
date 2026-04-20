using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    public static ObstacleObjectPool staticInstance;

    void Awake()
    {
        staticInstance = this;
        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();

        InitializePool(obstacleBarrelPool, obstacleBarrelPrefab);
        InitializePool(obstacleBarrierPool, obstacleBarrierPrefab);
        InitializePool(obstacleStoneWallPool, obstacleStoneWallPrefab);
    }

    private void InitializePool(List<GameObject> pool, GameObject prefab)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, new Vector3(0, -100, 0), prefab.transform.rotation);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject Acquire(int obstacleType)
    {
        List<GameObject> targetPool = GetPoolByType(obstacleType);
        if (targetPool.Count > 0)
        {
            GameObject obj = targetPool[0];
            targetPool.RemoveAt(0);
            obj.SetActive(true);
            return obj;
        }
        return Instantiate(GetPrefabByType(obstacleType));
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        obstacle.SetActive(false);
        GetPoolByType(obstacleType).Add(obstacle);
    }

    private List<GameObject> GetPoolByType(int type) => type switch
    {
        0 => obstacleBarrelPool,
        1 => obstacleBarrierPool,
        _ => obstacleStoneWallPool
    };

    private GameObject GetPrefabByType(int type) => type switch
    {
        0 => obstacleBarrelPrefab,
        1 => obstacleBarrierPrefab,
        _ => obstacleStoneWallPrefab
    };
}