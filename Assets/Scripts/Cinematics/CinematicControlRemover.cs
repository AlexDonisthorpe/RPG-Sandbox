using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics{
    public class CinematicControlRemover : MonoBehaviour
    {
        PlayableDirector playableDirector;
        GameObject player;

        private void OnEnable() {
            playableDirector = GetComponent<PlayableDirector>();
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
        }
        
        private void Start() {
            player = GameObject.FindWithTag("Player");
            playableDirector.Play();
        }

        void DisableControl(PlayableDirector played){
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector stopped){
            player.GetComponent<PlayerController>().enabled = true;
        }

        private void OnDisable() {
            playableDirector.stopped -= EnableControl;
            playableDirector.played -= DisableControl;
        }
    }
}