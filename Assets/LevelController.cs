using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.UI
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu;

        private bool isPaused;

        private void Start()
        {
            pauseMenu.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }

        private void Pause()
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                isPaused = true;
            }
        }

        public void ReloadLevel()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL("https://itch.io/jam/gjl-game-parade");
        #else
            Application.Quit();
        #endif
        }
    }
}
