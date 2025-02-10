using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistoryController : MonoBehaviour
{
    [SerializeField] GameObject cuadroDialogo;
    [SerializeField] TMP_Text dialogo;
    [SerializeField] Image ImageDial;
    [SerializeField] Sprite unk;
    [SerializeField] Sprite police;
    [SerializeField] Sprite playImg;
    [SerializeField] TMP_Text speaker;
    public static int historyCount = 8;

    bool canFinger = false;
    public static bool finger = false;
    [SerializeField] GameObject fingerDisp;
    public static bool infected = false;
    public static bool infectingDeath = false;
    [SerializeField] Slider infBar;
    [SerializeField] GameObject barCont;
    float infectionCount = 180;
    public static bool antidoto = false;
    [SerializeField] GameObject antidotoDisp;

    public AgentFirstControl AgFirstC;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("StartDialogue", 2);
        historyCount = 8;        
        antidoto = false;
        antidotoDisp.SetActive(false);
        finger = false;
        fingerDisp.SetActive(false);
        infected = false;
        barCont.SetActive(false);
        infectingDeath = false;
        infectionCount = 180;
    }

    // Update is called once per frame
    void Update()
    {
        if (infected)
        {
            barCont.SetActive(true);
            infectionCount -= Time.deltaTime;
            infBar.value = infectionCount;
            if (infectionCount <= 0)
            {
                infectingDeath = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            passDialogue();
        }

        if (historyCount == 9 || historyCount == 12 || historyCount == 20)
        {
            passDialogue();
        }        
        else if (historyCount == 23 || historyCount == 43 || historyCount == 31)
        {
            passDialogue();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            historyCount = 22;
        }
        if (antidoto == true)
        {
            antidotoDisp.SetActive(true);
        }

        if (finger == true)
        {
            fingerDisp.SetActive(true);
        }
        else
        {
            fingerDisp.SetActive(false);
        }
        if (antidoto == true)
        {
            passDialogue();
        }
    }

    void StartDialogue()
    {
        dialogo.text = "Hey, tú. ¿Puedes escucharme?";
        cuadroDialogo.SetActive(true);
        speaker.text = "¿?";
        ImageDial.sprite = unk;
        historyCount = 0;
    }

    public void passDialogue()
    {
        if (historyCount == 0)
        {
            dialogo.text = "No sé quién eres ni qué es lo que estas haciendo aquí... Pero tampoco me importa.";
            historyCount = 1;
        }
        else if (historyCount == 1)
        {
            dialogo.text = "Mira, trataré de ser breve. El gobierno está realizando ciertos experimentos en este lugar. Están probando con los prisioneros una especie de virus que te convierte en zombie.";
            historyCount = 2;
        }
        else if (historyCount == 2)
        {
            dialogo.text = "A ti también te lo han inyectado cuando te metieron aquí. No deberías tardar mucho en empezar a sentir los primeros síntomas.";
            historyCount = 3;
        }
        else if (historyCount == 3)
        {
            dialogo.text = "También te pusieron un microchip para monitorizar tu progreso durante la infección. He podido hackearlo y así me estoy comunicando contigo.";
            historyCount = 4;
        }
        else if (historyCount == 4)
        {
            dialogo.text = "Tendrás que escapar de la prisión. Pero no servirá de nada en tu estado actual. Por supuesto, los dirigentes de la celda cuentan con un antídoto, por si en alguna ocasión algo sale mal y terminan infectados. Deberás encontrarlo antes de salir. Solo así podrás curarte.";
            historyCount = 5;
        }
        else if (historyCount == 5)
        {
            dialogo.text = "He introducido un mapa a través de tu chip. En él he marcado la salida. Sin embargo, no tengo ni idea de donde pueden guardar el antídoto, por lo que tendrás que buscarlo por tu cuenta.";
            historyCount = 6;
        }
        else if (historyCount == 6)
        {
            dialogo.text = "También te he dejado una pistola. Tómala, aunque notarás que su munición es limitada.";
            ControlPlayer.canPistol = true;
            historyCount = 7;
        }
        else if (historyCount == 7)
        {
            cuadroDialogo.SetActive(false);
            historyCount = 8;
        }
        else if (historyCount == 9)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "En los próximos instantes, un policía debería pasar por la puerta.";
            historyCount = 10;
            AgFirstC.startMoving();
        }
        else if (historyCount == 10)
        {
            cuadroDialogo.SetActive(false);
            historyCount = 11;
        }
        else if (historyCount == 12)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "Oh, Maldita sea. ¿Cómo se supone que has conseguido ese arma?";
            speaker.text = "Policía";
            ImageDial.sprite = police;
            historyCount = 13;
        }
        else if (historyCount == 13)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "No dispares. A parte de ahorrar munición, si lo haces, es posible que te escuchen y vengan muchos guardias más.";
            speaker.text = "¿?";
            ImageDial.sprite = unk;
            historyCount = 14;
        }
        else if (historyCount == 14)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "Debería llevar las llaves de tu celda encima. Trata de conseguirlas.";            
            historyCount = 15;
        }
        else if (historyCount == 15)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "Si no quieres que dispare, será mejor que me des las llaves de mi celda y te vayas por donde has venido.";
            speaker.text = "Jugador";
            ImageDial.sprite = playImg;
            historyCount = 16;
        }
        else if (historyCount == 16)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "¡Vale, vale! Está bien, toma las malditas llaves. No durarás mucho más.";
            speaker.text = "Policía";
            ImageDial.sprite = police;
            historyCount = 17;
            AgFirstC.startGoing();
        }
        else if (historyCount == 17)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "Con la llave, deberías de poder abrir la puerta. Ahora, a escapar.";
            speaker.text = "¿?";
            ImageDial.sprite = unk;
            historyCount = 18;            
            DoorOpenControl.key1 = true;
            Invoke("QuitDialogue2", 5);
        }
        else if (historyCount == 18)
        {
            cuadroDialogo.SetActive(false);
            historyCount = 19;            
        }
        else if (historyCount == 20)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "Algunas puertas están abiertas. No es buena señal.";            
            historyCount = 21;     
        }
        else if (historyCount == 21)
        {
            cuadroDialogo.SetActive(false);
            historyCount = 22;   
        }
        else if (historyCount == 23 && !infected)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "Parece que hae falta una huella dactilar reconocida. Esto va a ser más difícil de lo que pensaba.";            
            canFinger = true;
            historyCount = 24;     
        }
        else if (historyCount == 43 && infected)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "Parece que hae falta una huella dactilar reconocida. Tendrás que volver a la sala donde mataste a los guardias y coger un dedo. Date prisa, queda poco tiempo.";            
            canFinger = true;
            historyCount = 44;     
        }
        else if (historyCount == 24)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "Vas a tener que acabar con algún guardia y robarle un dedo. Es la única manera de conseguir ese antídoto.";            
            historyCount = 25;     
        }
        else if (historyCount == 25)
        {
            cuadroDialogo.SetActive(false);
            historyCount = 26;   
        }
        else if (historyCount == 31)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "Parece ser que ya han aparecido los primeros síntomas. A partir de ahora, intuyo que deben quedarte unos 3 minutos para convertirte en un zombie.";            
            infected = true;
            historyCount = 32;     
        }
        else if (historyCount == 32 && canFinger)
        {
            dialogo.text = "Rápido. Cortale el dedo al policía y ve a abrir la puerta. No te queda mucho tiempo";            
            historyCount = 33; 
        }
        else if (historyCount == 32 && !canFinger)
        {
            dialogo.text = "Rápido. Tienes que encontrar el antídoto. No te queda mucho tiempo";            
            historyCount = 41; 
        }
        else if (historyCount == 33)
        {
            cuadroDialogo.SetActive(false);           
            historyCount = 34; 
        }        
        else if (historyCount == 41)
        {
            cuadroDialogo.SetActive(false);           
            historyCount = 42; 
        }        
        else if (historyCount == 44)
        {
            cuadroDialogo.SetActive(false);           
            historyCount = 45; 
        }        
        else if (historyCount == 49)
        {
            cuadroDialogo.SetActive(true);
            dialogo.text = "Rápido, ahora que tienes el antídoto, puedes irte.";
            infected = true;
            historyCount = 50; 
            Invoke("QuitDialogue3", 3); 
        }              
    }

    public void QuitDialogue1()
    {
        cuadroDialogo.SetActive(false);
        historyCount = 8;
    }
    public void QuitDialogue2()
    {
        cuadroDialogo.SetActive(false);
        historyCount = 19;
    }
    public void QuitDialogue3()
    {
        cuadroDialogo.SetActive(false);
        historyCount = 51;
    }
}
