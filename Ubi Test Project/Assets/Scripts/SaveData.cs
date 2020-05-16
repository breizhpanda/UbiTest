using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace UbiTest
{
    [SerializeField]
    public class SaveData
    {
        public string value;

        public SaveData(string defaultValue)
        {
            value = defaultValue;
        }

        public static SaveData CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<SaveData>(jsonString);
        }
    }
}
