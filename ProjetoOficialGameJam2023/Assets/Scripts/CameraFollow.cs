using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; //player
    public float minX, maxX; //definir limte de movimentacao da camera 
    public float timeLerp; //atrasar movimentacao da camera

    private void FixedUpdate() //fazer a camera centralizar no personagem
    {
        Vector3 newPosition = player.position + new Vector3(0, 0, -5);
        newPosition.y = 0.1f;
        newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);
        transform.position = newPosition;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
    }
}
