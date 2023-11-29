using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float visionRange = 10f;
    public string playerTag = "Player";
    public bool isChasingPlayer = false;
    public float moveSpeed = 5f;
    public float grabDistance = 2f;

    private Transform player;
    private bool isBeingGrabbed = false;

    private PlayerHealth playerHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (!isBeingGrabbed)
        {
            if (isChasingPlayer)
            {
                ChasePlayer();
            }
            else
            {
                DetectPlayer();
            }
        }
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRange);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag(playerTag))
            {
                // Player detected!
                StartChasingPlayer();
                break;
            }
        }
    }

    void StartChasingPlayer()
    {
        isChasingPlayer = true;

        // Stop the movement
        // Add your custom logic here for stopping the movement.

        // Notify other enemies to stop chasing if needed
        NotifyOtherEnemies();
    }

    void ChasePlayer()
    {
        // Check the distance between the player and the enemy
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= grabDistance)
        {
            // Player is close enough to grab the enemy
            GrabEnemy();
        }
        else
        {
            // Continue chasing the player
            // Add your custom logic for chasing the player.
            // Move towards the player using your internal movement logic.
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void NotifyOtherEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            if (enemy != this && enemy.isChasingPlayer)
            {
                enemy.StopChasingPlayer();
            }
        }
    }

    public void StopChasingPlayer()
    {
        isChasingPlayer = false;

        // Resume the movement
        // Add your custom logic here for resuming the movement.

        // Add any additional logic for stopping the chase.
    }

    void GrabEnemy()
    {
        // Access the player script to handle grabbing logic
        GrabObject playerGrabScript = player.GetComponent<GrabObject>();

        if (playerGrabScript != null)
        {
            // Notify the player script to grab the enemy
            playerGrabScript.GrabEnemy(this);

            // Set the enemy's state to being grabbed
            isBeingGrabbed = true;

            // Increase the enemy's mass to 100
            Rigidbody enemyRigidbody = GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                enemyRigidbody.mass = 1000f;
            }
        }
    }

    public void ReleaseEnemy()
    {
        // Access the player script to handle releasing logic
        GrabObject playerGrabScript = player.GetComponent<GrabObject>();

        if (playerGrabScript != null)
        {
            // Notify the player script to release the enemy
            playerGrabScript.ReleaseObject();

            // Set the enemy's state to not being grabbed
            isBeingGrabbed = false;

            // Reset the enemy's mass to its original value
            Rigidbody enemyRigidbody = GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                enemyRigidbody.mass = 1f;
            }
        }
    }
}
