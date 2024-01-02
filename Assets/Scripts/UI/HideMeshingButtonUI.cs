using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HideMeshingButtonUI : MonoBehaviour
{
    private Button hideMeshButton;
    [SerializeField] private TextMeshProUGUI buttonText;


    private void Start()
    {
        hideMeshButton = GetComponent<Button>();

        if (hideMeshButton != null)
        {
            hideMeshButton.onClick.AddListener(ToggleHideMesh);
        }
    }

    private void ToggleHideMesh()
    {
        GameManager.Instance.ToggleHideMesh();

        if (buttonText != null)
        {
            if (GameManager.Instance.getMeshRenderState() == false)
            {
                buttonText.text = "Show Meshing";
            }
            else
            {
                buttonText.text = "Hide Meshing";
            }
        }
    }
}
