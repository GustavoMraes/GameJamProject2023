using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class playerController : MonoBehaviour
{

    public static int nivel=0;




    [SerializeField] float speed;       // velocidade de movimento
    SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator playerAnim;

    private float recoveryCounter;      // Calculo do tempo de recuperacao
    private float attackTimer;          // calculo do cooldown de ataque

    [Header("Parametros Player")]
    public int maxHealth = 10;
    public int health;                  // Vida
    public float forca = 5;             // forca do pulo
    public int numeroPulos = 2;         // quantidade de pulos
    public float recoveryTime;          // Cooldown de recuperacao
    public HealthBar healthBar;

    [Header("Combate")]
    public int attackDamage;            // Dano por ataque
    public Transform attackHit;         // Ponto de origem do ataque
    public float attackRange;           // alcance do ataque
    public float attackCooldown;        // cooldown do ataque
    public LayerMask enemyLayers;       // Layers do inimigo

    private Vector3 healthBarScale;     // Tamanho da barra
    private float healthPercent;        // Percentual de vida para o calculo do tamanho da barra

    [Header("Extras")]
    public bool isMoving = false;
    public bool isGround;               // esta no chao
    private bool facingRight = true;    // olhando para direita/esquerda
    private int facingDirection = 1;    // 1 direita / -1 esquerda
    private bool recovering;            // Esta se recuperando de um ataque
    private bool canMove = true;        // Permite/bloqueia a movimentacao

    private float direcao = 1;

    private Menu menu;
    private bool isDead = false;
    public GameObject gameOverPanel;

    public int cookies;
    public Text quantidadeText;

    private bool boss = false;
    private bool inimigo = false;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        Time.timeScale = 1f;
        playerAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (canMove && !isDead)
        {
            Controls();
        }


        if (recovering) // cooldown de recuperacao
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
            canMove = true;
            isGround = true;
            numeroPulos = 2; //resetar quantidade de pulos
        }
        else
        {
            isGround = false;
        }

        if(collision.gameObject.tag == "Gelo")
        {
            if(facingDirection == -1)
                rb.velocity = new Vector2(transform.position.x*-1, transform.position.y);
            if(facingDirection == 1)
                rb.velocity = new Vector2(transform.position.x, transform.position.y);
        }
        
    }

    private void Attack()
    {
        boss = false;
        StartCoroutine("AttackAnim");
        Collider2D[] targets = Physics2D.OverlapCircleAll(attackHit.position, attackRange, enemyLayers);

            foreach (Collider2D target in targets)
            {
                target.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        rb.velocity = Vector2.zero;
    }

    private void AttackBoss()
    {
        inimigo = false;
        StartCoroutine("AttackAnim");
        Collider2D[] targets = Physics2D.OverlapCircleAll(attackHit.position, attackRange, enemyLayers);

        foreach (Collider2D target in targets)
        {
            target.GetComponent<BossHealth>().TakeDamage(attackDamage);
        }

        rb.velocity = Vector2.zero;
    }

    IEnumerator AttackAnim()
    {
        playerAnim.SetBool("Attack", true);

        yield return new WaitForSeconds(.5f);

        playerAnim.SetBool("Attack", false);
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
                if(!boss && !inimigo)
                {
                    StartCoroutine("AttackAnim");
                }
                Collider2D[] targets = Physics2D.OverlapCircleAll(attackHit.position, attackRange, enemyLayers);
                foreach (Collider2D target in targets)
                {
                    inimigo = target.gameObject.tag == "Inimigo";
                    boss = target.gameObject.tag == "Boss";
                    if (boss)
                    {
                        AttackBoss();
                    }else if (inimigo)
                    {
                        Attack();
                    }
                }
               
                attackTimer = attackCooldown;
            }
        }
        else
        {
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
           if (facingRight)
           {
                Flip();
           }
           //playerAnim.SetFloat("Velocidade", Mathf.Abs(facingDirection));
        }
        direcao = Input.GetAxisRaw("Horizontal");
        playerAnim.SetFloat("Velocidade", Mathf.Abs(direcao));

        Moving();

    }

    void Moving()
    {
        if(direcao == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
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

            healthBar.SetHealth(health);
            //UpdateHealthBar();

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
        isDead = true;
        playerAnim.SetBool("isDead", true);
        StartCoroutine(Dead());
        
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator Dead() 
    {
        GameOver();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
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

    public bool GetEstaAndadoEsquerda()
    {
        return isMoving && !facingRight;
    }

    public bool GetEstaAndadoDireita()
    {
        return isMoving && facingRight;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public void Curar(int cura)
    {
        if ((health + cura) > 10)
        {
            health = 10;
            healthBar.SetHealth(health);
        }
        else
        {
            health = health + cura;
            healthBar.SetHealth(health);
        }
    }

    public void Mola()
    {        
        rb.AddForce(transform.up * 3500);
    }

    public void GanharCookie()
    {
        cookies = cookies + 1;
        quantidadeText.text = "" + cookies;   
    }
    public void SubirNivel()
    {
        nivel += 1;
    }
    public int VerNivel()
    {
        return nivel;
    }
}