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
    private State phase;

    private int turn = 1;

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
        uiManager.SetCharacterImageToParty();
        phase = State.SelectBattleCharacterState;
    }

    void Update()
    {
        switch (phase)
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
