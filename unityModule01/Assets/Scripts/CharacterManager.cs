using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] characters; // Assign via Inspector: 0 - Blue, 1 - Red, 2 - Yellow
    public static CharacterManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // prevent duplicate managers
            return;
        }

        Instance = this;
    }

    public GameObject GetCharacter(int index)
    {
        if (index >= 0 && index < characters.Length)
            return characters[index];
        return null;
    }
}
