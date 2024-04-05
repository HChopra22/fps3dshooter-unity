using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

//The complete class to handle all AI behaviour
public class AIBehaviour : MonoBehaviour
{
    [Header ("Zombie Movement")]
    private GameObject fps;
    public float fov = 120f;
    public float wanderSpeed = 3f;
    private Vector3 wanderPoint;
    public float chaseSpeed = 20f;
    Vector3 velocity;

    [Header("Zombie Health")]
    public float maxZombieHealth = 100f;
    private float currentZombieHealth;
    private float damage = 20;

    [Header("Zombie Attack")]
    float lastZombieAttack = 0;
    float attackCooldown = 2;
    public GameObject rightFist;
    private GameObject target;
    public HealthBarScript zhealthBar;

    [Header("Zombie State")]
    private float fovDistance = 100f;
    private bool zombieAware = false;
    private NavMeshAgent agent;
    private float wanderRadius = 100f;

    [Header("Ground Variables")]
    public Transform isGrounded;
    private float groundDistance = 0.4f;
    public LayerMask groundedMask;
    bool grounded;

    [Header("Graphics Variables")]
    private Animator animator;
    public AudioClip zombieHit;
    private AudioSource audioSource;

    [Header("Score Variables")]
    GameController gameController;
    private float scoreAddAmount = 10;
    Spawner zSpawn;

    //set the current health of a zombie to max
    //apoint all of the logic from other scripts at the start of the script, using tags and get component
    public void Start()
    {
        currentZombieHealth = maxZombieHealth;
        
        agent = GetComponent<NavMeshAgent>();
        wanderPoint = wanderZombie();
        animator = GetComponentInChildren<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        fps = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        zSpawn = gameController.GetComponentInChildren<Spawner>();
    }
    
    /*if the zombie health is 0 then begin the death animation
     * ensure the zombie is grounded and chek if the zombie is aware or not,
     if the zombie is aware, change the state and play the corresponding animation and run the attack method*/ 
    void Update()
    {   
        if (currentZombieHealth <= 0)
        {
            agent.speed = 0;
            FindObjectOfType<AudioManager>().Play("ZombieDie");
            animator.SetBool("Death", true);
            StartCoroutine(Die());
            return;
        }
        
        grounded = Physics.CheckSphere(isGrounded.position, groundDistance, groundedMask);

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (zombieAware)
        {
            agent.SetDestination(fps.transform.position);
            agent.speed = chaseSpeed;
            animator.SetBool("Chase", true);
            Attack();
        }
        else
        {
            findPlayer();
            Wander();
            animator.SetBool("Chase", false);
            animator.SetBool("Attack", false);
            agent.speed = wanderSpeed;
        }
    }

    //if the fov of the zombie is within range of the user or the zombie is in a radius of the user then it is aware
    public void findPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(fps.transform.position)) < fov /2f)
        {
            if(Vector3.Distance(fps.transform.position, transform.position) < fovDistance)
            {
                OnAware();
            }
        }
        else if (Vector3.Distance(fps.transform.position, transform.position) < 20)
        {
            OnAware();
        }
    }

    //set the collider on the zombies right fist to active
    public void zombieAttack ()
    {
        rightFist.GetComponent<Collider>().enabled = true;
    }

    //set the collider on the zombies right fist to active
    public void deactivateAttack()
    {
        rightFist.GetComponent<Collider>().enabled = false;
    }

    //when the distance between the user and zombie is less than 2f, pause the zombie ans start the attack animation
    //attacks can only happen in 2 second intervals, if the player is in the attack range, cause damage and update it
    public void Attack()
    {
        float distance = Vector3.Distance(fps.transform.position, transform.position);
        if (distance < 2f)
        {
            agent.speed = 0;
            animator.SetBool("Attack", true);

            if (Time.time - lastZombieAttack >= attackCooldown)
            {
                lastZombieAttack = Time.time;
                target.GetComponent<PlayerStats>().takeDamage(damage);
                target.GetComponent<PlayerStats>().realtimeHealth();
                audioSource.PlayOneShot(zombieHit);
            }
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

        public void OnAware ()
    {
        zombieAware = true;
    }

    //If the zombie is dead, wait for x seconds to play the animation
    //add to the spawner variable to handle waves, then destroy the zombie from scene and add score
    IEnumerator Die()
    {
        yield return new WaitForSeconds(4.3f);
        zSpawn.zombiesKilled++;
        Destroy(gameObject);
        gameController.AddScore(scoreAddAmount);
    }

    //if the zombie is x distance away and hasnt been triggered already, wander to different points
    public void Wander()
    {
        if (Vector3.Distance(transform.position, wanderPoint) < 8f)
        {
            wanderPoint = wanderZombie();
        }
        else
        {
            agent.SetDestination(wanderPoint);
        }
    }

    //if the zombie gets shot at, update the zombie health and healthbar
    public void shootZombie (float damage)
    {
        currentZombieHealth -= damage;
        zhealthBar.setHealth(currentZombieHealth);

    }

    //wanderpoint logic for the zombies, when one waypoint is met, move to another
    public Vector3 wanderZombie ()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, navHit.position.y, navHit.position.z);
    }

}
