﻿using Instashop.Core;

namespace Instashop.Services.Interfaces;

public interface INavigationService
{
    BaseViewModel CurrentView
    {
        get;
    }

    void NavigateTo<T>() where T : BaseViewModel;
}
