# TogglOn

## Featuretoggle framework 

### Configuration in Startup.cs :

```
public IServiceProvider ConfigureServices(IServiceCollection services)
{
	services
		.AddTogglOnCore(options => options.UseMongoDb("mongodb://localhost:27017"))
		.AddClient(options => options.UseInProcClient());
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
	app.UseTogglOnClient(togglOn =>
    	{
		togglOn.DeclareNamespace("DevOps");
		togglOn.DeclareEnvironment(env.EnvironmentName);
		togglOn.DeclareFeatureGroups(groups => 
		{
			groups.WithGroup("office")
            		groups.WithCustomerIds("1")
            		groups.WithClientIps("127.0.0.1");
		});
		togglOn.DeclareFeatureToggles(toggles => 
		{
			toggles.WithToggle("my-awesome-feature", true)
                	.WhenAny(toggle =>
                	{
                    		toggle.WithFeatureGroup("office");
                    		toggle.WithPercentage(50);
                	});
		});
	});
}
```

### Evaluate featuretoggle

```
public IActionResult Index([FromServices] IFeatureToggleEvaluater evaluater)
{
    if (evaluater.IsEnabled("my-awesome-feature")) return View();

    return NotFound();
}
```
