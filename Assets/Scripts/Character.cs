using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public int id;
    public new string name;
    public Sprite characterImage;
    public enum HandElement
    {
        Goo, //グー
        Choki, //チョキ
        Paa, //パー
    }
    public HandElement handElement;
    public List<Skill> skills = new();

    [System.Serializable]
    public class Skill
    {
        public enum Hand
        {
            Goo, //グー
            Choki, //チョキ
            Paa //パー
        }
        public Hand hand;
        public int damage;
    }
}
