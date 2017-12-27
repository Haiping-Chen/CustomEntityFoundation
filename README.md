# Custom Entity Foundation

Custom Entity Foundation (CEF) is a business oriented data structure abstract layer to interactive with data storage in high flexible customized entity mapping library wirtten in C# .Net Core. It could be used in any .net core project to serve as an entity service. 

### Key Concept
Generic Entity (Model) + Fields (Custom Model Properties) = Bundle (Business Object)

### Features

* Support variety databases: SQL Server, MySql, Sqlite, InMemory
* Complete Restful APIs
* Build-In Text, DateTime, Decimal, Address and EntityReference field
* Easy to integrate with your current .net core project
* Dynamic entity properties customization
* Support adding new field type

### Install
Intall [CEF](https://www.nuget.org/packages/CustomEntityFoundation) with NuGet
````sh
Install-Package CustomEntityFoundation
````
Install [CEF REST API](https://www.nuget.org/packages/CustomEntityFoundation.RestApi) with NuGet
````sh
Install-Package CustomEntityFoundation.RestApi
````
### Developer References & Documents
For more detail please refer to [Developer Document](https://readthedocs.org/projects/customentityfoundation/)
### Usage
#### Construct Business Object Structure
```cs
// Construct a business oriented bundle type
Bundle bundle = new Bundle
{
    Name = $"Pizza Order",
    Description = "This is a business data structure as a Pizza Order",
    EntityName = "Node",
    Id = BUNDLE_ID_PIZZA_ORDER,
    Fields = new List<FieldInBundle>()
};

// Config business specific properties
bundle.Fields.AddRange(new List<FieldInBundle>{
    new FieldInBundle
    {
        FieldTypeName = "Text",
        Name = "CustomerName",
        Caption = "Customer Name",
        Description = "For customer name displayed on ticket"
    },

    new FieldInBundle
    {
        FieldTypeName = "EntityReference",
        Name = "PizzaType",
        Caption = "Pizza Type",
        RefBundleId = PizzaOrder.BUNDLE_ID_PIZZA_ORDER,
        IsMultiple = true
    },

    new FieldInBundle
    {
        FieldTypeName = "DateTime",
        Name = "DueDate",
        Caption = "Due Date"
    }
});
````
#### Persistent Business Data
````cs
// Create business object instance
var record = JObject.FromObject(new
{
    Name = $"Order #1",
    Description = "No description",
    CustomerName = $"Haiping {DateTime.UtcNow.Year} 1",
    PizzaType = new List<String>
    {
    	PizzaType.BUNDLE_ID_PIZZA_TYPE_1 
    },
    DueDate = DateTime.UtcNow.AddDays(3),
    Amount = 3
});

// Persistent object to storage
int rows = dc.DbTran(() =>
{
	node = bundle.AddRecord(dc, record);
});
````
#### Load Data
````cs
// Load data
var loadedNode = dc.Bundle.Find(node.BundleId).LoadRecord(dc, node.Id);

// Or convert to the friendly business object
var bo = loadedNode.ToBusinessObject(dc, bundle.EntityName);
            
````
### Benchmarks
#### Read

| Name                   | Milliseconds | Total |
|------------------------|--------------|---------|
| LoadRecord | 18795        | 100 |
| ToBusinessObject   | 589          | 2000    |  

#### Write

### Feedback
Please contact <haiping008@gmail.com> if you have any questions.
