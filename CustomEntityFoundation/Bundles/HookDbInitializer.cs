using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EntityFrameworkCore.BootKit;

namespace CustomEntityFoundation.Bundles
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 10;

        public void Load(IConfiguration config, Database dc)
        {
            Directory.GetFiles(CefOptions.ContentRootPath + "\\App_Data\\DbInitializer", "*.Bundles.json")
                .ToList()
                .ForEach(path =>
                {
                    string json = File.ReadAllText(path);
                    var dbContent = JsonConvert.DeserializeObject<JObject>(json);

                    if (dbContent["bundles"] != null)
                    {
                        InitBundles(dc, dbContent["bundles"].ToList());
                    }
                });
        }

        private void InitBundles(Database dc, List<JToken> jBundles)
        {
            jBundles.ToList().ForEach(bundle =>
            {
                var entity = bundle.ToObject<Bundle>();
                if (!dc.Table<Bundle>().Any(x => x.Id == entity.Id))
                {
                    // set default caption
                    if (entity.Fields != null)
                    {
                        entity.Fields
                            .Where(x => String.IsNullOrEmpty(x.Caption))
                            .ToList()
                            .ForEach(field => field.Caption = field.Name);
                    }

                    dc.Table<Bundle>().Add(entity);
                }
            });
        }
    }
}
