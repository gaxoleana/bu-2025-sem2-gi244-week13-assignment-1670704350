using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10f;
    public int obstacleType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnEnable()
    {
        speed = 10f; // รีเซ็ตความเร็วให้กลับมาวิ่งได้ใหม่
    }

    // Update is called once per frame
    void Update()
    {
        // 1.17 stop moving left when the game is over
        GameObject player = GameObject.Find("Player");
        if (player != null && player.GetComponent<PlayerController>().gameOver)
        {
            speed = 0;
            return;
        }

        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        if (transform.position.x < -15 && gameObject.CompareTag("Obstacle"))
        {
            ObstacleObjectPool.staticInstance.Release(gameObject, obstacleType);
        }
    }
}