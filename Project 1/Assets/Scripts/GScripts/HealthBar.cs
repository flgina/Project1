using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;

    [SerializeField] GameObject heartContainerPrefab;
    [SerializeField] List<GameObject> heartContainers;

    int totalHearts;
    float currentHearts;

    HeartContainer currentContainer;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        heartContainers = new List<GameObject>();

        //SceneManager.activeSceneChanged += LoadHealthBarLevelTwo;

       // SetupHearts(10);
    }

    public void SetupHearts(int heartsIn)
    {
        heartContainers.Clear();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        totalHearts = heartsIn;
        currentHearts = (float) totalHearts;

        for (int i = 0; i < totalHearts; i++)
        {
            GameObject newHeart = Instantiate(heartContainerPrefab, transform);
            heartContainers.Add(newHeart);

            if(currentContainer != null)
            {
                currentContainer.next = newHeart.GetComponent<HeartContainer>();
            }

            currentContainer = newHeart.GetComponent<HeartContainer>();

        }

        currentContainer = heartContainers[0].GetComponent<HeartContainer>();
    }

    public void SetCurrentHealth(float health)
    {
        currentHearts = health;
        currentContainer.SetHeart(currentHearts);
    }

    public void AddHearts(float healthUp)
    {
        currentHearts += healthUp;
        if (currentHearts > totalHearts)
        {
            currentHearts = (float) totalHearts;
        }

        currentContainer.SetHeart(currentHearts);
    }

    public void RemoveHearts(float healthDown)
    {
        currentHearts -= healthDown;
        if (currentHearts < 0)
        {
            currentHearts = 0f;
        }

        currentContainer.SetHeart(currentHearts);
    }

    // public void LoadHealthBarLevelTwo(Scene current, Scene next)
    // {
    //     if (next.buildIndex == 2) { print("yo"); SetupHearts(10); }
    // }
}
