# Getting Started

We understand this may be a stressful experience for you. 
We've all been there; We know it's hard. 

That's why we are providing you with some additional help, to make life a little easier. 

## Compilation / Running

There are shell scripts provided for both Compilation and Run (Watch) of your application. 

`build.sh` - Restores and Builds the Application and Runs any Unit Tests

`run.sh` - Restores and Runs the Application in a "Watch" mode. This will recompile any changes on the fly. 

## Connecting To Azure Table Storage. 

This machine has already been setup with Connection String Environment Variable that will allow you to connect to Azure Table Storage. 

Name of the Environment Variable is: `TableStorage` and can be accessed the `GetConnectionString` method on the `IConfiguration` interface.

#### Example:

```
    public MyClass(IConfiguration configuration) 
    {
        var connectionString = configuration.GetConnectionString("TableStorage");
    }
```

## Model State Helper

During the course of the task you will need to return ModelState Validation Errors as a `Dictionary<string,string>` for JSON Serialization.

We have provided a Static Extension Method `ModelStateDictionary.ToDictionary()` that will simplify this task. 

#### Example:

```
    public IActionResult Post(PostModel model)
    {
        if (ModelState.IsValid())
        {
            // Do Something
        }

        var errors = ModelState.ToDictionary();

        return BadRequest(errors);
    }
```

## Unit Testing

An example Unit Testing Project has been provided if you wish to write Unit Tests. 
