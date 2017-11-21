# .NET AREA 2017
## About

EPITECH Project (3rd year) in .NET Programing module : Interface that allow you to automatise you e-life.
IFTT like.

## Conventions

### Modules of the project
The project is composed of project where the name is prefixed by "Module{Name}"
These modules are an abstraction of "social media" calls.
Own project is composed of call of type action or reaction.

In this project the reactions calls will respect this schema
```
namespace ModuleGmail
{
    public class ModuleGmail : AModule
    {
    	[...]
    	/**
         * Reactions
         */
        public static string ReactionSendMessage(User user, string message)
        {
        	[...]
        }
    	[...]
    }
}
```

### Git
All the commit will respect this format :
```
[{type}] [{scope}] {CommitTitle}

{CommitDescription}

```

The format of the {type} must respect this enumeration :
* "+" for commit a feature, improvement
* "-" for commit a revert or a deletion
* "*" for commit a fix, hotfix
* "M" for commit a merge

The {Scope}, {CommitTitle} and {CommitDescription} must be written in comprehensible english.

## Dependencies
### JsonSerialization
https://github.com/codecutout/JsonApiSerializer/
### MySqlConnection
https://www.nuget.org/packages/MySqlConnector/
### Orm
https://github.com/StackExchange/Dapper/