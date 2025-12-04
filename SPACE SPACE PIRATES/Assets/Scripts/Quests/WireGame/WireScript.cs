using UnityEngine;
using UnityEngine.EventSystems;
public class WireScript : MonoBehaviour, IPointerClickHandler
{

    float[] rotations = {0f, 90f, 180f, 270f};

    [Header("Which rotations are correct for THIS piece?")]
    [SerializeField] private bool[] correctRotations = new bool[4];

    private int rotationIndex;  
    private WirePuzzleManager puzzleManager;

    private void Start()
    {
        rotationIndex = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0f, 0f, rotations[rotationIndex]);

        puzzleManager = GetComponentInParent<WirePuzzleManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        rotationIndex = (rotationIndex + 1) % rotations.Length;
        transform.eulerAngles = new Vector3(0f, 0f, rotations[rotationIndex]);

        if (puzzleManager != null)
        {
            puzzleManager.CheckIfSolved();
        }
    }

    public bool IsInCorrectRotation()
    {
        if (correctRotations == null || correctRotations.Length != 4)
            return false;

        return correctRotations[rotationIndex];
    }
}
