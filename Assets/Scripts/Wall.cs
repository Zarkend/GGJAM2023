using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class Wall : Obstacle, IInteractable
    {
        public event Action<IInteractable> Destroyed;

        [SerializeField]
        private int hits;

        [SerializeField]
        private HealthBar nameplatePrefab;

        [SerializeField]
        private Transform nameplatePosition;

        [SerializeField]
        private GameObject destroyParticle;

        HealthBar _nameplateInstance;

        private void Awake()
        {
        }

        private void Start()
        {
            _nameplateInstance = Instantiate(nameplatePrefab, GameObject.Find("Nameplates").transform);

            _nameplateInstance.Initialize(hits);
        }

        private void Update()
        {
            _nameplateInstance.transform.position = Camera.main.WorldToScreenPoint(nameplatePosition.transform.position);
        }

        public void Interact()
        {
            Debug.Log("INTERACTED");
            hits -= 1;

            _nameplateInstance.SetHealth(hits);

            if (hits <= 0)
            {
                Destroyed?.Invoke(this);

                var rumble = Instantiate(destroyParticle);
                rumble.transform.position = transform.position;

                Destroy(_nameplateInstance.gameObject);
                Destroy(gameObject);
            }
        }

        [SerializeField]
        private int damage;

        private void OnTriggerEnter(Collider other)
        {
            var carretilla = other.GetComponent<Carretilla>();

            if (carretilla != null)
            {
                carretilla.Attack(damage);
                var rumble = Instantiate(destroyParticle);
                rumble.transform.position = transform.position;

                Destroy(_nameplateInstance.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
