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
        if(pointerDownFlag && pointerDownTime >= duration && !holdAction) //長押し判定
        {
            Debug.Log("Hold");
            SkillInformationPanel.SetActive(true);
            holdAction = true;
        }
        if(!pointerDownFlag && holdAction) //長押し終了
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
    /// タップ時間を初期化するメソッド
    /// </summary>
    private void ResetPointerDownTime() => pointerDownTime = 0;

    public void HoldAction()
    {

    }

    /// <summary>
    /// 戦闘に使用するキャラクター選択時のキャラクター画像の押下時アクション
    /// </summary>
    /// <param name="orderNumber">編成番号</param>
    public void ClickCharacterCard(int orderNumber)
    {
        battleManager.MyBattleCharacter = battleManager.myParty[orderNumber];
        battleManager.EnemyBattleCharacter = battleManager.enemyParty[Random.Range(0, battleManager.enemyParty.Count)];
        Debug.Log("Tap");
    }
}
