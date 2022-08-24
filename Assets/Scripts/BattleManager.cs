using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

    public UIManager uiManager;
    //�Q�[���̏��
    public enum State
    {
        SelectBattleCharacterState, //�o�g���ɂ����L�����N�^�[��I������X�e�[�g
        SelectHandState, //����񂯂�̎��I������X�e�[�g

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

    //�L�����N�^�[�f�[�^���i�[
    public List<Character> characters = new();

    //�p�[�e�B�Ґ����i�[
    public List<Character> myParty = new(); 
    public List<Character> enemyParty = new();
    //�o�g���Ɏg�p����L�����N�^�[
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
