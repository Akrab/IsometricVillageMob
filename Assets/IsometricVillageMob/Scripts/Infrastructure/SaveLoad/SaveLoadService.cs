﻿using Infrastructure.SaveLoad.Player;
using UnityEngine;

namespace IsometricVillageMob.Infrastructure.SaveLoad
{
    public class SaveLoadService
    {
        private PlayerInventory _playerInventory = new();

        public SaveLoadService()
        {
            _playerInventory.Load();
        }
        
        public void Save()
        {
            PlayerPrefs.Save();
        }
    }
}