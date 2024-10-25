using Unity.Services.Vivox;
using UnityEngine;
using XRMultiplayer;

namespace _Project.Scripts
{
    public class SpectatorModeController : MonoBehaviour
    {
        public Transform ourHeadTransform;
        public XRINetworkGameManager manager;
        public GameObject xrOriginHands;
        
        private Transform _currentPlayer;
        private bool _playersConnected;
        private bool _firstPlayer = true;

        private void Start()
        {
            if (!manager)
            {
                Debug.LogError("Manager isn't assigned!");
                return;
            }

            manager.playerStateChanged += PlayerStateChanged;
            VivoxService.Instance.ParticipantAddedToChannel += DeactivatePcXR;
        }
        private void FixedUpdate()
        {
            if (_playersConnected)
            {
                SetupSpectator();
            }
        }
        private void DeactivatePcXR(VivoxParticipant obj)
        {
            FindObjectOfType<XRAvatarVisuals>().gameObject.SetActive(false);
            //xrOriginHands.SetActive(false);
            ourHeadTransform.gameObject.SetActive(true);
            VivoxService.Instance.ParticipantAddedToChannel -= DeactivatePcXR;
        }
        
        private void PlayerStateChanged(ulong arg1, bool joined)
        {
            if (_firstPlayer)
            {
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
        
        private void SetupSpectator()
        {
            ourHeadTransform.position = _currentPlayer.position;
            ourHeadTransform.rotation = _currentPlayer.rotation;
        }
    }
}