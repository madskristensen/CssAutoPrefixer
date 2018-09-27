using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace CssAutoPrefixer
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [Guid(PackageGuids.guidPackageString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class CssAutoPrefixPackage : AsyncPackage
    {

    }
}
