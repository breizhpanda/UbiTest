using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UbiTest
{
    public class DataModifierUI : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] private InputField m_inputField;
        [SerializeField] private Text m_valueDisplay;
        [SerializeField] private Text m_invalidInputmessage;
        [SerializeField] private Button m_saveButton;
        [SerializeField] private Button m_loadButton;
#pragma warning restore 649

        private DataController m_dataController;
        
        void Start()
        {
            m_dataController = DataController.Instance;
            m_inputField.onValueChanged.AddListener(OnInputFieldValueChanged); 
        }

        private void UpdateDisplayedValue()
        {
            m_valueDisplay.text = m_dataController.saveData.value;
        }

        private void OnInputFieldValueChanged(string value)
        {
            bool isValid = m_dataController.IsValueValid(value);
            m_saveButton.interactable = isValid;
            m_loadButton.interactable = isValid;
            m_invalidInputmessage.gameObject.SetActive(!isValid);
        }

        public void SaveData()
        {
            m_dataController.SaveNewValue(m_inputField.text);
            //UpdateDisplayedValue();
        }

        public void LoadData()
        {
            UpdateDisplayedValue();
        }

        public void ResetData()
        {
            m_dataController.EraseValue();
            //UpdateDisplayedValue();
        }
        
        void Destroy()
        {
            m_inputField.onValueChanged.RemoveAllListeners();
        }
    }
}
