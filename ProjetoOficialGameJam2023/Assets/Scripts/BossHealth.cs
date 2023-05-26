using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

	public int health = 500;

	public GameObject deathEffect;

	public Animator animator;

	public bool isInvulnerable = false;

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

		yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
    public int quantodevida()
    {
        return health;
    }

}
