# Complex Find & Replace extension for Visual Studio

## About

ComplexReplace is a tool to facilitate your refactoring efforts. The goal is to allow replacing a set of strings with another set of strings with a single click of a button within an active file. The dialog contents are also persistent throughout VS instances, making it a lot easier to use for this purpose than default Find & Replace.

## Example Usage

You've created some properties for your class members, additionally you've renamed some attributes and enums. For some reason the change did not propagate accross 4 dozen files that you have. You can use Complex Replace to (relatively pain-free) update these files one-by-one using the same pattern via a single click (for each file).

![Example](https://github.com/chebz/ComplexReplace/blob/master/preview1.PNG)

In the above example:
1. All instances of "injectionBinder" (within an active document) will be replaced with "InjectionBinder" (capitalized).
2. "commandBinder" -> "CommandBinder"
3. "Relationship." -> "SceneRelationshipType."
4. "[Relationship" -> "[SceneRelationship"

The line number of searched string must match the line number of replacement string.

Click "Replace" for each file with the same problem, and you're done.

There are many possible ways to extend and improve it, but I don't have the time or patience to dig through VS shell API. You feel free though.
