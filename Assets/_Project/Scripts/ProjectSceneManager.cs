using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class ProjectSceneManager : NetworkBehaviour
    {
        [SerializeField]
        private string sceneName;

        [ServerRpc(RequireOwnership = false)]
        public void LoadSceneServerRpc()
        {
            if (!IsServer) return;
            NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}