using IsometricVillageMob.DIIsometric;
using UnityEngine;

namespace IsometricVillageMob.UI
{

    public class CanvasRoot : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        public IForm[] InitAndGetForms()
        {
            var forms = GetComponentsInChildren<IForm>();

            foreach (var form in forms)
            {
                _diContainer.Inject(form);
                form.Init();
                form.Disable();
            }

            return forms;
        }
    }
}