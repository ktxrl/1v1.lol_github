using UnityEngine;

public class TullyMonster67 : MonoBehaviour
{
    [SerializeField] Flying flying;
    [SerializeField] Skeleton skeleton;
    [SerializeField] Lizard lizard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FlyingSelect()
    {

        skeleton.GetComponent<Skeleton>().Deselect();
        lizard.GetComponent<Lizard>().Deselect();
    }
    public void SkeletonSelect()
    {

        flying.GetComponent<Flying>().Deselect();
        lizard.GetComponent<Lizard>().Deselect();
    }
    public void LizardSelect()
    {
        flying.GetComponent<Flying>().Deselect();
        skeleton.GetComponent<Skeleton>().Deselect();
    }
}
