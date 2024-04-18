using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Game;
using IsometricVillageMob.Game.Building;
using IsometricVillageMob.Infrastructure.Containers;
using IsometricVillageMob.Infrastructure.Controllers.Inputs;
using IsometricVillageMob.UI;
using IsometricVillageMob.UI.Forms;
using UnityEngine;

namespace IsometricVillageMob.Infrastructure.Controllers
{
    public class SelectBuildingController
    {
        [Inject] private IInputController _inputController;

        [Inject] private UIContainer _uiContainer;
        
        private void Click(Transform transform)
        {
            var building = transform.GetComponent<IBuilding>();
            if (building == null) return;
            
            var form = GetForm(building.BuildingType);
            if (form == null) return;

            form.Show();

        }

        private IForm GetForm(BuildingType target)
        {
            switch (target)
            {
                case BuildingType.Manufacture:
                    return _uiContainer.GetForm<ManufactureForm>();
                case BuildingType.Market:
                    return _uiContainer.GetForm<MarketForm>();
                case BuildingType.Resource:
                    return _uiContainer.GetForm<ResourceForm>();
                
                default: return null;
            }
        }
        
        public void Init()
        {
            _inputController.AddListener(Click);
        }

    }
}