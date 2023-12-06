using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kittyscript : MonoBehaviour
{
    public float speed;
    public bool vertical;
    float changeTime = 0.0f;
    float walkTime = 3.0f;

    public GameObject Explosion;

    public AudioClip catSound;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    bool broken = true;

    Animator animator;

    AudioSource audioSource;

    public GameObject vignette;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        vignette.SetActive(false);
        changeTime = 0.0f;
        timer = walkTime;
    }

    void Update()
    {
        if (changeTime > 0)
    {
        changeTime -= Time.deltaTime;
        vignette.SetActive(true);
    }
    else if(changeTime <= 0)
    {
        vignette.SetActive(false);
    }
    timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = walkTime;
        }
    
    }

    void FixedUpdate()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won�t be executed.
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            PlaySound(catSound);
            player.ChangeHealth(-3);
           
           changeTime += 3.0f;
        }

    }

    //Public because we want to call it from elsewhere like the projectile script
    public void Murder()
    {
        broken = false;
        rigidbody2D.simulated = false;
        //optional if you added the fixed animation
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
}
