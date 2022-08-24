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
    private State phase;

    private int turn = 1;

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
