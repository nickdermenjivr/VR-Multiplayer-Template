using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class SceneSwitcherWithFade : MonoBehaviour
    {
        public Image fadeImage; // Ссылка на Image для фейда (черный экран)
        public float fadeDuration = 1f; // Длительность фейда

        private void Start()
        {
            // Устанавливаем начальную прозрачность на 1 (полностью черный)
            if (fadeImage != null)
            {
                fadeImage.color = new Color(0, 0, 0, 1);
                StartCoroutine(FadeIn()); // Запускаем FadeIn при старте сцены
            }
        }

        // Метод для загрузки сцены по её имени с эффектом FadeOut
        public void LoadSceneByName(string sceneName)
        {
            StartCoroutine(FadeOutAndLoad(sceneName));
        }

        // Метод для перезагрузки текущей сцены с эффектом FadeOut
        public void ReloadCurrentScene()
        {
            StartCoroutine(FadeOutAndLoad(SceneManager.GetActiveScene().name));
        }

        // Корутина для плавного появления изображения (FadeIn)
        IEnumerator FadeIn()
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                fadeImage.color = new Color(0, 0, 0, 1 - elapsedTime / fadeDuration);
                yield return null;
            }
            fadeImage.color = new Color(0, 0, 0, 0); // Полностью прозрачный
        }

        // Корутина для плавного исчезновения (FadeOut) и загрузки новой сцены
        IEnumerator FadeOutAndLoad(string sceneName)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                fadeImage.color = new Color(0, 0, 0, elapsedTime / fadeDuration);
                yield return null;
            }
            fadeImage.color = new Color(0, 0, 0, 1); // Полностью черный
            SceneManager.LoadScene(sceneName);
        }
    }
}