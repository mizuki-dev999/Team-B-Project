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
                break;
            case State.SelectHandState:
                break;
            default:
                break;
        }
    }
}
