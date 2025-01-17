using System.Collections.Generic; using UnityEngine; using UnityEngine.UI; 
public class CustomToggleGroup : MonoBehaviour 
{ 
    public List<Toggle> toggles; 
    public int MaxToggles = 2; 
    private Queue<Toggle> selectedToggles = new Queue<Toggle>(); 
    void Start() 
    { 
        foreach (var toggle in toggles) 
        { 
            toggle.onValueChanged.AddListener(delegate { OnToggleValueChanged(toggle); }); 
        } 
    }
    void OnToggleValueChanged(Toggle toggle) 
    { 
        if (toggle.isOn) 
        { 
            selectedToggles.Enqueue(toggle); 
            // Si hay más toggles seleccionados que el número máximo, destecla el más antiguo 
            if (selectedToggles.Count > MaxToggles) 
            { 
                Toggle t = selectedToggles.Dequeue(); 
                t.isOn = false;
            } 
        } 
        else 
        { 
            // Quita el toggle desteclado de la cola 
            if (selectedToggles.Contains(toggle)) 
            { 
                Queue<Toggle> tempQueue = new Queue<Toggle>(); 
                while (selectedToggles.Count > 0) 
                { 
                Toggle t = selectedToggles.Dequeue();
                if (t != toggle) 
                { 
                    tempQueue.Enqueue(t); 
                } 
            } 
            selectedToggles = tempQueue; 
            } 
        } 
    } 
}
