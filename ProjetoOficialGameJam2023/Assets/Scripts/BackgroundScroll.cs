using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public MeshRenderer mr;
    private float speed;
    public playerController player;
    //public CameraFollow camera;
    //public Transform localizacaoCamera;


    public Transform playerLocal; //player
    public float minX, maxX; //definir limte de movimentacao da camera 
    public float timeLerp; //atrasar movimentacao da camera
   


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerController>();
        speed = 0.1f;
       }

    // Update is called once per frame
    void Update()
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

  

    private void FixedUpdate() //fazer a camera centralizar no personagem
    {
        Vector3 newPosition = playerLocal.position + new Vector3(0, 0, -5);
        //newPosition.y = 0.1f;
        newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);

        transform.position = newPosition;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, 2);
     //   background.Seguir(newPosition);
    }


}
