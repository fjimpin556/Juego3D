using TMPro;
using UnityEngine;

public class HistoryController : MonoBehaviour
{
    [SerializeField] GameObject cuadroDialogo;
    [SerializeField] TMP_Text dialogo;
    int historyCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("StartDialogue", 2);        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            passDialogue();
        }
    }

    void StartDialogue()
    {
        dialogo.text = "Hey, tú. ¿Puedes escucharme?";
        cuadroDialogo.SetActive(true);
    }

    public void passDialogue()
    {
        if (historyCount == 0)
        {
            dialogo.text = "No sé quién eres ni qué es lo que estas haciendo aquí... Pero tampoco me importa";
            historyCount += 1;
        }
    }
}
