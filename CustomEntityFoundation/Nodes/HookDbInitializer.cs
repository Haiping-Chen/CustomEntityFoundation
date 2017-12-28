using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.BootKit;
using CustomEntityFoundation.Bundles;

namespace CustomEntityFoundation.Nodes
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 101;

        public void Load(IConfiguration config, Database dc)
        {
            Directory.GetFiles(CefOptions.ContentRootPath + "\\App_Data\\DbInitializer", "*.Nodes.json")
                .ToList()
                .ForEach(path =>
                {
                    string json = File.ReadAllText(path);
                    var dbContent = JsonConvert.DeserializeObject<JObject>(json);

                    if (dbContent["nodes"] != null)
                    {
                        InitNodes(dc, dbContent.GetValue("nodes").ToList());
                    }
                });
        }

        private void InitNodes(Database dc, List<JToken> records)
        {
            records.ForEach(entity =>
            {
                var bundle = dc.Table<Bundle>().Include(x => x.Fields).First(x => x.Id == entity["bundleId"].ToString());

                bundle.AddRecord(dc, JObject.FromObject(entity));
            });
        }
    }
}
