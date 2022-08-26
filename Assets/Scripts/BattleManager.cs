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
        BattleEnd, //戦闘終了ステート
        WaitState, //動作なしステート
    }
    private State state;
    public State STATE
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

    public List<int> usedMyPatry = new();
    public List<int> usedEnemyParty = new();
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

    //エフェクト
    public SpriteAnimator myEffect;
    public SpriteAnimator enemyEffect;

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
                uiManager.InitializeSelectCharacterUI();
                uiManager.selectCharacterCanvas.SetActive(true);
                uiManager.SlideInPartyCharacterCard();
                state = State.WaitState;
                break;
            case State.SelectHandState:
                uiManager.InitializeSelectHandUI();
                uiManager.selectHandCanvas.SetActive(true);
                uiManager.SlideInBattleCharacterCard();
                uiManager.ChangeBattleCardImage();
                uiManager.ChangeHandUI();
                state = State.WaitState;
                break;
            case State.BattleEnd:
                break;
            case State.WaitState:
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
            Win(myBattleCharacter.skills[myHandNumber]);
        }
        else if ((myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.グー && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.パー) ||
            (myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.チョキ && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.グー) ||
            (myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.パー && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.チョキ))
        {
            Lose(enemyBattleCharacter.skills[enemyHandNumber]);
        }
        else Draw(myBattleCharacter.skills[myHandNumber], enemyBattleCharacter.skills[enemyHandNumber]);
    }

    private void Win(Character.Skill mySkill)
    {
        enemyPartyCurrentHp = Mathf.Clamp(enemyPartyCurrentHp - mySkill.damage, 0, enemyPartyCurrentHp);
        if (mySkill.SkillEffectSprites.Length == 0) enemyEffect.BeginAnimation();
        else enemyEffect.BeginAnimation(mySkill.SkillEffectSprites);
        uiManager.ShowJudgeImage(uiManager.winLogoGameObject);
        uiManager.ChangeEnemyHpUI();
    }

    private void Lose(Character.Skill enemySkill)
    {
        myPartyCurrentHp = Mathf.Clamp(myPartyCurrentHp - enemySkill.damage, 0, myPartyCurrentHp);
        if (enemySkill.SkillEffectSprites.Length == 0) myEffect.BeginAnimation();
        else myEffect.BeginAnimation(enemySkill.SkillEffectSprites);
        uiManager.ShowJudgeImage(uiManager.loseLogoGameObject);
        uiManager.ChangeMyHpUI();
    }

    private void Draw(Character.Skill mySkill, Character.Skill enemySkill)
    {
        enemyPartyCurrentHp = Mathf.Clamp(enemyPartyCurrentHp - mySkill.damage, 0, enemyPartyCurrentHp);
        if (mySkill.SkillEffectSprites.Length == 0) enemyEffect.BeginAnimation();
        else enemyEffect.BeginAnimation(mySkill.SkillEffectSprites);
        uiManager.ChangeEnemyHpUI();

        myPartyCurrentHp = Mathf.Clamp(myPartyCurrentHp - enemySkill.damage, 0, myPartyCurrentHp);
        if (enemySkill.SkillEffectSprites.Length == 0) myEffect.BeginAnimation();
        else myEffect.BeginAnimation(enemySkill.SkillEffectSprites);
        uiManager.ChangeMyHpUI();

        uiManager.ShowJudgeImage(uiManager.drawLogoGameObject);
    }
}
