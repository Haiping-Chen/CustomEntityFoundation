using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace CustomEntityFoundation.Nodes
{
    public class HookDbInitializer : IHookDbInitializer
    {
        public int Priority => 101;

        public void Load(IConfiguration config, EntityDbContext dc)
        {
            Directory.GetFiles(EntityDbContext.Options.ContentRootPath + "\\App_Data\\DbInitializer", "*.Nodes.json")
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

        private void InitNodes(EntityDbContext dc, List<JToken> records)
        {
            records.ForEach(entity =>
            {
                var bundle = dc.Bundle.Include(x => x.Fields).First(x => x.Id == entity["bundleId"].ToString());

                bundle.AddRecord(dc, JObject.FromObject(entity));
            });
        }
    }
}
