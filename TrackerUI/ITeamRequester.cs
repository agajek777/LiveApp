﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveLibrary.Models;

namespace LiveAppUI
{
    public interface ITeamRequester
    {
        void TeamComplete(TeamModel model);
    }
}
