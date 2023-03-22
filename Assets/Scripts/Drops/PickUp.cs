using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField] float percentHeal;
    [SerializeField] DropsType dropsType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (dropsType == DropsType.Heal)
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.Heal(percentHeal);
                }
            }
            if (dropsType == DropsType.Magnet)
            {
                List<ExperienceGem> gems = FindObjectsOfType<ExperienceGem>(true).ToList();
                //StartCoroutine(MoveGems());
                foreach (ExperienceGem gem in gems)
                {
                    gem.isSuck = true;
                }
            }
            Destroy(gameObject);
        }

    }

    //IEnumerator MoveGems()
    //{
    //    List<ExperienceGem> gems = FindObjectsOfType<ExperienceGem>(true).ToList();
    //    PlayerMover player = FindObjectOfType<PlayerMover>();
    //    foreach (ExperienceGem gem in gems)
    //    {
    //        gem.transform.position = Vector2.Lerp(gem.transform.position, player.transform.position,
    //        20f * Time.deltaTime
    //        );
    //    }
    //    yield return null;
    //}
}

public enum DropsType
{
    Heal, 
    Magnet
}
