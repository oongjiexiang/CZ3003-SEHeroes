using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Groups_Table : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private void Awake() 
    {
        entryContainer = transform.Find("Tutorial_Entry_Container");
        entryTemplate = entryContainer.Find("Tutorial_Entry_Template");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 40f;
        for (int i = 0; i < 40; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int order = i + 1;
            string orderString;
            switch (order)
            {
                default: orderString = order.ToString(); break;
                case 1: orderString = "1"; break;
                case 2: orderString = "2"; break;
                case 3: orderString = "3"; break;
            }

            entryTransform.Find("orderText").GetComponent<Text>().text = orderString;

            int index = Random.Range(0, 10000);
            entryTransform.Find("tutorialText").GetComponent<Text>().text = index.ToString();

            int studentNo = Random.Range(0, 10000);
            entryTransform.Find("studentNoText").GetComponent<Text>().text = studentNo.ToString();

            string manageLink = "manage";
            entryTransform.Find("manageText").GetComponent<Text>().text = manageLink;

        }

    }
}
