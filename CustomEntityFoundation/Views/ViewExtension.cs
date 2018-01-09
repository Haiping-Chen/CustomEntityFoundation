using CustomEntityFoundation.Bundles;
using CustomEntityFoundation.Utilities;
using DotNetToolkit;
using EntityFrameworkCore.BootKit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEntityFoundation.Views
{
    public static class ViewExtension
    {
        public static bool Add(this View viewModel, Database dc)
        {
            if (viewModel.IsExist<View>(dc)) return false;

            dc.Table<View>().Add(viewModel);
            
            return true;
        }

        public static View LoadDefinition(this View viewModel, Database dc)
        {
            var view = dc.Table<View>()
                .Include(x => x.Bundle).ThenInclude(x => x.Fields)
                .Include(x => x.Columns)
                .Include(x => x.Filters)
                .First(x => x.Id == viewModel.Id);

            var bundleEntity = dc.Table<Bundle>().Include(x => x.Fields).FirstOrDefault(m => m.Id == view.BundleId);
            view.Filters.ForEach(filter => {
                filter.BundleFieldId = bundleEntity.Fields.FirstOrDefault(x => x.Name == filter.FieldName).Id;
                filter.Field = bundleEntity.Fields.FirstOrDefault(x => x.Name == filter.FieldName);
            });

            return view;
        }

        /// <summary>
        /// Load records by filter
        /// </summary>
        /// <param name="dm"></param>
        /// <param name="dc"></param>
        /// <returns></returns>
        public static void LoadRecords(this View dm, Database dc, bool fullLoad = false)
        {
            var query = dm.Bundle.QueryRecords(dc);

            dm.Result.LoadRecords(query);

            var columns = fullLoad ?
                        dm.Bundle.Fields.Select(x => new { FieldName = x.Name }).ToList()
                        : dm.Columns.Select(x => new { x.FieldName }).ToList();

            List<Object> items = new List<Object>();

            // assemble records
            dm.Result.Items.ForEach(item =>
            {
                var entity = item as BundleDbRecord;
                entity.Fields = dm.Bundle.Fields;
                entity.LoadEntity(dc, dm.Bundle.EntityName);

                var businessObject = entity.ToBusinessObject(dc, dm.Bundle.EntityName);

                // only expose data defined in view columns
                var row = new JObject();

                columns.ForEach(column =>
                {
                    if (!row.Children().Any(x => x.Path == column.FieldName))
                    {
                        row.Add(column.FieldName, businessObject[column.FieldName]);
                    }
                });

                items.Add(row);
            });

            dm.Result.Items = items;
        }

        public static void ExtractViewFilters(this View view, Database dc, JObject model)
        {
            model["filters"].Children().ToList().ForEach(filter =>
            {
                if(!String.IsNullOrEmpty(filter.ToString()))
                {
                    var viewFilter = filter.ToObject<ViewFilter>();

                    var existedViewFilter = view.Filters.FirstOrDefault(x => x.FieldName == viewFilter.FieldName);
                    if (existedViewFilter == null)
                    {
                        view.Filters.Add(viewFilter);
                    }
                    else
                    {
                        existedViewFilter.Data = viewFilter.Data;
                    }
                }
            });

        }
    }
}
