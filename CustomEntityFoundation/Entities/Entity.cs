using CustomEntityFoundation.Utilities;
using EntityFrameworkCore.BootKit;
using EntityFrameworkCore.Triggers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CustomEntityFoundation.Entities
{
    public abstract class Entity : DbRecord
    {
        private static List<IEntityNotification> instancesInCore = TypeHelper.GetInstanceWithInterface<IEntityNotification>(EntityDbContext.Assembles);

        public Entity()
        {
            UpdatedTime = DateTime.UtcNow;
            Status = EntityStatus.Active;
        }

        static Entity()
        {
            Triggers<Entity>.Inserting += entry =>
            {
                entry.Entity.UpdatedTime = DateTime.UtcNow;
                entry.Entity.Status = EntityStatus.Active;
            };

            Triggers<Entity>.Inserted += entry =>
            {
                instancesInCore.ForEach(x => x.OnRecordInserted(entry.Context, entry.Entity));
            };

            Triggers<Entity>.Deleting += entry =>
            {

            };

            Triggers<Entity>.Deleted += entry =>
            {
                instancesInCore.ForEach(x => x.OnRecordDeleted(entry.Context, entry.Entity));
            };

            Triggers<Entity>.Updating += entry =>
            {
                entry.Entity.UpdatedTime = DateTime.UtcNow;
            };

            Triggers<Entity>.Updated += entry =>
            {
                instancesInCore.ForEach(x => x.OnRecordUpdated(entry.Context, entry.Entity));
            };

            Triggers<Entity>.UpdateFailed += entry =>
            {

            };
        }

        public EntityStatus Status { get; set; }

        [Required]
        public DateTime UpdatedTime { get; set; }

        public virtual string GetEntityName()
        {
            Type type = this.GetType();
            return type.Name;
        }

        public virtual bool IsExist<T>(EntityDbContext dc) where T : Entity
        {
            return dc.Table<T>().Any(x => x.Id == Id);
        }
    }
}
