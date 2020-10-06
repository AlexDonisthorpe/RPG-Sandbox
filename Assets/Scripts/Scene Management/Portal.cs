using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player"){
            LoadScene(sceneToLoad);
            }
        }

        public void LoadScene(int buildIndex){
            SceneManager.LoadScene(buildIndex);
        }
    }   
}
