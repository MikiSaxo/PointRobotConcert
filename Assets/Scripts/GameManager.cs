using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField] public PlayerController Player { get; private set; }
    [field: SerializeField] public CanvasInventory CanvasInventory { get; private set; }
    public const string NextSceneKey = "NextScene";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("WTF");
        }
    }

    private void Start()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString(NextSceneKey, "Scene1"), LoadSceneMode.Additive);
        PlayerPrefs.DeleteKey(NextSceneKey);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Player.Move(mousePosWorld);

            RaycastHit2D hit = Physics2D.Raycast(mousePosWorld, Vector2.right, 0.01f);

            if (hit.collider != null)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    interactable.Execute();
                }
            }
        }
    }

    public void AddItem(Item item)
    {
        CanvasInventory.AddItem(item);
    }
}
