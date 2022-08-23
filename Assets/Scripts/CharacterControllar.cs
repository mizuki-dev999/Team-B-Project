using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterControllar : MonoBehaviour
{
    public List<Image> myCharacterImages;
    public List<Image> enemyCharacterImages;
    [Header("")]
    public List<Character> characters = new();

    public List<Character> myParty = new();
    public List<Character> enemyParty = new();

    public Character nowCharacter;

    public enum Phase
    {
        SelectCharacterOrder, //キャラクターの順番を決める
        SelectHand, //手を決める
        damage,
    }
    public Phase phase;
    public int turn;

    private void Start()
    {
        phase = Phase.SelectHand;
    }

    private void Update()
    {
        switch(phase)
        {
            case Phase.SelectCharacterOrder:
                //選択
                //
                break;
            case Phase.SelectHand:

                break;
            case Phase.damage:
                
                break;
            default:
                break;
        }
    }

}
