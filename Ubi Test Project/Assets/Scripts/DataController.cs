using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace UbiTest
{
    public class DataController : SingletonBehaviour<DataController>
    {
        private string m_saveDataPath;
        private SaveData m_saveData;

        public SaveData saveData { get { return m_saveData; } }

        protected override void Awake()
        {
            base.Awake();
            
            m_saveDataPath = Path.Combine(Application.persistentDataPath, "saveData.json");
            m_saveData = LoadData();
        }

        private SaveData LoadData()
        {
            if (!File.Exists(m_saveDataPath))
            {
                Debug.Log("fichiers de sauvegarde non trouvés, création d'un nouveau fichier");
                return new SaveData();
            }
            else
            {
                using (StreamReader streamReader = File.OpenText(m_saveDataPath))
                {
                    string jsonString = streamReader.ReadToEnd();
                    return SaveData.CreateFromJSON(jsonString);
                }
            }
        }

        private void Save()
        {
            string jsonString = JsonUtility.ToJson(m_saveData);
            using (StreamWriter streamWriter = File.CreateText(m_saveDataPath))
            {
                streamWriter.Write(jsonString);
            }
        }

        public void SaveNewValue(string value)
        {
            m_saveData.value = value;
            Save();
        }
    }
}
