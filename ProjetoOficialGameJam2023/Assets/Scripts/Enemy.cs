using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private RaycastHit2D rightWall;         //verificador da parede direita
    private RaycastHit2D leftWall;          //verificador da parede esquerda
    private RaycastHit2D rigthEdge;         //verificador da borda do chao direito
    private RaycastHit2D leftEdge;          //verificador da borda do chao esquerdo
    private RaycastHit2D rightEnemy;        //Verificador de inimigo a direita
    private RaycastHit2D leftEnemy;         //verificador de inimigo a esquerda

    public bool facingLeft = false;         // verificador da posicao do sprite
    private bool canMove = true;            //Verificar se pode se mover
    private bool recovering;                // verificar se pode tomar dano ou nao
    private float recoveringCounter;        // contador ate ser possivel tomar dano
    SpriteRenderer spriteRenderer;

    

    [Header("Raycast")]
    public Vector2 offset;
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    [Header("Componentes")]
    public Rigidbody2D enemyRb;             // enemy rigidbody
    public Animator enemyAnim;              // enemy animacao

    [Header("Movimento")]
    public float moveSpeed;                 //velocidade de movimentacao
    public float rangeDetect;               //area pra detectar o player
    public bool followMode = false;         //modo seguir player
    public playerController player;

    [Header("Combate")]
    public int health;
    public Transform healthBar; //Barra de vida verde
    public GameObject healthBarObject; //Objeto pai das barras
    public float recoveryTime;

    [Header("Barra de vida")]
    private Vector3 healthBarScale; // Tamanho da barra
    private float healthPercent; // Percentual de vida para o calculo do tamanho da barra
    private float pivot; //Ordem pela qual a barra vai diminuir

    
    private int direction = 1; // 1 - direita / -1 esquerda
    // Start is called before the first frame update
    void Start()
    {
        healthBarScale = healthBar.localScale;
        healthPercent = healthBarScale.x / health;
        //healthBarObject.SetActive(false);
    }

    void UpdateHealthBar()
    {
        healthBarScale.x = healthPercent * health;
        healthBar.localScale = healthBarScale;
    }

    // Update is called once per frame
    void Update()
    {
        checkSurroundings();
        Recovering();
    }

    void Recovering()
    {
        if (recovering)
        {
            recoveringCounter += Time.deltaTime;
            if (recoveringCounter >= recoveryTime)
            {
                recoveringCounter = 0;
                recovering = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Move();
        }
       
    }
    private void Move() //movimentacao
    {
        enemyRb.velocity = new Vector2 (moveSpeed * direction, enemyRb.velocity.y);
    }

    void Flip() //virar o sprite
    {
        facingLeft = !facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    private void checkSurroundings() //verificar paredes e chao
    {
        var playerDistance = player.gameObject.transform.position.x - transform.position.x;
        var followPlayer = Mathf.Abs(playerDistance) < rangeDetect;
        if (followMode && followPlayer && rigthEdge.collider != null && leftEdge.collider != null)
        {
            if (playerDistance < 0)
            {
                direction = -1;
                moveSpeed = 3;
            }
            else
            {
                direction = 1;
                moveSpeed = 3;
            }
        }
        else
        {
            moveSpeed = 2;
        }

        rightEnemy = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.right, 1f, enemyLayer);
        Debug.DrawRay(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.right, Color.blue);
        if (rightEnemy.collider != null)
        {
            direction = -1;
        }

        leftEnemy = Physics2D.Raycast(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.left, 1f, enemyLayer);
        Debug.DrawRay(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.left, Color.blue);
        if (leftEnemy.collider != null)
        {
            direction = 1;
        }

        rightWall = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.right, 1f, wallLayer);
        Debug.DrawRay(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.right, Color.yellow);
        if (rightWall.collider != null)
        {
            direction = -1;
        }

        leftWall = Physics2D.Raycast(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.left, 1f, wallLayer);
        Debug.DrawRay(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.left, Color.yellow);
        if (leftWall.collider != null)
        {
            direction = 1;
        }

        rigthEdge = Physics2D.Raycast(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.down, 1f, groundLayer);
        Debug.DrawRay(new Vector2(transform.position.x + offset.x, transform.position.y + offset.y), Vector2.down, Color.red);
        if (rigthEdge.collider == null && !followPlayer)
        {
            direction = -1;
        }
        if (rigthEdge.collider == null && followPlayer)
        {
            direction = 0;
        }
        if (rigthEdge.collider == null && playerDistance < 0)
        {
            direction = -1;
        }

        leftEdge = Physics2D.Raycast(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.down, 1f, groundLayer);
        Debug.DrawRay(new Vector2(transform.position.x - offset.x, transform.position.y + offset.y), Vector2.down, Color.red);
        if (leftEdge.collider == null && !followPlayer)
        {
            direction = 1;
        }
        if (leftEdge.collider == null && followPlayer)
        {
            direction = 0;
        }
        if(leftEdge.collider == null && playerDistance > 0)
        {
            direction = 1;
        }

        if (facingLeft && direction == 1 || !facingLeft && direction == -1) 
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerController>().TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!recovering)
        {
            //healthBarObject.SetActive(true);

            //enemyAnim.SetTrigger("hurt");

            health -= damage;

            UpdateHealthBar();

            StartCoroutine("StopMove");

            recovering = true;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StopMove()
    {
        enemyRb.velocity = Vector2.zero;

        canMove = false;

        yield return new WaitForSeconds(.5f);

        //healthBarObject.SetActive(true);

        canMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeDetect);
    }
}
