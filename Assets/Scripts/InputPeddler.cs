using Player;
using UnityEngine;
using Ui.Chat;

public class InputPeddler : MonoBehaviour
{
    private void Start()
    {
        InputSystem input = new InputSystem();
        input.Enable();
        FindObjectOfType<PlayerMovement>().Init(input);
        FindObjectOfType<Chat>().Init(input);
    }
}
