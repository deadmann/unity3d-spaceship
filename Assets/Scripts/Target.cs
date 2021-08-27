using UnityEngine;

public class Target : MonoBehaviour
{
    private GameObject _graphicalGameObject;
    private GameObject _bangEffect;

    public float health = 5;

    private void Awake()
    {
        _graphicalGameObject = GetComponentInChildren<SpriteRenderer>().gameObject;
        _bangEffect = transform.Find("BangEffect").gameObject;
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Annihilated();
    }
    
    public void Annihilated()
    {
        Destroy(_graphicalGameObject);
        // _graphicalGameObject.SetActive(false);
        _bangEffect.SetActive(true);
        Destroy(gameObject, .5f);
    }
}