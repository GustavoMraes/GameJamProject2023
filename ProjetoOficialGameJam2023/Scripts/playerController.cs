using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] float speed; // velocidade de movimento
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public float forca = 5; // forca do pulo
    public bool isGround; //esta no chao
    public int numeroPulos = 2; // quantidade de pulos

    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && numeroPulos > 0) //Pular
        {
            rb.AddForce(transform.up * forca);  
            numeroPulos -= 1;
        }

        if (Input.GetKey(KeyCode.RightArrow)) //andar pra direita
        {
            transform.position += new Vector3(1 * speed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow)) //andar pra esquerda
        {
            transform.position -= new Vector3(1 * speed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) //comecar animacao de andar pra direita
        {
            anim.SetBool("isMovingRight", true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) //terminar animacao de andar pra direita
        {
            anim.SetBool("isMovingRight", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) //comecar animacao de andar pra esquerda
        {
            anim.SetBool("isMoving", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) //terminar animacao de andar pra esquerda
        {
            anim.SetBool("isMoving", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bloco") //verificar se o personagem esta no chao
        {
            isGround = true;
            numeroPulos = 2; //resetar quantidade de pulos
        }
        else
        {
            isGround = false;
        }
    }


}
