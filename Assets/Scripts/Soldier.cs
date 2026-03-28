using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private GameObject projectile; //projectile(bullet, arrow, rocket, etc.)

    private Rigidbody2D rb;
    private char state = 'w'; //state of the soldier(fighting(f), walking(w), etc.)
    bool isEnemy = false;

    public SoldierData unitData;

    //Unit stats
    public int health;
    private float speed;
    private float damage;

    [SerializeField] private int scoreChangeAmount = 2;//amount of score change when the soldier passes the border
    [SerializeField] private float attackCooldown = 2.0f;//attack cooldown in seconds
    private float timeSinceLastShoot = 0;

    public int laneIndex;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (gameObject.tag == "Enemy") isEnemy = true;
        
        laneIndex = Mathf.RoundToInt(3.5f - transform.position.y);//3.5 is the top lane
        LaneManager.Instance.AddUnitToLane(laneIndex, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 'w')
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if (state == 'f' && timeSinceLastShoot > attackCooldown)
        {
            Shoot();
            timeSinceLastShoot = 0;
        }
        timeSinceLastShoot += Time.deltaTime;


        if (health <= 0)
            Destroy(gameObject);
        if (!isEnemy && transform.position.x > 9.0f)
        {
            GameManager.Instance.ChangeScore(scoreChangeAmount);
            Destroy(gameObject);
        }
        if (isEnemy && transform.position.x < -9.0f)
        {
            GameManager.Instance.ChangeScore(-scoreChangeAmount);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (LaneManager.Instance != null)
        {
            LaneManager.Instance.RemoveUnitFromLane(laneIndex, gameObject);
        }
    }

    void Shoot()
    {
        GameObject newUnit = Instantiate(projectile, transform.position, transform.rotation);
        newUnit.tag = gameObject.tag + " Projectile";
    }

    private int enemyCounter = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isEnemy)
        {
            enemyCounter++;
        }
        if (other.CompareTag("Player") && isEnemy)
        {
            enemyCounter++;
        }
        if (enemyCounter > 0)
            state = 'f';
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isEnemy)
        {
            enemyCounter--;
        }
        if (other.CompareTag("Player") && isEnemy)
        {
            enemyCounter--;
        }
        if (enemyCounter == 0)
            state = 'w';
    }
}
