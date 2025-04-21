using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public abstract class Scene
    {
        protected SceneController controller;
        public Scene(SceneController controller)
        {
            this.controller = controller;
        }
        public abstract void Start();
        public abstract void Update();
        public abstract void End();
    }
}
