using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace UbiTest
{
    public class DataController : SingletonBehaviour<DataController>
    {
        #pragma warning disable 649
        [SerializeField] private char[] m_disabledCharacters;
        [SerializeField] private string m_defaultValue;
        [SerializeField] private int m_valueCharacterLimit;
        #pragma warning restore 649

        private string m_saveDataPath;
        private SaveData m_saveData;
        public SaveData saveData { get { return m_saveData; } }
        public int valueCharacterLimit { get { return m_valueCharacterLimit; } }

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
                return new SaveData(m_defaultValue);
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

        public void EraseValue()
        {
            m_saveData.value = m_defaultValue;
            Save();
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
            if (!IsValueValid(value))
                return;
            m_saveData.value = value;
            Save();
        }

        public bool IsValueValid(string value)
        {
            foreach(char c in m_disabledCharacters)
            {
                if (value.IndexOf(c) != -1)
                    return false;
            }
            return true;
        }
    }
}
