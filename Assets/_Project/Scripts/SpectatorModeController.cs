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

        private Transform _initialPosition;

        private void Start()
        {
            if (!manager)
            {
                Debug.LogError("No network manager assigned");
                return;
            }
            
            manager.QuickJoinLobby();
            manager.playerStateChanged += PlayerStateChanged;
            
            _initialPosition.position = ourHeadTransform.transform.position;
            _initialPosition.rotation = ourHeadTransform.transform.rotation;
        }

        private void PlayerStateChanged(ulong arg1, bool joined)
        {
            if (joined)
            {
                _currentPlayer = FindObjectOfType<XRAvatarVisuals>().transform.Find("Head");
                _playersConnected = true;
            }
            else
            {
                ourHeadTransform.position = _initialPosition.position;
                ourHeadTransform.rotation = _initialPosition.rotation;
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
            ourHeadTransform.transform.position = _currentPlayer.transform.position;
            ourHeadTransform.transform.rotation = _currentPlayer.transform.rotation;
        }
    }
}