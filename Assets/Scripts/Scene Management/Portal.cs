using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 2f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float loadTimeDelay = 1f;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0){
                Debug.LogError("sceneToLoad is not set.");
                yield break;
            }

            Fader fader = FindObjectOfType<Fader>();            
            DontDestroyOnLoad(gameObject);
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            yield return new WaitForSeconds(loadTimeDelay);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            foreach(Portal portal in portals){
                if(portal == this) continue;
                if(portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
    }   
}
