﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtillerySeries.src
{
    class FireWeaponCommand : Command
    {
        public FireWeaponCommand()
        {
        }

        public override void Execute(Character c)
        {
            c.FireWeapon();
        }
    }
}
