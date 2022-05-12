using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionMenu : MonoBehaviour{
    public GameObject interactionMenu;

    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        interactionMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.nearDoor){
            interactionMenu.SetActive(true);
            text.text = "'E' to start run!";
        }
        else if (Player.nearUpStation){
            interactionMenu.SetActive(true);
            text.text = "'E' for shop!";
        }
        else if (Player.nearTeleporter){
            interactionMenu.SetActive(true);
            text.text = "'T' to teleport!";
        }
        else{
            interactionMenu.SetActive(false);
        }
    }
}
