using CustomEntityFoundation;
using CustomEntityFoundation.Entities;
using EntityFrameworkCore.BootKit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;

namespace CustomEntityFoundation.Views
{
    public class ViewAction : Entity, IDbRecord
    {
        [JsonIgnore]
        [StringLength(36)]
        public String ViewId { get; set; }

        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(32)]
        public String Icon { get; set; }

        /// <summary>
        /// Actions show in data line lelve or view level
        /// </summary>
        public Boolean IsViewLevel { get; set; }

        public Boolean ConfirmBeforeSubmit { get; set; }

        [MaxLength(128)]
        public String ConfirmMessage { get; set; }

        [MaxLength(256)]
        public String Tooltip { get; set; }

        [MaxLength(32)]
        public String RestApiHost { get; set; }

        [MaxLength(128)]
        public String RestApiPath { get; set; }

        [NotMapped]
        public String RequestUrl
        {
            get
            {
                if (String.IsNullOrEmpty(RestApiHost))
                {
                    return RestApiPath;
                }

                return String.IsNullOrEmpty(RestApiPath) ? "" : $"/http?host={WebUtility.UrlEncode(RestApiHost)}&path={WebUtility.UrlEncode(RestApiPath)}";
            }
        }

        [MaxLength(16)]
        public String RequestMethod { get; set; }

        [MaxLength(64)]
        public String RedirectUrl { get; set; }

        public override bool IsExist<T>(Database dc)
        {
            return dc.Table<ViewAction>().Any(x => x.ViewId == ViewId && x.Name == Name);
        }
    }
}
