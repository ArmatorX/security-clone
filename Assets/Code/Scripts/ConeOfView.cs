using UnityEngine;

public class ConeOfView : MonoBehaviour
{
    private EntityWithCoV _entity;
    public EntityWithCoV Entity
    {
        get 
        {
            if (_entity == null)
            {
                _entity = GetComponentInParent<EntityWithCoV>();
            }

            return _entity;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Entity.OnSeenPlayer();
    }
}
