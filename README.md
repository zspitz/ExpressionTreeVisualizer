# Expression Tree Visualizer

> **NOTE: The visualizer does not currently work with .NET Core or .NET Standard projects, pending the resolution of [this issue](https://developercommunity.visualstudio.com/content/problem/562662/vs2019-custom-debugger-visualizer-for-net-fx-and-c.html#comment-865316). Use the [1.5.67 release](https://github.com/zspitz/ExpressionTreeVisualizer/releases/tag/1.5.67) and copy the visualizer DLL to a subfolder named `netstandard2.0`.**
>
> ExpressionTreeToString, the string representation library, has been moved [to its own repo](https://github.com/zspitz/ExpressionTreeToString). 

[![AppVeyor build status](https://img.shields.io/appveyor/ci/zspitz/expressiontostring?style=flat&max-age=86400)](https://ci.appveyor.com/project/zspitz/expressiontostring) [![Tests](https://img.shields.io/appveyor/tests/zspitz/expressiontostring?compact_message&style=flat&max-age=86400)](https://ci.appveyor.com/project/zspitz/expressiontostring) [![GitHub Release](https://img.shields.io/github/release/zspitz/expressiontostring.svg?style=flat&max-age=86400)](https://github.com/zspitz/ExpressionToString/releases)

This project provides a custom debugging visualizer for expression trees that can be used with Visual Studio (on Windows). The UI consists of:

1. a graphical treeview of the expression tree structure,
2. source code representation of the tree, and
3. end nodes -- nodes in the tree which are not composed of other expressions: parameters, closure variables, constants and default values

![Screenshot](screenshot-01.png)

You can switch formatters without reloading the visualizer:

![Language switch](formatter-switch.gif)

Selection syncing:

* when selecting from the tree:

  ![Selection sync from tree](sync-from-tree.gif)

* from source code:

  ![Selection sync from source code](sync-from-code.gif)

* and from end nodes:

  ![Selection sync from end nodes](sync-from-endnodes.gif)
  
# Installation

The visualizer DLL and the dependent DLL (`ExpressionTreeToString.dll` and `MultiSelectTreeView.DLL`), from the [releases page](https://github.com/zspitz/ExpressionToString/releases), should be placed in the appropriate folder, as described [here](https://docs.microsoft.com/en-us/visualstudio/debugger/how-to-install-a-visualizer). It may be necessary to [unblock the DLLs, and/or to put a copy of them in a subfolder](https://github.com/zspitz/ExpressionToString/wiki/Troubleshooting-visualizer-installation).

# Feedback

* Star the project
* File an [issue](https://github.com/zspitz/ExpressionToString/issues)

# Credits

* John M. Wright's series on [writing debugger visualizers](https://wrightfully.com/writing-a-readonly-debugger-visualizer)
* Multiple-selection treeview is provided by [MultiSelectTreeView](https://github.com/ygoe/MultiSelectTreeView)
* [ReadableExpressions](https://github.com/agileobjects/ReadableExpressions)
* [Greenshot](https://getgreenshot.org/) and [ScreenToGIF](https://www.screentogif.com/) for the screenshots
