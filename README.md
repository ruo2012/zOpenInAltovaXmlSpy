[AppVeyorProjectUrl]: https://ci.appveyor.com/project/GregTrevellick/OpenInAltovaXmlSpy
[AppVeyorProjectBuildStatusBadgeSvg]: https://ci.appveyor.com/api/projects/status/33e93co68kooud5r?svg=true
[GitHubRepoURL]: https://github.com/GregTrevellick/OpenInAltovaXmlSpy
[GitHubRepoIssuesURL]: https://github.com/GregTrevellick/OpenInAltovaXmlSpy/issues
[GitHubRepoPullRequestsURL]: https://github.com/GregTrevellick/OpenInAltovaXmlSpy/pulls
[ThirdPartyAppHomePage]: http://www.altova.com/xml-editor/
[ThirdPartyAppOfficialLogo]: http://www.altova.com/images/linktoaltova/xmlspy.png
[VersionNumberBadgeURL]: https://vsmarketplacebadge.apphb.com/version/GregTrevellick.OpenInAltovaXmlSpy.svg
[VSMarketplaceUrl]: https://marketplace.visualstudio.com/items?itemName=GregTrevellick.OpenInAltovaXmlSpy
[VSMarketplaceReviewsUrl]: https://marketplace.visualstudio.com/items?itemName=GregTrevellick.OpenInAltovaXmlSpy#review-details

# Open In Altova XmlSpy

[![Licence](https://img.shields.io/github/license/gittools/gitlink.svg)](/LICENSE.txt)
[![Build status](https://ci.appveyor.com/api/projects/status/33e93co68kooud5r?svg=true)](https://ci.appveyor.com/project/GregTrevellick/OpenInAltovaXmlSpy)
![](https://vsmarketplacebadge.apphb.com/version/GregTrevellick.OpenInAltovaXmlSpy.svg)

Download this extension from the [VS Marketplace](https://marketplace.visualstudio.com/items?itemName=GregTrevellick.OpenInAltovaXmlSpy).

---------------------------------------

<!--COPY START FOR VS GALLERY-->

A Visual Studio extension to open a file in [Altova XMLSpy XML Editor][ThirdPartyAppHomePage] directly from within Visual Studio.

[![][ThirdPartyAppOfficialLogo]][ThirdPartyAppHomePage]

If you like this *free* extension, please give it a [review][VSMarketplaceReviewsUrl].

See the [change log](CHANGELOG.md) for road map and release history. Bugs can be logged [here][GitHubRepoIssuesURL].

## Features

![](OpenInApp/Resources/ReadMeScreenShot_ContextMenu.png)

- Open multiple files simultaneously with XML Spy directly from within Solution Explorer.

- Warning if attemptimg to open a large quantity of files. The quantity is configurable, and if set very high effectively suppresses the warning altogether.

- Warning if attempting to open one or more files which typically do not contain XML formated content. The list of file extensions that typically constitute XML files is configurable in Tools | Options, and the warning message can be suppressed.

## Options

![](OpenInApp/Resources/ReadMeScreenShot_OptionsGeneral.png)

<!--COPY END FOR VS GALLERY-->

## Contribute

Contributions to this project are welcome by raising an [Issue][GitHubRepoIssuesURL] or submitting a [Pull Request][GitHubRepoPullRequestsURL].

## License

[MIT](/LICENSE.txt)

## Legal

The [owner](https://github.com/GregTrevellick) of this [GitHub repository / software](https://github.com/GregTrevellick/OpenInAltovaXmlSpy) is not affiliated, associated, authorized, endorsed by, employed by, sponsored by, or in any way officially connected with [Altova][ThirdPartyAppHomePage] or any of its subsidiaries or its affiliates.

Nor has [this](https://github.com/GregTrevellick/OpenInAltovaXmlSpy) software been authorised, approved, verified or in anyway assessed by [Altova](https://www.altova.com/company.html), or any of its subsidiaries or its affiliates, either as [raw source code](https://github.com/GregTrevellick/OpenInAltovaXmlSpy) on [GitHub.com](https://github.com/) or as a [Visual Studio Extension](https://marketplace.visualstudio.com/items?itemName=GregTrevellick.OpenInAltovaXmlSpy) in the [Visual Studio Marketplace](https://marketplace.visualstudio.com/vs).

All Trademark, intellectual property rights, and other rights belonging to [Altova][ThirdPartyAppHomePage] as described in [here](https://www.altova.com/legal.html) and [here](https://www.altova.com/eula.html) apply.

All Altova logos and Altova links belong to [Altova][ThirdPartyAppHomePage] and their use here and any associated goodwill inures to [Altova][ThirdPartyAppHomePage] as described [here](https://www.altova.com/link-to-altova.html).

In no event shall [Altova GMBH][ThirdPartyAppHomePage] be liable for any claim, damages or other liability, whether in an action of contract, tort or otherwise, arising from, out of or in connection with the software or the use or dealings in the software.

## Credits

Adapted from code originally written by [Mads Kristensen](https://github.com/madskristensen) [here](https://github.com/madskristensen/OpenInSublimeText/ "Open in Sublime Text") and [here](https://github.com/madskristensen/OpenInVsCode "Open in Visual Studio Code").

Also adapted from code originally written by [Calvin Allen](https://github.com/CalvinAllen) [here](https://github.com/CalvinAllen/OpenInNotepadPlusPlus).

Additional thanks goes to [Build 2016 Conference](https://channel9.msdn.com/Events/Build/2016/B886) and [Visual Studio Toolbox](https://channel9.msdn.com/Shows/Visual-Studio-Toolbox/Extensions-by-Mads-Kristensen).

Thanks also to [Altova][ThirdPartyAppHomePage] themselves, who have additional IDE integration available [here](https://www.altova.com/ide_integration.html).
