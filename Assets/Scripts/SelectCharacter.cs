using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SelectCharacter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public BattleManager battleManager;
    public UIManager uiManager;
    public RectTransform thisRectTransform;
    private const float duration = 0.3f;
    private float pointerDownTime = 0;
    private bool pointerDownFlag = false;
    private bool holdAction = false;
    private bool used = false;
    public bool Used
    {
        get => used;
        set => used = value;
    }
    public bool myCard = true;
    public int orderNumber;
    public GameObject coverImageGameObject;
    public TextMeshProUGUI orderNumberText;

    void Update()
    {
        if (pointerDownFlag && !holdAction) pointerDownTime += Time.deltaTime;
        if(pointerDownFlag && pointerDownTime >= duration && !holdAction) //����������
        {
            HoldAction(orderNumber, myCard);
            holdAction = true;
        }
        if(!pointerDownFlag && holdAction) //�������I��
        {
            uiManager.skillInformationPanel.SetActive(false);
            holdAction = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownFlag = true;
        uiManager.PointerDownAnimation(thisRectTransform);
        this.GetComponent<Image>().color = Color.gray;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!holdAction && pointerDownFlag && myCard && !used && battleManager.STATE == BattleManager.State.WaitState) ClickCharacterCard(orderNumber);
        pointerDownFlag = false;
        ResetPointerDownTime();
        uiManager.PointerUpAnimation(thisRectTransform);
        this.GetComponent<Image>().color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerDownFlag = false;
        ResetPointerDownTime();
        uiManager.PointerUpAnimation(thisRectTransform);
        this.GetComponent<Image>().color = Color.white;
    }

    /// <summary>
    /// �^�b�v���Ԃ����������郁�\�b�h
    /// </summary>
    private void ResetPointerDownTime() => pointerDownTime = 0;

    public void HoldAction(int orderNumber, bool myCard)
    {
        Character character = (myCard) ? battleManager.myParty[orderNumber] : battleManager.enemyParty[orderNumber];
        uiManager.skillInformationPanelNameText.text = character.name;
        for (int i = 0; i<character.skills.Count; i++)
        {
            uiManager.handIconImages[i].sprite = GetHandIconSprite(character.skills[i].hand);
            uiManager.skillInformationPanelHandTexts[i].text = $"�����{character.skills[i].damage}�_���[�W�^����";
        }
        uiManager.skillInformationPanel.SetActive(true);
    }

    public Sprite GetHandIconSprite(Character.Skill.Hand hand)
    {
        switch (hand)
        {
            case Character.Skill.Hand.�O�[:
                return uiManager.gooSprite;
            case Character.Skill.Hand.�`���L:
                return uiManager.chokiSprite;
            case Character.Skill.Hand.�p�[:
                return uiManager.paaSprite;
            default:
                return uiManager.gooSprite;
        }
    }

    /// <summary>
    /// �퓬�Ɏg�p����L�����N�^�[�I�����̃L�����N�^�[�摜�̉������A�N�V����
    /// </summary>
    /// <param name="myOrderNumber">�Ґ��ԍ�</param>
    public void ClickCharacterCard(int myOrderNumber)
    {
        uiManager.SlideOutPartyCharacterCard();
        battleManager.MyBattleCharacter = battleManager.myParty[myOrderNumber];
        used = true;
        battleManager.usedMyPatry.Add(myOrderNumber);
        int enemyOrderNumber = GetEnemyOrderNumber();
        battleManager.EnemyBattleCharacter = battleManager.enemyParty[enemyOrderNumber];
        uiManager.enemyPartyImages[enemyOrderNumber].GetComponent<SelectCharacter>().used = true;
        battleManager.usedEnemyParty.Add(enemyOrderNumber);
    }

    public int GetEnemyOrderNumber()
    {
        List<int> nums = new() { 0, 1, 2, 3 };
        foreach(int i in battleManager.usedEnemyParty) nums.Remove(i);
        return nums[Random.Range(0, nums.Count)];
    }
}
