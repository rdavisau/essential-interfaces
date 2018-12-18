# essential-interfaces-generator
Generates interfaces for the [Xamarin.Essentials](https://github.com/xamarin/Essentials) library. 

If you're after the interfaces themselves, you can install them from [NuGet](https://www.nuget.org/packages/Xamarin.Essentials.Interfaces): 

`
Install-Package Xamarin.Essentials.Interfaces
`

You can also grab the raw source outputs from [here](https://essential-interfaces.azurewebsites.net/) (see the end of this readme for a few more details). 

#### Why would I want this? 
The Xamarin.Essentials library is a great initiative by the Xamarin team to provide a simple, consolidated, endorsed and low-overhead set of cross-platform apis for mobile applications. As a core design decision, the Essentials library does not include any interfaces - [for several reasons](https://github.com/xamarin/Essentials/wiki/FAQ-%7C-Essentials#where-are-the-interfaces) - and all features are accessed via static methods, properties and events. If you use dependency injection in your mobile apps, you may miss the interfaces that the 'old skool' plugins for Xamarin typically shipped with - fret not, essential-interfaces-generator is here! 

#### How does it work? 
essential-interface-generator reads the Xamarin.Essentials source and generates an intermediate model representing the apis exposed by Xamarin.Essentials (`RoslynModelGenerator`). It then uses that model to generate C# code containing matching interface definitions for the apis, and implementations for interfaces that forward members on to the static classes provided by Xamarin Essentials (`ImplementationGenerator`). These are dropped into an output project with some basic version information (`ProjectMutator`) and packed for NuGet.

#### How do I use the interfaces?
Install the NuGet package and get registering! Each Xamarin.Essentials api has a matching interface and implementation in the `Xamarin.Essentials.Interfaces` and `Xamarin.Essentials.Implementation` namespaces respectively. These follow a basic pattern:

*Xamarin.Essentials API*: `Thing`
*Interface*: `IThing`
*Implementation*: `ThingImplementation`

Knowing this, you can register each implementation you need with its matching interface. For example:

```cs 
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

builder.Register<IAccelerometer, AccelerometerImplementation>();
builder.Register<IBattery, BatteryImplementation>();
```

Alternatively, you can use the marker interface `IEssentialsImplementation` (adopted by all implementation classes) and reflection to find and register all implementation classes at once. When using this approach, you may need to hint the linker to preserve the implementations (since they are never directly referenced). The technique typically used for this purpose is [falseflagging](https://docs.microsoft.com/en-us/xamarin/android/deploy-test/linker#falseflag).

For mocking, just work as you would with any other mocks:
```cs 
var mockGeo = new Mock<IGeocoding>();
mockGeo.Setup(x => x.GetPlacemarksAsync(It.IsAny<double>(), It.IsAny<double>()))
       .ReturnsAsync(Enumerable.Empty<Placemark>());
// etc.
``` 

#### Does it play nice with the Mono Linker? 
Yes. The generated assembly is marked with the `LinkerSafe` attribute, making it eligible for linking regardless of the link level of your consuming project. Only the types and members you invoke from your application are included in compiled outputs, even if you register them all. You can verify this using a dissassembler like dotPeek. 

#### Is reading from the source directly a bit brittle? 
Probably. The generator makes assumptions based on the current conventions in the repo. Since it is being run against each commit in the Xamarin.Essentials repo, any breaking changes should become clear quickly enough. 

#### I just want to get the raw output!
No problems, you totally can! The interfaces and implementations being generated off each commit to Xamarin.Essentials are available [here](https://essential-interfaces.azurewebsites.net/). That's a mono/WASM page so give it a few seconds to load. 
