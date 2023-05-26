using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{


	public int health = 500;

	public GameObject deathEffect;

	public Animator animator;

	public bool isInvulnerable = false;

	public string lobby;


    public playerController player;
    private int nivel;
	
	void Start()
	{
        player = FindObjectOfType<playerController>();
    }

    public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

		health -= damage;

		if (health <= 200)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (health <= 0)
		{
			StartCoroutine(Die());
		}
	}

	IEnumerator Die()
	{
		animator.SetBool("isDead", true);

        yield return new WaitForSeconds(5f);
        player.SubirNivel();
        SceneManager.LoadScene(lobby);
		
    }
    public int quantodevida()
    {
        return health;
    }

}
