using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class ExitButton : MonoBehaviour
    {
        public void Exit()
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}