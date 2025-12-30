using UnityEngine;
using UnityEngine.UI;

public class HowToPlayButtonScript : MonoBehaviour
{
    public InstructionScript instructionScript;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (instructionScript == null || button == null) return;

        // Instructionï\é¶íÜÇÕHowToPlayButtonÇñ≥å¯âª
        button.interactable = !instructionScript.gameObject.activeSelf;
    }

    public void OnClickHowToPlay()
    {
        if (instructionScript == null) return;

        // îOÇÃÇΩÇﬂÉKÅ[Éh
        if (instructionScript.gameObject.activeSelf) return;

        instructionScript.OpenInstruction();
    }
}
