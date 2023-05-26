using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public Text dialogueText;

    public BattleState state;

    private int enemyAttackCount = 0;
    private bool enemyIsDead = false;
    private int playerAttackCount = 0;
    private bool playerIsDead = false;
    private bool canHeal = true;

    public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerUnit = player.GetComponent<Unit>();

        enemyUnit = enemy.GetComponent<Unit>();

        dialogueText.text = "O " + enemyUnit.unitName + " se aproxima";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        playerAttackCount += 1;

        enemyHUD.SetHP(enemyUnit.currentHealth);
        dialogueText.text = "O ataque acertou";

        canHeal = true;
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " ataca!!";

        yield return new WaitForSeconds(2f);

        bool isDead = EnemyDealDamage();

        //bool 

        playerHUD.SetHP(playerUnit.currentHealth);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public bool EnemyDealDamage()
    {        
        if(enemyAttackCount < 5)
        {
            enemyIsDead = playerUnit.TakeDamage(enemyUnit.damage);           //Ataque Normal
            enemyAttackCount += 1;                                     //Aumenta o contador de ataques
            dialogueText.text ="Ataque normal";
        } else if(enemyAttackCount == 5)
        {
            enemyIsDead = playerUnit.TakeDamage(30);
            enemyAttackCount = 0;
            dialogueText.text ="Ataque supremo";
        }

        return enemyIsDead;
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Você ganhou a batalha";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "Você foi derrotado";
        }

    }

    void PlayerTurn()
    { 
        dialogueText.text = "Escolha sua ação: ";
        StartCoroutine(specialButton());
    }

    IEnumerator specialButton()
    {
        if (playerAttackCount < 3)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.1f);  
    }

    public void OnSpecialAttackButton()
    {
        if(playerAttackCount < 3)
        {
            return;
        }
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerSpecialAttack());
    }

    IEnumerator PlayerSpecialAttack()
    {
        bool isDead = enemyUnit.TakeDamage((playerUnit.damage * 2));
        playerAttackCount = 0;

        enemyHUD.SetHP(enemyUnit.currentHealth);
        dialogueText.text = "O ataque acertou";

        canHeal = true;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerHeal()
    {
        playerAttackCount += 1;
        if (canHeal == true) 
        { 
        canHeal = false;
        playerUnit.Heal(15);
        playerHUD.SetHP(playerUnit.currentHealth);

        dialogueText.text = "Você sente uma energia revigorante!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            dialogueText.text = "Não foi possivel curar nesse turno";
            yield return new WaitForSeconds(2f);
            PlayerTurn();            
        }
    }
}