using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour
{
    public Slider playerHPbar;
    public Slider enemyHPbar;
    public int playerHP;
    public int enemyHP;
    int playerHPMax = 100;
    int enemyHPMax = 100;

    private void Start()
    {
        playerHP = playerHPMax;
        enemyHP = enemyHPMax;
    }

    private void Update()
    {
        playerHPbar.value = playerHP;
        enemyHPbar.value = enemyHP;
    }
    /// <summary>
    /// player‚ªUŒ‚‚·‚é‚ÌŠÖ”
    /// </summary>
    public void PlayerAttack(int _damage)
    {
        enemyHP -= _damage;
    }
    /// <summary>
    /// enemy‚ªUŒ‚‚·‚é‚Æ‚«‚ÌŠÖ”
    /// </summary>
    /// <param name="_damage"></param>
    public void EnemyAttack(int _damage)
    {
        playerHP -= _damage;
    }

    public void testButton()
    {
        playerHP -= 10;
        enemyHP -= 10;
    }
}
