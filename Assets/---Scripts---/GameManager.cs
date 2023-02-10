using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField] public PlayerController Player { get; private set; }
    // [field: SerializeField] public CanvasInventory CanvasInventory { get; private set; }
    [SerializeField] private Transform[] _spawnPointsPlayer;
    [SerializeField] private LayerMask _layerMask;


    public const string NextSceneKey = "NextScene";

    private IInteractable _lastTouchedInterac;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("WTF");
    }

    private void Start()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString(NextSceneKey, "1.Maison"), LoadSceneMode.Additive);
        PlayerPrefs.DeleteKey(NextSceneKey);
        ChooseSpawnPlayer(CanvasInventory.Instance.SaveLastDoorSide ? 1 : 0);
    }

    private void ChooseSpawnPlayer(int pos)
    {
        Player.transform.position = _spawnPointsPlayer[pos].position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if(!CanvasInventory.Instance.IsMouseOnUI)
                Player.Move(mousePosWorld);

            RaycastHit2D hit = Physics2D.Raycast(mousePosWorld, Vector2.right, 0.01f,_layerMask);

            if (hit.collider != null)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Execute();
                    _lastTouchedInterac = interactable;
                }
                else
                {
                    if (_lastTouchedInterac != null)
                        _lastTouchedInterac.ResetClicked();
                    // _lastTouchedInterac = null;
                }
            }
            else
            {
                if (_lastTouchedInterac != null)
                    _lastTouchedInterac.ResetClicked();
            }
        }
    }

    public void AddItem(Item item)
    {
        CanvasInventory.Instance.AddItem(item);
    }
}