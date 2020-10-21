using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour 
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 position = new SerializableVector3(playerTransform.position);

                formatter.Serialize(stream, position);
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);

            using(FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 position = (SerializableVector3) formatter.Deserialize(stream);
                GetPlayerTransform().position = position.ToVector();
            }
        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }


        private string GetPathFromSaveFile(string saveFile){
            return Path.Combine(Application.dataPath, saveFile + ".sav");
        }
    }
}
