using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

    public UIManager uiManager;
    //ゲームの状態
    public enum State
    {
        SelectBattleCharacterState, //バトルにだすキャラクターを選択するステート
        SelectHandState, //じゃんけんの手を選択するステート
        NoAction, //動作なしステート
    }
    private State state;
    public State _state
    {
        get => state;
        set => state = value;
    }

    private int turn = 1;
    public int Turn
    {
        get => turn;
        set => turn = value;
    }
    public int maxTurn = 4;
    public int myPartyMaxHp = 100;
    public int enemyPartyMaxHp = 100;
    private int myPartyCurrentHp;
    public int MyPartyCurrentHp
    {
        get => myPartyCurrentHp;
        set => myPartyCurrentHp = value;
    }
    private int enemyPartyCurrentHp;
    public int EnemyPartyCurrentHp
    {
        get => enemyPartyCurrentHp;
        set => enemyPartyCurrentHp = value;
    }

    //キャラクターデータを格納
    public List<Character> characters = new();

    //パーティ編成を格納
    public List<Character> myParty = new(); 
    public List<Character> enemyParty = new();
    //バトルに使用するキャラクター
    private Character myBattleCharacter;
    public Character MyBattleCharacter
    {
        get => myBattleCharacter;
        set => myBattleCharacter = value;
    }
    private Character enemyBattleCharacter;
    public Character EnemyBattleCharacter
    {
        get => enemyBattleCharacter;
        set => enemyBattleCharacter = value;
    }

    void Start()
    {
        myPartyCurrentHp = myPartyMaxHp;
        enemyPartyCurrentHp = enemyPartyMaxHp;
        uiManager.SetCharacterImageToParty();
        state = State.SelectBattleCharacterState;
    }

    void Update()
    {
        switch (state)
        {
            case State.SelectBattleCharacterState:
                uiManager.selectCharacterCanvas.SetActive(true);
                uiManager.SlideInPartyCharacterCard();
                state = State.NoAction;
                break;
            case State.SelectHandState:
                uiManager.InitializeSelectHandUI();
                uiManager.selectHandCanvas.SetActive(true);
                uiManager.SlideInBattleCharacterCard();
                uiManager.ChangeBattleCardImage();
                uiManager.ChangeHandUI();
                state = State.NoAction;
                break;
            case State.NoAction:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// じゃんけんの勝敗判定メソッド
    /// </summary>
    /// <param name="myHandNumber">自分の手の番号</param>
    /// <param name="enemyHandNumber">相手の手の番号</param>
    public void Judge(int myHandNumber, int enemyHandNumber)
    {
        if ((myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.グー && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.チョキ) ||
            (myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.チョキ && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.パー) ||
            (myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.パー && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.グー))
        {
            Debug.Log("win");
            Win(myBattleCharacter.skills[myHandNumber]);
        }
        else if ((myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.グー && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.パー) ||
            (myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.チョキ && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.グー) ||
            (myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.パー && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.チョキ))
        {
            Debug.Log("lose");
            Lose(enemyBattleCharacter.skills[enemyHandNumber]);
        }
        else Draw(myBattleCharacter.skills[myHandNumber], enemyBattleCharacter.skills[enemyHandNumber]);
    }

    private void Win(Character.Skill mySkill)
    {
        enemyPartyCurrentHp = Mathf.Clamp(enemyPartyCurrentHp - mySkill.damage, 0, enemyPartyCurrentHp);
        uiManager.ChangeEnemyHpUI();
    }

    private void Lose(Character.Skill enemySkill)
    {
        myPartyCurrentHp = Mathf.Clamp(myPartyCurrentHp - enemySkill.damage, 0, myPartyCurrentHp);
        uiManager.ChangeMyHpUI();
    }

    private void Draw(Character.Skill mySkill, Character.Skill enemySkill)
    {
        Debug.Log(mySkill.damage);
        Debug.Log(enemySkill.damage);
        enemyPartyCurrentHp = Mathf.Clamp(enemyPartyCurrentHp - mySkill.damage, 0, enemyPartyMaxHp);
        uiManager.ChangeEnemyHpUI();
        myPartyCurrentHp = Mathf.Clamp(myPartyCurrentHp - enemySkill.damage, 0, myPartyMaxHp);
        uiManager.ChangeMyHpUI();
    }
}
