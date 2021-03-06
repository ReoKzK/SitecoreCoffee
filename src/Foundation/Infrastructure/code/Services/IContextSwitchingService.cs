﻿using System;
using Sitecore.Data;

namespace SitecoreCoffee.Foundation.Infrastructure.Services
{
    public interface IContextSwitchingService
    {
        bool IsExperienceEditor { get; }

        void SwitchContextItem(Guid id);
        void SwitchContextItem(ID id);
    }
}