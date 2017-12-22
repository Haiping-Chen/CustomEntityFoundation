using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomEntityFoundation.Entities
{
    public interface IEntityNotification
    {
        Task OnRecordInserted(DbContext dc, Entity record);
        Task OnRecordUpdated(DbContext dc, Entity record);
        Task OnRecordDeleted(DbContext dc, Entity record);
    }
}
