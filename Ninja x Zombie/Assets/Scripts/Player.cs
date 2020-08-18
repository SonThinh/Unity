using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public delegate void DeadEventHandler();
public class Player : Character
{
    private static Player instance;
   
    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    private Vector2 startPos;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;
 
    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    private bool immortal = false;
    [SerializeField]
    private float immortalTime;

    private SpriteRenderer spriteRenderer;
    public Rigidbody2D r2 { get; set; }

    public bool Slide { get; set; }

    public bool Jump { get; set; }

    public bool OnGround { get; set; }

    public event DeadEventHandler Dead;
    public override bool IsDead
    {
        get
        {
            if(healthStat.CurrentVal <= 0)
            {
                OnDead();
            }
            return healthStat.CurrentVal <= 0;
        }
    }
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        r2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -14f)
            {
                Death();
            }
            HandleInput();
        }
        
        
    }
    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            OnGround = IsGrounded();
            HandleMovement(horizontal);
            Flip(horizontal);
            HandleLayers();
        }
    }
    public void OnDead()
    {
        if(Dead != null)
        {
            Dead();
        }
    }
    private void HandleMovement(float horizontal)
    {
       if(r2.velocity.y < 0)
       {
            anim.SetBool("land", true);
       }
       if(!Attack && !Slide && (OnGround || airControl))
       {
            r2.velocity = new Vector2(horizontal * movementSpeed, r2.velocity.y);
       }
       if (Jump && r2.velocity.y == 0)
       {
            r2.AddForce(new Vector2(0, jumpForce));
       }
        anim.SetFloat("speed", Mathf.Abs(horizontal));
    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("throw");
        }
        
    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            ChangeDirection();
        }
    }


    private bool IsGrounded()
    {
        if(r2.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for(int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void HandleLayers()
    {
        if (!OnGround)
        {
            anim.SetLayerWeight(1, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }
    public override void ThrowKunai(int value)
    {
        if(!OnGround && value == 1 || OnGround && value == 0)
        {
            base.ThrowKunai(value);
        }
        
    }
    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
    public override IEnumerator TakeDamaged()
    {
        if(!immortal)
        {
            healthStat.CurrentVal -= 10;
            if (!IsDead)
            {
                anim.SetTrigger("damaged");
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
            else
            {
                anim.SetLayerWeight(1, 0);
                anim.SetTrigger("die");
            }
        }
    }

    public override void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Coins.Instance.CollectedCoins++;
            Destroy(other.gameObject);
            
        }
    }
}
