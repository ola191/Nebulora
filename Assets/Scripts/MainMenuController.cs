using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject title;
    public GameObject button_newWorld;
    public GameObject button_loadWorld;
    public GameObject scrollView;
    public float transitionDuration = 0.5f;
    public Transform content;
    private Vector3 originalTitlePosition;

    GameObject worldItemPrefab;

    void Awake()
    {
        // Ładujemy prefabrykat w metodzie Awake
        worldItemPrefab = Resources.Load<GameObject>("Ui/prefabListViewButton");
    }

    void Start()
    {
        originalTitlePosition = title.transform.position;
        scrollView.SetActive(false);

        string[] worlds = { "World 1", "World 2", "World 3", "World 4", "World 5", "World 6", "World 2", "World 3", "World 4", "World 5", "World 6" };

        foreach (var world in worlds)
        {
            AddWorldToList(world);
        }
    }

    public void AddWorldToList(string worldName)
    {
        GameObject worldItem = Instantiate(worldItemPrefab, content);
        // worldItem.GetComponentInChildren<Text>().text = worldName;
        worldItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = worldName;
    }

    public void OnLoadWorldButtonClicked()
    {
        // Hide buttons
        StartCoroutine(FadeOutUIElement(button_newWorld));
        StartCoroutine(FadeOutUIElement(button_loadWorld));
        StartCoroutine(FadeOutUIElement(title));

        // Show scroll view
        scrollView.SetActive(true);
    }

    private System.Collections.IEnumerator FadeOutUIElement(GameObject uiElement)
    {
        float elapsedTime = 100f;
        CanvasGroup canvasGroup = uiElement.GetComponent<CanvasGroup>();

        // Jeśli nie ma komponentu CanvasGroup, dodajmy go
        if (canvasGroup == null)
        {
            canvasGroup = uiElement.AddComponent<CanvasGroup>();
        }

        while (elapsedTime < transitionDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiElement.SetActive(false);
    }
}
