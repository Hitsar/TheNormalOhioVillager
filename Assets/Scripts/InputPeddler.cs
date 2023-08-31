using Player;
using UnityEngine;
using Ui.Chat;

public class InputPeddler : MonoBehaviour
{
    private void Awake()
    {
        InputSystem input = new InputSystem();
        FindObjectOfType<PlayerMovement>().Init(input);
        FindObjectOfType<Chat>().Init(input);
    }
}
