﻿using System;
using InformationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class RunEditViewModel : EditViewModel
{
    public RunEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }

    protected override bool CanSave()
    {
        throw new NotImplementedException();
    }

    protected override void ExecuteSave()
    {
        throw new NotImplementedException();
    }

    protected override void ExecuteRemove()
    {
        throw new NotImplementedException();
    }
}