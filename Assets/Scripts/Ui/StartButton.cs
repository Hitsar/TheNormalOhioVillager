using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class StartButton : MonoBehaviour
    {
        public void StartGame() => SceneManager.LoadScene(1);
    }
}