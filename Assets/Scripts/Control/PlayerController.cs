using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Combat;
using Game.Movement;
using Game.Core;

namespace Game.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        // Start is called before the first frame update
        public void Start()
        {
            health= GetComponent<Health>();
        }

        // Update is called once per frame
        public void Update()
        {
            if (health.isDead()) return;
            if (InteractWithCombat())
            {
                return;
            }
            //if (InteractWithPickups())
            //{
                //return;
            //}
            if (InteractWithMovement())
            {
                return;
            }
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null)  
                {
                    continue;
                }
                if (!target.GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }
        /*
        private bool InteractWithPickups()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                WeaponPickup target = hit.transform.GetComponent<WeaponPickup>();
                if (target == null)
                {
                    continue;
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().EquipWeapon(target.GetWeapon());
                    Destroy(target.gameObject);
                }
                return true;
            }
            return false;
        }*/
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}


