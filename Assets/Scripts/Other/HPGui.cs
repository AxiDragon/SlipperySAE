using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPGui : MonoBehaviour
{
    public List<FishHP> hpList = new List<FishHP>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int randomScene = Random.Range(0, SceneManager.sceneCountInBuildSettings - 1);
            SceneManager.LoadScene(randomScene);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void OnGUI()
    {
        hpList = FindObjectsOfType<FishHP>().ToList();

        Gizmos.color = Color.yellow;
        GUI.Label(new Rect(50, 50, 100, 100), "Press R for new Map");
        for (int i = 0; i < hpList.Count; i++)
        {
            GUI.Label(new Rect(50, 100 + i * 20, 100, 100), hpList[i].name.ToString() + " : " + hpList[i].health.ToString());
        }
    }
}
