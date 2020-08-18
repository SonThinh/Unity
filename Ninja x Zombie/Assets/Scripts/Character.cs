using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    

    [SerializeField]
    protected Transform knifePos;

    [SerializeField]
    protected float movementSpeed;

    protected bool facingRight;

    [SerializeField]
    protected Stat healthStat;

    [SerializeField]
    private GameObject kunaiPrefab;
    [SerializeField]
    private EdgeCollider2D meleeCollider;
    [SerializeField]
    private List<string> damageSource;

    public abstract bool IsDead { get;}
    public bool TakingDamage { get; set; }
    public bool Attack { get; set; }
    public Animator anim { get; private set; }
    public EdgeCollider2D MeleeCollider { get => meleeCollider;}

    // Start is called before the first frame update
    public virtual void Start()
    {
        facingRight = true;
        anim = GetComponent<Animator>();
        healthStat.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
    public virtual void ThrowKunai(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(kunaiPrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
            tmp.GetComponent<Kunai>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(kunaiPrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            tmp.GetComponent<Kunai>().Initialize(Vector2.left);
        }
    }

    public abstract IEnumerator TakeDamaged();
    public abstract void Death();

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(damageSource.Contains(other.tag))
        {
            StartCoroutine(TakeDamaged());
        }
    }
    public void MeleeAttack()
    {
        MeleeCollider.enabled = true;
    }
}
