using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [CreateAssetMenu(fileName = "NewGameProgressData", menuName = "ScriptableObjects/NoFrogsAllowed/GameProgressData", order = 1)]
    public class GameProgressData : ScriptableObject
    {
        private const string PK_NFA_GAMEDATA = "Lego.SummerJam.NoFrogsAllowed.GameProgressData";

        public GameProgressDataModel Data { get; set; }

        public void LoadData()
        {
            string json = PlayerPrefs.GetString(PK_NFA_GAMEDATA, string.Empty);
            if (string.IsNullOrEmpty(json))
            {
                Data = new GameProgressDataModel
                { 
                    Level = 0,
                    Money = 600
                };
                return;
            }

            Data = JsonConvert.DeserializeObject<GameProgressDataModel>(json);
        }

        public void SaveData()
        {
            string json = JsonConvert.SerializeObject(Data);
            PlayerPrefs.SetString(PK_NFA_GAMEDATA, json);
        }
    }
}
