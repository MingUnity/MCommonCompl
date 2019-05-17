using UnityEngine;

namespace MingUnity.InputModule
{
    [System.Serializable]
    public class MCamera
    {

        public Camera camera;
        public bool guiCamera;

        public MCamera(Camera cam, bool gui)
        {
            this.camera = cam;
            this.guiCamera = gui;
        }

    }
}
