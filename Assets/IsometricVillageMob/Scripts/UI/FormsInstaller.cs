using UnityEngine;

namespace IsometricVillageMob.IsometricVillageMob.UI
{
    public class FormsInstaller : MonoBehaviour
    {

        public IForm[] Init()
        {
            var forms = GetComponentsInChildren<IForm>();

            foreach (var form in forms)
            {
                form.Disable();
            }

            return forms;
        }
    }
}