using Player;
using UnityEngine;
using Ui.Chat;
using Ui.Menus.LoseMenu;
using Ui.Menus.Pause;

public class InputPeddler : MonoBehaviour
{
    private void Start()
    {
        InputSystem input = new InputSystem();
        input.Enable();
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.Init(input);
        player.GetComponentInChildren<PlayerAttack>().Init(input);
        FindObjectOfType<Chat>().Init(input);
        FindObjectOfType<PauseMenu>().Init(input);
        FindObjectOfType<LoseMenu>().Init(input);
    }
}
