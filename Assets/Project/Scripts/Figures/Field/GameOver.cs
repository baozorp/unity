using System.Collections;
using UnityEngine;
using TMPro;

public partial class Generator : MonoBehaviour
{
    IEnumerator GameOver()
    {
        _isPause = true;
        GameObject gameOverField = Camera.main.transform.GetChild(0).gameObject;
        for (int i = 0; i < gameOverField.transform.childCount; i++)
        {
            gameOverField.SetActive(true);
            if (gameOverField.transform.GetChild(i).name == "Text")
            {
                TextMeshPro textField = gameOverField.transform.GetChild(i).GetChild(0).GetComponent<TextMeshPro>();
                string savedText = textField.text;
                for (int j = 5; j > 0; j--)
                {
                    textField.text = savedText + "\n" +
                     "Игра возобновится через \n" + j.ToString();
                    yield return new WaitForSeconds(1f);
                }
                textField.text = savedText;
                gameOverField.SetActive(false);
            }
        }
        int count = transform.childCount;
        for (int childOfField = 0; childOfField < count; childOfField++)
        {
            var childToDel = transform.GetChild(childOfField).gameObject;
            Destroy(childToDel);
        }
        _isPause = false;
        Start();
    }
}