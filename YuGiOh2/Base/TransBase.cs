﻿using System.Collections.Generic;

namespace YuGiOh2.Base
{
    public class MessageToClient
    {
        public string UID { get; set; }
        public string Winner { get; set; }
        public bool FirstTurn { get; set; }
        public bool Enable { get; set; }
        public bool CanSummon { get; set; }
        public int PlayerHP { get; set; }
        public int EnemyHP { get; set; }
        public int PlayerDeckCount { get; set; }
        public int EnemyDeckCount { get; set; }
        public List<Card> PlayerHands { get; set; }
        public int EnemyHandsCount { get; set; }
        public Field PlayerField { get; set; }
        public Field EnemyField { get; set; }
        public List<Card> PlayerGrave { get; set; }
        public List<Card> EnemyGrave { get; set; }
        public int ChooseTargetType { get; set; }
        public string Message { get; set; }
    }

    public class MessageFactory
    {
        public static MonsterCard FaceDownM(string uid)
        {
            var faceDownM = new MonsterCard();
            faceDownM.Status.FaceDown = true;
            faceDownM.Status.DefensePosition = true;
            faceDownM.UID = uid;
            return faceDownM;
        }

        public static SpellAndTrapCard FaceDownSAT(string uid)
        {
            var faceDownSAT = new SpellAndTrapCard();
            faceDownSAT.Status.FaceDown = true;
            faceDownSAT.UID = uid;
            return faceDownSAT;
        }

        public static MessageToClient GetGameMessage(Player player, Player enemy, string uid)
        {
            MessageToClient message = new MessageToClient();
            if (player != null)
            {
                message.PlayerDeckCount = player.Deck.Count;
                message.PlayerField = player.Field;
                message.PlayerGrave = player.Grave;
                message.PlayerHP = player.HP;
                message.PlayerHands = player.Hands;
                message.CanSummon = player.CanSummon;
                message.Enable = player.YourTurn;
                message.FirstTurn = player.FirstTurn;
                message.ChooseTargetType = player.ChooseTarget;
                message.Message = player.Message;
            }
            if (enemy != null)
            {
                message.EnemyDeckCount = enemy.Deck.Count;
                message.EnemyField = ProcessEnemyField(enemy.Field);
                message.EnemyGrave = enemy.Grave;
                message.EnemyHP = enemy.HP;
                message.EnemyHandsCount = enemy.Hands.Count;
            }
            message.UID = uid;
            if (player != null && enemy != null)
            {
                if (player.Lose && enemy.Lose)
                {
                    message.Winner = "Draw";
                    return message;
                }
                if (player.Lose)
                {
                    message.Winner = "Enemy";
                    return message;
                }
                if (enemy.Lose)
                {
                    message.Winner = "Player";
                }
            }
            return message;
        }

        private static Field ProcessEnemyField(in Field field)
        {
            Field copy = new Field();
            for (int i = 0; i < 5; i++)
            {
                MonsterCard m = field.MonsterFields[i];
                if (m != null)
                {
                    copy.MonsterFields[i] = m.Status.FaceDown ? FaceDownM(m.UID) : m;
                    copy.MonsterFields[i].UID = m.UID;
                }
                SpellAndTrapCard s = field.SpellAndTrapFields[i];
                if (s != null)
                {
                    copy.SpellAndTrapFields[i] = s.Status.FaceDown ? FaceDownSAT(s.UID) : s;
                    copy.SpellAndTrapFields[i].UID = s.UID;
                }
            }
            if (field.FieldField != null)
            {
                copy.FieldField = field.FieldField.Status.FaceDown ? FaceDownSAT(field.FieldField.UID) : field.FieldField;
                copy.FieldField.UID = field.FieldField.UID;
            }
            return copy;
        }
    }
}
