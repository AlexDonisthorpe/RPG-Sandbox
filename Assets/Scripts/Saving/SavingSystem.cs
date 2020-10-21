using System.IO;
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
                byte[] bytes = Encoding.UTF8.GetBytes("¡Insert Save Data Here!");
                stream.Write(bytes, 0, bytes.Length);
            }
        }
        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);

            using(FileStream stream = File.Open(path, FileMode.Open))
            {
                byte[] byteBuffer = new byte[stream.Length];
                stream.Read(byteBuffer, 0, byteBuffer.Length);
                print(Encoding.UTF8.GetString(byteBuffer));    
            }
        }

        private string GetPathFromSaveFile(string saveFile){
            return Path.Combine(Application.dataPath, saveFile + ".sav");
        }
    }
}
