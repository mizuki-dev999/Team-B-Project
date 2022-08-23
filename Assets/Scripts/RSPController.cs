using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RSPController : MonoBehaviour
{
    public int playerHand;
    public int enemyHand;
    public bool win;
    public bool lose;
    public bool draw;

    public HPController hpController;

    public Character myCharacter;
    public Character enemyCharacter;

    public Image myCharacterImage;
    public Image enemyCharacterImage;

    public Text 

    private void Start()
    {
        myCharacterImage.sprite = myCharacter.characterImage;
        enemyCharacterImage.sprite = enemyCharacter.characterImage;
    }

    public void RockButton()
    {
        playerHand = 0;
        enemyHand = Random.Range(0, 3);
        Judge();
    }
    
    public void ScissorsButton()
    {
        playerHand = 1;
        enemyHand = Random.Range(0, 3);
        Judge();
    }

    public void PaperButton()
    {
        playerHand = 2;
        enemyHand = Random.Range(0, 3);
        Judge();
    }

    public void Judge()
    {
        if(playerHand == 0)
        {
            if (enemyHand == 0)
            {
                Debug.Log("Ç†Ç¢Ç±");
                Draw();
            }
            if (enemyHand == 1)
            {
                Debug.Log("èüóò");
                Win();
            }
            if (enemyHand == 2)
            {
                Debug.Log("îsñk");
                Lose();
            }
        }

        if (playerHand == 1)
        {
            if (enemyHand == 1)
            {
                Debug.Log("Ç†Ç¢Ç±");
                Draw();
            }
            if (enemyHand == 2)
            {
                Debug.Log("èüóò");
                Win();
            }
            if (enemyHand == 0)
            {
                Debug.Log("îsñk");
                Lose();
            }

        }
        if (playerHand == 2)
        {
            if (enemyHand == 2)
            {
                Debug.Log("Ç†Ç¢Ç±");
                Draw();
            }
            if (enemyHand == 1)
            {
                Debug.Log("èüóò");
                Win();
            }
            if (enemyHand == 0)
            {
                Debug.Log("îsñk");
                Lose();
            }
        }
    }

    private void Draw()
    {
        draw = true;
        win = false;
        lose = false;
        
    }

    public int turn;

    void Win()
    {
        win = true;
        draw = false;
        lose = false;

        hpController.PlayerAttack(myCharacter.skills[playerHand].damage);
    }

    void Lose()
    {
        lose = true;
        win = false;
        draw = false;

        hpController.EnemyAttack(enemyCharacter.skills[enemyHand].damage);
    }
}
