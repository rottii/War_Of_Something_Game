using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private int damage = 50;
    bool isEnemy = false;
    public float destroyRange = 9.5f;
    void Start()
    {
        if (gameObject.tag == "Enemy Projectile")
            isEnemy = true;
    }

    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        if (transform.position.x > destroyRange || transform.position.x < -destroyRange)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isEnemy)
        {
            Soldier soldierScript = other.GetComponent<Soldier>();
            soldierScript.health -= damage;
            Destroy(gameObject);
        }
        if (other.CompareTag("Player") && isEnemy)
        {
            Soldier soldierScript = other.GetComponent<Soldier>();
            soldierScript.health -= damage;
            Destroy(gameObject);
        }

    }
}
