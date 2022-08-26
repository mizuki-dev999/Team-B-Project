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
        BattleEnd, //�퓬�I���X�e�[�g
        WaitState, //����Ȃ��X�e�[�g
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
    private int maxTurn = 4;
    public int GetMaxTurn() => maxTurn;
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

    public List<int> usedMyPatry = new();
    public List<int> usedEnemyParty = new();
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

    //�G�t�F�N�g
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
    /// ����񂯂�̏��s���胁�\�b�h
    /// </summary>
    /// <param name="myHandNumber">�����̎�̔ԍ�</param>
    /// <param name="enemyHandNumber">����̎�̔ԍ�</param>
    public void Judge(int myHandNumber, int enemyHandNumber)
    {
        if ((myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.�O�[ && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.�`���L) ||
            (myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.�`���L && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.�p�[) ||
            (myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.�p�[ && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.�O�[))
        {
            Win(myBattleCharacter.skills[myHandNumber]);
        }
        else if ((myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.�O�[ && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.�p�[) ||
            (myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.�`���L && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.�O�[) ||
            (myBattleCharacter.skills[myHandNumber].hand == Character.Skill.Hand.�p�[ && enemyBattleCharacter.skills[enemyHandNumber].hand == Character.Skill.Hand.�`���L))
        {
            Lose(enemyBattleCharacter.skills[enemyHandNumber]);
        }
        else Draw();
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

    private void Draw()
    {
        uiManager.ShowJudgeImage(uiManager.drawLogoGameObject);
        SecondJudge();
    }

    /// <summary>
    /// ���������̂Q�i�K���胁�\�b�h
    /// </summary>
    public void SecondJudge()
    {
        if ((myBattleCharacter.handElement == Character.CharacterElement.�O�[ && enemyBattleCharacter.handElement == Character.CharacterElement.�`���L) ||
            (myBattleCharacter.handElement == Character.CharacterElement.�`���L && enemyBattleCharacter.handElement == Character.CharacterElement.�p�[) ||
            (myBattleCharacter.handElement == Character.CharacterElement.�p�[ && enemyBattleCharacter.handElement == Character.CharacterElement.�O�[))
        {
            SecondWin();
        }
        else if ((myBattleCharacter.handElement == Character.CharacterElement.�O�[ && enemyBattleCharacter.handElement == Character.CharacterElement.�p�[) ||
            (myBattleCharacter.handElement == Character.CharacterElement.�`���L && enemyBattleCharacter.handElement == Character.CharacterElement.�O�[) ||
            (myBattleCharacter.handElement == Character.CharacterElement.�p�[ && enemyBattleCharacter.handElement == Character.CharacterElement.�`���L))
        {
            SecondLose();
        }
        else SecondDraw();
    }

    public void SecondWin()
    {
        enemyPartyCurrentHp = Mathf.Clamp(enemyPartyCurrentHp - myBattleCharacter.handElementDamage, 0, enemyPartyCurrentHp);
        enemyEffect.BeginAnimation();
        uiManager.ChangeEnemyHpUI();
    }

    public void SecondLose()
    {
        myPartyCurrentHp = Mathf.Clamp(myPartyCurrentHp - enemyBattleCharacter.handElementDamage, 0, myPartyCurrentHp);
        myEffect.BeginAnimation();
        uiManager.ChangeMyHpUI();
    }

    public void SecondDraw()
    {
        if (myBattleCharacter.handElementDamage > enemyBattleCharacter.handElementDamage) SecondWin();
        else if (myBattleCharacter.handElementDamage < enemyBattleCharacter.handElementDamage) SecondLose();
    }
}
