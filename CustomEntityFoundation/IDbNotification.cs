using CustomEntityFoundation.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomEntityFoundation
{
    public interface IDbNotification
    {
        Task OnDbInserted(DbContext dc, Entity record);
        Task OnDbUpdated(DbContext dc, Entity record);
        Task OnDbDeleted(DbContext dc, Entity record);
    }
}
