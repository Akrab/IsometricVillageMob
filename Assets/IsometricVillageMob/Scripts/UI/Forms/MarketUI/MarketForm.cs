using DG.Tweening;
using IsometricVillageMob.Game.Building;
using IsometricVillageMob.UI.CustomComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IsometricVillageMob.UI.Forms
{
    public class MarketForm : BaseContentForm,IBuildingForm 
    {
        [SerializeField] private ButtonExt _btnSell;
        [SerializeField] private ButtonExt _btnSelectResource;
        
        [SerializeField] private Image _resourceSelectedIco;

        [SerializeField] private TextMeshProUGUI _priceText;

        
        private void OnSell()
        {
          
        }
        
        private void OnSelectResource()
        {
          
        }
        
        protected override void setup()
        {
            _btnClose.onClick.AddListener(OnClose);
            _btnSell.onClick.AddListener(OnSell);
            _btnSelectResource.onClick.AddListener(OnSelectResource);

            _resourceSelectedIco.enabled = false;
            _priceText.text = "0";
        }

        public void Bind(IBuilding building)
        {
          
        }
    }
}