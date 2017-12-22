using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CustomEntityFoundation;

namespace CustomEntityFoundation.Views
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 110;

        public void Load(IConfiguration config, EntityDbContext dc)
        {
            Directory.GetFiles(EntityDbContext.Options.ContentRootPath + "\\App_Data\\DbInitializer", "*.Views.json")
                .ToList()
                .ForEach(path =>
                {
                    string json = File.ReadAllText(path);
                    var dbContent = JsonConvert.DeserializeObject<JObject>(json);

                    if(dbContent["views"] != null)
                    {
                        InitViews(dc, dbContent["views"].ToList());
                    }
                });
        }

        private void InitViews(EntityDbContext dc, List<JToken> jViews)
        {
            jViews.ForEach(jView => {
                var dmView =jView.ToObject<View>();
                dmView.Add(dc);
            });
        }
    }
}
