using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayerInGame : MonoBehaviour
{
    public GameObject charSelection;
    public string controlName;
    public GameObject avatarRight, avatarLeft;
    bool isRunning = false;

    // Update is called once per frame
    private void Update()
    {
        if (!isRunning && charSelection)
        {
            isRunning = true;
            CreatePlayerDeviceStart();
        }
    }
    private void CreatePlayerDeviceStart()
    {
        PlayerInput player;
        if (controlName == "Keyboard")
        {
            player = PlayerInput.Instantiate(charSelection, controlScheme: controlName, pairWithDevice: Keyboard.current);
        }
        else
        {
            player = PlayerInput.Instantiate(charSelection, controlScheme: controlName, pairWithDevice: Gamepad.current);
        }
        SpawnToPlayer(player);
    }

    private void SpawnToPlayer(PlayerInput player)
    {
        player.transform.parent = transform;
        player.transform.position = transform.position;
        player.transform.GetChild(0).gameObject.SetActive(false);

        SetAvatarToPlayer();
    }

    private void SetAvatarToPlayer()
    {
        if (transform.name == "Player1")
        {
            var nextPos = avatarLeft.transform.position;
            if (transform.GetChild(0).name.Contains("Si"))
            {
                GameObject.Find("Si_Avatar").transform.position = nextPos;
            }
            else
            {
                var obj = GameObject.Find("La_Avatar").transform;
                obj.position = nextPos;
                obj.localScale = new Vector3(-1, 1, 1);
            }
        }
        else if (transform.name == "Player2")
        {
            var nextPos = avatarRight.transform.position;
            if (transform.GetChild(0).name.Contains("Si"))
            {
                var obj = GameObject.Find("Si_Avatar").transform;
                obj.position = nextPos;
                obj.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                GameObject.Find("La_Avatar").transform.position = nextPos;
            }
        }
    }
}
