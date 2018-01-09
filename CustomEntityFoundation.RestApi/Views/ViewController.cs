using CustomEntityFoundation.Utilities;
using CustomEntityFoundation.Views;
using DotNetToolkit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomEntityFoundation.RestApi.Views
{
    public class ViewController : EntityFoundationController
    {
        [HttpGet("{id}")]
        public View GetView(string id)
        {
            var dm = new View { Id = id };
            dm.LoadDefinition(dc);

            return dm;
        }

        [HttpGet("Query")]
        public PageResult<View> GetViews(string name, [FromQuery] int page = 1)
        {
            var query = dc.Table<View>().AsQueryable();
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            return new PageResult<View>() { Page = page }.LoadRecords(query);
        }

        [HttpPost("{viewId}/execute")]
        public View ExecuteByFilter([FromRoute] string viewId, [FromBody] JObject filters, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var view = new View { Id = viewId }.LoadDefinition(dc);
            view.Result = new PageResult<Object> { Page = page, Size = size, Items = new List<Object>() };
            view.ExtractViewFilters(dc, filters);
            view.LoadRecords(dc);

            return view;
        }

        [HttpGet("{viewId}/execute")]
        public View Execute([FromRoute] string viewId, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var view = new View { Id = viewId }.LoadDefinition(dc);
            view.Result = new PageResult<Object> { Page = page, Size = size, Items = new List<Object>() };
            view.LoadRecords(dc);

            return view;
        }
    }
}
