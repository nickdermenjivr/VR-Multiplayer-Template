using UnityEngine;
using XRMultiplayer;

namespace _Project.Scripts
{
    public class SpectatorModeController : MonoBehaviour
    {
        public Transform ourHeadTransform;
        public XRINetworkGameManager manager;
        
        private Transform _currentPlayer;
        private bool _playersConnected;
        private bool _firstPlayer = true;

        private void Start()
        {
            if (!manager)
            {
                Debug.LogError("No network manager assigned");
                return;
            }
            
            manager.playerStateChanged += PlayerStateChanged;
        }

        private void PlayerStateChanged(ulong arg1, bool joined)
        {
            if (_firstPlayer)
            {
                DeactivatePlayer();
                _firstPlayer = false;
                return;
            }
            
            if (joined)
            {
                _currentPlayer = FindObjectOfType<XRAvatarVisuals>().transform.Find("Head");
                _playersConnected = true;
            }
            else
            {
                _playersConnected = false;
            }
        }

        private void FixedUpdate()
        {
            if (_playersConnected)
            {
                SetupSpectator();
            }
        }
        private void SetupSpectator()
        {
            ourHeadTransform.position = _currentPlayer.position;
            ourHeadTransform.rotation = _currentPlayer.rotation;
        }

        public void DeactivatePlayer()
        {
            FindObjectOfType<XRAvatarVisuals>().gameObject.SetActive(false);
        }
    }
}