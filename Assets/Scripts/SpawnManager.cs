using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 2f, 2f);
    }

    void Spawn()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null && player.GetComponent<PlayerController>().gameOver) return;

        int randomType = Random.Range(0, 3);

        GameObject obstacle = ObstacleObjectPool.staticInstance.Acquire(randomType);

        if (obstacle != null)
        {
            obstacle.transform.position = spawnPoint.position;
            obstacle.GetComponent<MoveLeft>().obstacleType = randomType;
        }
    }
}