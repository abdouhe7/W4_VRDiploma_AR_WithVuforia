using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;
namespace vr
{
    public class Manger : MonoBehaviour
    {
        public List<GameObject> Models = new List<GameObject>();

        public Button next;
        public Button preview;
        public Text text;
        public VariableJoystick Joystick;
        public GameObject target;
        public Vector3 Plus = new Vector3(1f, 1f, 1f);
        public Vector3 ResetScale;
        public bool FaceYou;
        public GameObject ARCamira;
        int scene;

        public int Counter = 0;

        public VirtualButtonBehaviour VRup;
        public VirtualButtonBehaviour VRdown;
        public VirtualButtonBehaviour VRreset;

        public void LoadNextModel()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Counter++;

            GetModel(Models[Counter]);


        }
        public void LoadPreviewModel()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            Counter--;

            GetModel(Models[Counter]);
        }


        private void Start()
        {
            // transform. = 0;
            scene = SceneManager.GetActiveScene().buildIndex;
            if (VRup && VRdown && VRreset)
            {
                VRup.RegisterOnButtonPressed(ScaleUpV);
                VRdown.RegisterOnButtonPressed(ScaleDownV);
                VRreset.RegisterOnButtonPressed(ResetV);
            }
        }

        void GetModel(GameObject Model)
        {
            GameObject g = Instantiate(Model, Vector3.zero, Quaternion.identity) as GameObject;
            g.GetComponent<JoystickPlayerExample>().variableJoystick = Joystick;
            g.GetComponent<MeshRenderer>().enabled = true;
            g.transform.parent = GameObject.Find(this.name).transform;
            g.GetComponent<Transform>().position = transform.position;
            target = g;
            ResetScale = g.transform.localScale;

        }

        private void Update()
        {
            if (FaceYou)
            {
                Vector3 DirctionToFace = ARCamira.transform.position - transform.position;
                target.transform.rotation = Quaternion.LookRotation(DirctionToFace);
            }

            if (Counter == Models.Count - 1)
            {
                next.interactable = false;
            }
            else
            {
                next.interactable = true;
            }
            if (Counter == 0)
            {
                preview.interactable = false;
            }
            else
            {
                preview.interactable = true;
            }
        }
        public void targetFound()
        {
            GetModel(Models[Counter]);
        }
        public void targetLost()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void LoadNextSeen()
        {
            SceneManager.LoadScene(scene + 1);
        }
        public void LoadPreviewSeen()
        {
            SceneManager.LoadScene(scene - 1);
        }

        public void ScaleUp()
        {
            target.transform.localScale = target.transform.localScale + Plus * 5;
        }
        public void ScaleDown()
        {
            target.transform.localScale = target.transform.localScale - Plus * 5;
        }
        public void resetscale()
        {
            target.transform.localScale = ResetScale;
        }

        public void ScaleDownV(VirtualButtonBehaviour obj)
        {
            ScaleDown();
        }

        public void ScaleUpV(VirtualButtonBehaviour ob)
        {
            ScaleUp();
        }
        public void ResetV(VirtualButtonBehaviour o)
        {
            resetscale();
        }
        public void facecam()
        {
            if (FaceYou)
            {
                FaceYou = false;
            }
            else
            {
                FaceYou = true;
            }
        }
    }



}