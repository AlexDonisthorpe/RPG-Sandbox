using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics{
    public class CinematicControlRemover : MonoBehaviour
    {
        PlayableDirector playableDirector;
        GameObject player;

        private void Awake() {
            player = GameObject.FindWithTag("Player");
            playableDirector = GetComponent<PlayableDirector>();
        }

        private void OnEnable() {
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
        }

        private void OnDisable() {
            playableDirector.played -= DisableControl;
            playableDirector.stopped -= EnableControl;
        }
        
        private void Start() {
            playableDirector.Play();
        }

        void DisableControl(PlayableDirector played){
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector stopped){
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}