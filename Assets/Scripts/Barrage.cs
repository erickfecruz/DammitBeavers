using UnityEngine;

public class Barrage : MonoBehaviour
{
    [SerializeField]
    public GameObject waterGameObject;

    public float minSpawnTime = 2f;
    public float maxSpawnTime = 15f;

    public float actualTime = 0;
    public float nextSpawnTimer = 0;

    void Start() {
        nextSpawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void FixedUpdate() {
        actualTime = actualTime + Time.fixedDeltaTime;

        if (actualTime > nextSpawnTimer) {
            SpawnEvent();
            nextSpawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
            actualTime = 0;
        }
    }

    void SpawnEvent() {
        GameObject waterObj = Instantiate(waterGameObject, transform.position, Quaternion.identity);
        waterObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -15f);
    }
}