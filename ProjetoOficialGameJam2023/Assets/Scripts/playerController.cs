using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] float speed;       // velocidade de movimento
    SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator playerAnim;

    private float recoveryCounter;      // Calculo do tempo de recuperacao
    private float attackTimer;          // calculo do cooldown de ataque

    [Header("Parametros Player")]
    public int health;                  // Vida
    public float forca = 5;             // forca do pulo
    public int numeroPulos = 2;         // quantidade de pulos
    public float recoveryTime;          // Cooldown de recuperacao

    [Header("Combate")]
    public int attackDamage;            // Dano por ataque
    public Transform attackHit;         // Ponto de origem do ataque
    public float attackRange;           // alcance do ataque
    public float attackCooldown;        // cooldown do ataque
    public LayerMask enemyLayers;       // Layers do inimigo

    public bool isGround;               // esta no chao
    private bool facingRight = false;    // olhando para direita/esquerda
    private int facingDirection = -1;    // 1 direita / -1 esquerda
    private bool recovering;            // Esta se recuperando de um ataque
    private bool canMove = true;        // Permite/bloqueia a movimentacao

    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            Controls();
            
        }
        

        if(recovering) // cooldown de recuperacao
        {
            recoveryCounter += Time.deltaTime;
            if(recoveryCounter >= recoveryTime)
            {
                recoveryCounter = 0;
                recovering = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") //verificar se o personagem esta no chao
        {
            isGround = true;
            numeroPulos = 2; //resetar quantidade de pulos
        }
        else
        {
            isGround = false;
        }
    }

    private void Attack()
    {
        anim.SetBool("Attack", true);

        Collider2D[] targets = Physics2D.OverlapCircleAll(attackHit.position, attackRange, enemyLayers);

        foreach (Collider2D target in targets)
        {
            target.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
        rb.velocity = Vector2.zero;
        StartCoroutine("Freeze");

        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHit.position, attackRange);
    }

    private void Controls()
    {
        if (attackTimer <= 0) // se o ataque nao esta em cooldown
        {
            if(Input.GetKeyDown(KeyCode.X) && isGround)
            {
                Attack();
                attackTimer = attackCooldown;
            }
        }
        else
        {
            anim.SetBool("Attack", false);
            attackTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && numeroPulos > 0) //Pular
        {
            rb.AddForce(transform.up * forca);
            numeroPulos -= 1;
        }

        if (Input.GetKey(KeyCode.RightArrow)) //andar pra direita
        {
            transform.position += new Vector3(1 * speed * Time.deltaTime, 0, 0);
            if (!facingRight)
            {
                Flip();
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow)) //andar pra esquerda
        {
            transform.position -= new Vector3(1 * speed * Time.deltaTime, 0, 0);
            if(facingRight)
            {
                Flip();
            }   
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) //comecar animacao de andar pra direita
        {
            anim.SetBool("isMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) //terminar animacao de andar pra direita
        {
            anim.SetBool("isMoving", false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        facingDirection *= -1;
    }

    public void TakeDamage(int damage)
    {
        if(!recovering) 
        {
            playerAnim.SetTrigger("hurt");
            Knockback();
            //vCam.GetComponent<CameraController>().CameraShake();
            health -= damage;

            recovering = true;

            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        // Tocar animacao
        anim.SetBool("isDead", true);
    }

    void Knockback()
    {
        rb.AddForce(new Vector2(10 * -facingDirection, 10), ForceMode2D.Impulse);

        StartCoroutine("Freeze");
    }

    IEnumerator Freeze()
    {
        // Retira o controle do personagem
        canMove = false;

        yield return new WaitForSeconds(.5f);

        // Devolve o controle do personagem
        canMove = true;
    }


}
