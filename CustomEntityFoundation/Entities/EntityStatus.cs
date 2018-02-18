using System;
using System.Collections.Generic;
using System.Text;

namespace CustomEntityFoundation.Entities
{
    public enum EntityStatus
    {
        /// <summary>
        /// New record, not in database yet
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Readable/ Writable/ Deletable
        /// </summary>
        Active = 1,

        /// <summary>
        /// Readonly
        /// </summary>
        Readonly = 2,

        /// <summary>
        /// Hide to user
        /// </summary>
        Inactive = 4,

        /// <summary>
        /// Deleted
        /// </summary>
        Deleted = 8
    }
}
