﻿using GameEstate.Core;

namespace GameEstate.Cry
{
    public class CryFileManager : CoreFileManager<CryFileManager, CryGame>
    {
        protected override CryFileManager Load()
        {
            _locations.Add(CryGame.StarCitizen, @"D:\Roberts Space Industries\StarCitizen\LIVE");
            return this;
        }
    }
}