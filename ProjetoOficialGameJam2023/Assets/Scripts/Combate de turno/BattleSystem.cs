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
    //public Text playerName;
    //public Text enemyName;
    public Text dialogueText;

    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerUnit = player.GetComponent<Unit>();

        enemyUnit = enemy.GetComponent<Unit>();

        dialogueText.text = "A " +enemyUnit.unitName+  " approaches";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "O ataque acertou";

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        //change state based on what happened
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "Você ganhou a batalha";
        } else if(state = BattleState.LOST)
        {
            dialogueText.text = "Você foi derrotado";
        }
        
    }

    void PlayerTurn()
    {
        dialogueText.text = "Escolha sua ação: ";
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }
}
