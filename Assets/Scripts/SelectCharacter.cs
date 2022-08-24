using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCharacter : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public BattleManager battleManager;
    private const float duration = 0.3f;
    private float pointerDownTime = 0;
    private bool pointerDownFlag = false;
    private bool holdAction = false;
    public GameObject SkillInformationPanel;
    public bool myCard = true;
    public int orderNumber;

    void Update()
    {
        if (pointerDownFlag && !holdAction) pointerDownTime += Time.deltaTime;
        if(pointerDownFlag && pointerDownTime >= duration && !holdAction) //����������
        {
            Debug.Log("Hold");
            SkillInformationPanel.SetActive(true);
            holdAction = true;
        }
        if(!pointerDownFlag && holdAction) //�������I��
        {
            Debug.Log("Finish Hold");
            SkillInformationPanel.SetActive(false);
            holdAction = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownFlag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!holdAction && pointerDownFlag && myCard) ClickCharacterCard(orderNumber);
        pointerDownFlag = false;
        ResetPointerDownTime();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerDownFlag = false;
        ResetPointerDownTime();
    }

    /// <summary>
    /// �^�b�v���Ԃ����������郁�\�b�h
    /// </summary>
    private void ResetPointerDownTime() => pointerDownTime = 0;

    public void HoldAction()
    {

    }

    /// <summary>
    /// �퓬�Ɏg�p����L�����N�^�[�I�����̃L�����N�^�[�摜�̉������A�N�V����
    /// </summary>
    /// <param name="orderNumber">�Ґ��ԍ�</param>
    public void ClickCharacterCard(int orderNumber)
    {
        battleManager.MyBattleCharacter = battleManager.myParty[orderNumber];
        battleManager.EnemyBattleCharacter = battleManager.enemyParty[Random.Range(0, battleManager.enemyParty.Count)];
        Debug.Log("Tap");
    }
}
