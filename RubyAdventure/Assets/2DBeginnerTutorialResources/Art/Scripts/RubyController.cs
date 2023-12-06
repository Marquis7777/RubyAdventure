using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    
    public int maxHealth = 5;

    public int maxSpeed = 6;

    float changeTime = 5.0f;

    public GameObject projectilePrefab;
    public GameObject loseTextObject;
    public GameObject Explosion;
    public GameObject RubyDeath;
    public GameObject ExplosionSound;
    public GameObject JambiSound;
    public GameObject dogsound;
    public GameObject Drinking;
    public GameObject losemusic;
    public GameObject BackgroundMusic;

    public bool SpeedBoost = false;

    public AudioClip throwSound;
    public AudioClip hitSound;
    
    public int health { get { return currentHealth; }}
    int currentHealth;
    
    public float timeInvincible = 2.0f;
    public ParticleSystem DamagedEffect;
    public ParticleSystem HealthEffect;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        loseTextObject.SetActive(false);
        RubyDeath.SetActive(false);
        ExplosionSound.SetActive(false);
        JambiSound.SetActive(false);
        Drinking.SetActive(false);
        currentHealth = maxHealth;
        losemusic.SetActive(false);
        //currentSpeed = Speed;
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (currentHealth == 0)
            {
                loseTextObject.SetActive(true);
                rigidbody2d.simulated = false;
                animator.SetTrigger("Dead");
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                RubyDeath.SetActive(true);
                ExplosionSound.SetActive(true);
                losemusic.SetActive(true);
                Destroy(BackgroundMusic);
        }

         //If game is over
        if (loseTextObject)
        {
            //If R is hit, restart the current scene
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Load the level named "MainScene".
        Application.LoadLevel("MainScene");
            }
            
            //If Q is hit, quit the game
            if (Input.GetKeyDown(KeyCode.Q))
            {
                print("Application Quit");
                Application.Quit();
            }
        }
        
        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                    JambiSound.SetActive(true);
                }
                doggycontroller doggy = hit.collider.GetComponent<doggycontroller>();
                if (doggy != null)
                {
                    doggy.DisplayDialog();
                    dogsound.SetActive(true);
                }
            }
        }

        if (SpeedBoost)
        {
            changeTime -= Time.deltaTime;
        }
        if (changeTime <= 0)
        {
            SpeedBoost = false;
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        if (SpeedBoost)
        {
            speed = 10f;
            Drinking.SetActive(true);
        }
        else
        {
            speed = 3f;
        }

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
             Instantiate(DamagedEffect, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            PlaySound(hitSound);
            DamagedEffect.Play();
        }
        else if (amount > 0)
        {
             Instantiate(HealthEffect, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
             HealthEffect.Play();
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

    }

    public void ChangeSpeed(int amount)
    {
        speed = maxSpeed;
    }


    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        
        PlaySound(throwSound);
    } 
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}