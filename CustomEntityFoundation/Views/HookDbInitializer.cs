using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;

namespace CustomEntityFoundation.Views
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 110;

        public void Load(IConfiguration config, Database dc)
        {
            Directory.GetFiles(Database.ContentRootPath + "\\App_Data\\DbInitializer", "*.Views.json")
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

        private void InitViews(Database dc, List<JToken> jViews)
        {
            jViews.ForEach(jView => {
                var dmView =jView.ToObject<View>();
                dmView.Add(dc);
            });
        }
    }
}
