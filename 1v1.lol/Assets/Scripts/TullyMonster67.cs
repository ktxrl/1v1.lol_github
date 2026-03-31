using UnityEngine;

public class TullyMonster67 : MonoBehaviour
{
    [SerializeField] Enemy flying;
    [SerializeField] Enemy skeleton;
    [SerializeField] Enemy lizard;

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

        skeleton.GetComponent<Enemy>().Deselect();
        lizard.GetComponent<Enemy>().Deselect();
    }
    public void SkeletonSelect()
    {

        flying.GetComponent<Enemy>().Deselect();
        lizard.GetComponent<Enemy>().Deselect();
    }
    public void LizardSelect()
    {

        flying.GetComponent<Enemy>().Deselect();
        skeleton.GetComponent<Enemy>().Deselect();
    }
}
