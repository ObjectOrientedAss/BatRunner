using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerControls playerControls;

    public float movementHorizontal { get; private set; }
    public float movementVertical { get; private set; }
    public float rotationDirection { get; private set; }

    public float cameraHorizontal { get; private set; }
    public float cameraVertical { get; private set; }

    public bool attackButtonPressed { get; private set; }
    public bool aimButtonPressed { get; private set; }
    public bool coverButtonPressed { get; private set; }
    public bool dodgeButtonPressed { get { return playerControls.GamePlay.Dodge.triggered; } }
    public bool jumpButtonPressed { get { return playerControls.GamePlay.Jump.triggered; } }
    public bool leanButtonPressed { get { return playerControls.GamePlay.Lean.triggered; } }
    public bool interactButtonPressed { get { return playerControls.GamePlay.Interact.triggered; } }


    public PlayerControls GetPlayerControls()
    {
        return playerControls;
    }

    private void SetPlayerControls()
    {
        playerControls = new PlayerControls();
        playerControls.GamePlay.Enable();
        SetGamePlayCallbacks();
    }

    public void Init()
    {
        SetPlayerControls();
    }

    private void SetGamePlayCallbacks()
    {
        //MOVEMENT
        playerControls.GamePlay.Movement.performed += ctx =>
        {
            movementHorizontal = playerControls.GamePlay.Movement.ReadValue<Vector2>().x;
            movementVertical = playerControls.GamePlay.Movement.ReadValue<Vector2>().y;
        };
        playerControls.GamePlay.Movement.canceled += ctx => { movementHorizontal = 0; movementVertical = 0; };

        //LOOK FREE - LOOK AIM
        playerControls.GamePlay.Look.performed += ctx =>
        {
            cameraHorizontal = Mathf.Clamp(playerControls.GamePlay.Look.ReadValue<Vector2>().x, -1, 1);//camera horizontal value
            cameraVertical = Mathf.Clamp(playerControls.GamePlay.Look.ReadValue<Vector2>().y, -1, 1);//camera vertical input
        };
        playerControls.GamePlay.Look.canceled += ctx => { cameraHorizontal = 0; cameraVertical = 0; };

        //START - STOP AIM
        playerControls.GamePlay.Aim.started += ctx => aimButtonPressed = true;
        playerControls.GamePlay.Aim.canceled += ctx => aimButtonPressed = false;

        //ATTACK
        playerControls.GamePlay.Attack.started += ctx => attackButtonPressed = true;
        playerControls.GamePlay.Attack.canceled += ctx => attackButtonPressed = false;

        //SWITCH COVER / CORNER
        playerControls.GamePlay.Cover.started += ctx => coverButtonPressed = true;
        playerControls.GamePlay.Cover.canceled += ctx => coverButtonPressed = false;
    }

    //private void SetTowerDefenceCallbacks()
    //{
    //    //MOVEMENT
    //    playerControls.TowerDefence.Movement.performed += ctx =>
    //    {
    //        movementHorizontal = playerControls.TowerDefence.Movement.ReadValue<Vector2>().x;
    //        movementVertical = playerControls.TowerDefence.Movement.ReadValue<Vector2>().y;
    //    };
    //    playerControls.TowerDefence.Movement.canceled += ctx => { movementHorizontal = 0; movementVertical = 0; };

    //    //LOOK FREE - LOOK AIM
    //    playerControls.TowerDefence.Look.performed += ctx =>
    //    {
    //        cameraHorizontal = Mathf.Clamp(playerControls.TowerDefence.Look.ReadValue<Vector2>().x, -1, 1);//camera horizontal value
    //        cameraVertical = Mathf.Clamp(playerControls.TowerDefence.Look.ReadValue<Vector2>().y, -1, 1);//camera vertical input
    //    };
    //    playerControls.TowerDefence.Look.canceled += ctx => { cameraHorizontal = 0; cameraVertical = 0; };

    //    //ATTACK
    //    playerControls.TowerDefence.Attack.started += ctx => attackButtonPressed = true;
    //    playerControls.TowerDefence.Attack.canceled += ctx => attackButtonPressed = false;

    //    //START - STOP AIM
    //    playerControls.TowerDefence.Aim.started += ctx => aimButtonPressed = true;
    //    playerControls.TowerDefence.Aim.canceled += ctx => aimButtonPressed = false;
    //}

    private void EnableAllInputs()
    {
        playerControls.GamePlay.Enable();
    }

    private void DisableAllInputs()
    {
        playerControls.GamePlay.Disable();
    }
}