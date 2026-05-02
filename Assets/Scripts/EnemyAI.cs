using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public float enemyMovementSpeed = 2f;
    public float enemyChaseSpeed = 2f;
    private float playerDistance;
    public Transform playerTransform;
    private bool chasing = false;
    public Transform leftWall;
    public Transform rightWall;
    private Transform patrolTarget;
    public bool takeDamageAllowed = true;
    public float health = 3;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        patrolTarget = rightWall;
        print("HP: " + health);
    }

   
    void Update()
    {


        playerDistance = Vector2.Distance(transform.position, playerTransform.position);

        if (playerDistance < 1.1f)
        {
            StopMoving();
            Attack();
            return;
        }

       
        if (playerDistance < 5f)
        {
            
            Vector2 targetposition = new Vector2(playerTransform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetposition, enemyChaseSpeed * Time.deltaTime);
            return;
        }
       
            Vector2 patroltarget = new Vector2(patrolTarget.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, patroltarget, enemyMovementSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patroltarget) < 0.1f)
            {
                if (patrolTarget == rightWall) patrolTarget = leftWall; else patrolTarget = rightWall;
            }
        }

    

    void Attack()
    {
        if (takeDamageAllowed == true)
        {
            print("Enemy attacks player!");
            health--;
            print("HP: " + health);
            takeDamageAllowed = false;

            StartCoroutine(DamageCooldown());
        }
    }

    void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
    }

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(1);
        takeDamageAllowed = true;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, playerDistance);
    }

}
