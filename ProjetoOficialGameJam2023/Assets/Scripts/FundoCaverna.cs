using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FundoCaverna : MonoBehaviour
{


    public MeshRenderer mr;
    private float speed;    
    public float minX, maxX;
    public float timeLerp;
    public playerController player;


    public Transform playerLocal;
    private float EntrouCavernaX = 85.5f;
    private float SaiuCavernaX = 247.2f;

    public bool estaligada = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        player = FindObjectOfType<playerController>();
        speed = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        {
            if (player != null)
            {
                if (player.GetEstaAndadoEsquerda())
                {
                    mr.material.mainTextureOffset += new Vector2(-speed * Time.deltaTime, 0);
                }

                if (player.GetEstaAndadoDireita())
                {
                    mr.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
                }

                if (!player.GetIsMoving())
                {
                    mr.material.mainTextureOffset += new Vector2(0, 0);
                }
            }         
            
        }

    }

    private void FixedUpdate()
    {
        Vector3 newPosition = playerLocal.position + new Vector3(0, 0, -5);
        newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);
        transform.position = newPosition;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, 2);
    }


    public void ligarFundo()
    {
        gameObject.SetActive(true);
        estaligada = true;
    }
    public void desligarFundo()
    {
        gameObject.SetActive(false);
        estaligada = false;

    }



}
