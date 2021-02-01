using UnityEngine;

public class WoodBranchWhip : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets = null;

    private float time = 0;
    private bool segmentsInitialized = false;

    [HideInInspector] public GameObject origin = null;
    [HideInInspector] public float rotationSpeed = 0;
    [HideInInspector] public float lifeTime = 0;

    private void Awake()
    {

    }

    private void Update()
    {
        time += Time.deltaTime;

        if (!segmentsInitialized)
        {
            InitializeSegments();
        }

        // Whip motion is pretty basic
        // Could be changed so that each segment moves independently to be more whip-like
        // May not be worth the effort to do ^
        transform.Rotate(new Vector3(0, 0, -rotationSpeed));       

        if (time >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSegments()
    {
        foreach (Transform child in transform)
        {
            WoodBranch branch = child.gameObject.GetComponent<WoodBranch>();
            branch.origin = origin;
            branch.healing = true;
            branch.poison = true;
        }
        segmentsInitialized = true;
    }
}
