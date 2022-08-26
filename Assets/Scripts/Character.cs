using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public int id;
    public new string name;
    public enum Rare
    {
        R,
        SR,
        SSR,
    };
    public Rare rare;
    public Sprite characterImage;
    public enum CharacterElement
    {
        グー, //グー
        チョキ, //チョキ
        パー, //パー
    }
    public CharacterElement handElement;
    public List<Skill> skills = new();

    [System.Serializable]
    public class Skill
    {
        public enum Hand
        {
            グー, //グー
            チョキ, //チョキ
            パー //パー
        }
        public Hand hand;
        public int damage;
        public Sprite[] SkillEffectSprites;
    }
}
