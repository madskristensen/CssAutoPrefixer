# CSS AutoPrefixer

[![Build status](https://ci.appveyor.com/api/projects/status/dr60r5gqjd7jjnf4?svg=true)](https://ci.appveyor.com/project/madskristensen/cssautoprefixer)

Download this extension from the [Marketplace](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.CSSAutoPrefixer)
or get the [CI build](http://vsixgallery.com/extension/J1da7ad9e-85b3-4a0c-8e45-b2ae59a575a7/).

---------------------------------------

Write your CSS rules without vendor prefixes (in fact, forget about them entirely). Autoprefixer will use the data based on current browser popularity and property support to apply prefixes for you.

See the [change log](CHANGELOG.md) for changes and road map.

## Features

- Auto-prefixes CSS properties
- Uses [autoprefixer](https://github.com/postcss/autoprefixer) node module

### Auto-prefix
This extension calls the [autoprefixer](https://github.com/postcss/autoprefixer) node module behind the scenes to add missing vendor prefixes to any CSS document.

For example, take the following code:

```css
:fullscreen a {
    display: flex
}
```

Running auto-prefixing makes sure that all vendor specific syntaxes are added.

```css
:-webkit-full-screen a {
    display: -webkit-box;
    display: flex
}
:-moz-full-screen a {
    display: flex
}
:-ms-fullscreen a {
    display: -ms-flexbox;
    display: flex
}
:fullscreen a {
    display: -webkit-box;
    display: -ms-flexbox;
    display: flex
}
```

Invoke the command from the context menu in the CSS editor.

![Context Menu](art/context-menu.png)

## Contribute
Check out the [contribution guidelines](.github/CONTRIBUTING.md)
if you want to contribute to this project.

For cloning and building this project yourself, make sure
to install the
[Extensibility Tools 2015](https://visualstudiogallery.msdn.microsoft.com/ab39a092-1343-46e2-b0f1-6a3f91155aa6)
extension for Visual Studio which enables some features
used by this project.

## License
[Apache 2.0](LICENSE)