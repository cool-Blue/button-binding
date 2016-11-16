# Customising Controls with WPF — a learning curve!  
Coming in to Windows Presentation Foundation for the first time, I found it hard to get a big picture view of canonical patterns. Here is a record of my progress…

I needed a panel of buttons to control the state and manage the content of a remote logger and some of the buttons needed to be toggles, so, they change state (between checked and unchecked) every time they are clicked. I thought I would just quickly modify the visual behaviour of the standard WPF buttons to change their content depending on their `IsCheckedstate`.

My first attempt was to write some code to contain the two-item list of content and to manage the state as generically as possible. The only reason I took this route was because I kind of understand C# but I had no clue about WPF and the XAML environment and to me, the mark-up looked ugly and verbose and hard to read. Having said that, _**don’t do it this way**_: I just include it as part of the journey to the correct way to do it. The techniques are useful for creating View Models so it’s not a wasted journey and I will present the more idiomatically correct way after.

So, anyway, I created some classes in C# to facilitate _DataBinding_ between code and XAML.

## Data Binding
The whole idea of data binding is to have visual content that dynamically links to some state in the code. So, there needs to be a mechanism to notify consumers when the source object changes and this behaviour is provided by the [INotifyPropertyChanged](https://medium.com/r/?url=https%3A%2F%2Fmsdn.microsoft.com%2Fen-us%2Flibrary%2Fsystem.componentmodel.inotifypropertychanged%2528v%3Dvs.110%2529.aspx%3Ff%3D255%26MSPPError%3D-2147217396) interface in WPF. I also had a quick look at [DependencyProperties](https://medium.com/r/?url=https%3A%2F%2Fmsdn.microsoft.com%2Fen-us%2Flibrary%2Fms752914%2528v%3Dvs.110%2529.aspx%3Ff%3D255%26MSPPError%3D-2147217396) as an alternate binding approach, but could not find any benefit in my case.


---

### INotifyPropertyChanged  
I was experimenting with ToggleButtons and ordinary Buttons to figure out which way was best. My thinking was that you could eliminate some state from the code behind by using a toggle button. Because of this, I needed a slightly different wrapper for each button type so I tried to build it as DRY as possible utilising inheritance.
![Image](img/INotify.jpg)
This allowed me to create `ObservableButton` and `ObservableToggleButton` instances in the View which I could bind to the XAML component of the View. I also wanted to have an extra button, to activate another button programmatically. I considered this to be View Model part of the system because its driven by outside stimuli. This is what `button2` is for.


---

### ICommand  
The other aspect of binding between XAML and code behind is Commanding and this is supported by the [ICommand interface]()https://medium.com/r/?url=https%3A%2F%2Fmsdn.microsoft.com%2Fen-us%2Flibrary%2Fsystem.windows.input.icommand%28v%3Dvs.110%29.aspx . This is the method I chose to automate clicking a button. Basically it uses the `Command` property that some controls have to call some code behind from the XAML node. The class I created for this uses a thing called an _AutomationPeer_ to pragmatically ‘press’ the target button and it exposes a method called `Push` that wraps the _AutomationPeer_ in a class that implements the `Icommand` interface using an `Action` delegate to keep things DRY.
![Image](img/ICommand.jpg)
### MVVM  
Then I could write the code behind for my View component. Reading about the best practice in WPF lead me (very quickly) to the [Model View View Model](https://medium.com/r/?url=https%3A%2F%2Fmsdn.microsoft.com%2Fen-us%2Flibrary%2Fhh848246.aspx%3Ff%3D255%26MSPPError%3D-2147217396) pattern: this is the preferred way to go for many many reasons that are outside of the scope of this blog but, figuring out how to comply with this principle, in a practical sense, occupied me for some time and continues to do so.

The code I was writing (wrongly as it turns out) was all about the View, that is, state and behaviour associated with the UI. Because I was doing it wrong it presented a bit of a conundrum.

WPF provides a natural way of referencing between the XAML and code behind elements of the View via a concept called DataContext and dependency properties.

Controls that are instantiated by declaration in the XAML, can be directly referenced in the code behind as plain old CLR objects (POCO’s). This is managed, by the WPF property system, by wrapping CLR accessors around _Dependency Properties_ on the controls. A _Dependency Object_ provides methods for registering and querying the dependency properties that are stored in it as _key value_ pairs in a Dictionary. The bottom line is that XAML nodes and their properties can be referenced by name in the code behind.

In order to reference a class (and it’s members) that is declared in code behind, from the XAML, is a little bit more complicated. The objects to be exposed need to be added onto a property of the `Window` class called `DataContext`. Everything I read about MVVM suggested that the View Model object should be set on this.

Because I was doing it wrong, I needed to reference View and View model objects from the XAML so, I had to figure out the best option for exposing them. The first thing that came to mind to add an object onto the `DataContext` that had the View and the View Model instances as properties. And I guess this is fine but, I don’t know. It’s not clear to me if this is a bad idea or not but, none of the other kids were doing it so I started investigating alternatives. This lead me to the concept of [XAML Resources](https://medium.com/r/?url=https%3A%2F%2Fmsdn.microsoft.com%2Fen-us%2Flibrary%2Fms750613%2528v%3Dvs.110%2529.aspx%3Ff%3D255%26MSPPError%3D-2147217396) . Every object that derives from `FrameworkElement` has a `Resources` property, that is a ResourceDictionary, which contains _key value_ pairs, where value is a pure `Object` reference. Basically, any object can be added to a `ResourceDictionary`, in XAML, using [Object Element Syntax](https://medium.com/r/?url=https%3A%2F%2Fmsdn.microsoft.com%2Fen-us%2Flibrary%2Fms788723%28v%3Dvs.110%29.aspx%3Ff%3D255%26mspperror%3D-2147217396%23Anchor_2) .  


```XML
<Window.Resources>
    <!--Get a reference to the window to establish View Context-->
    <RelativeSource x:Key="View" Mode="FindAncestor" 
        AncestorType="{x:Type Window}" />
    <!--Custom Button backgrounds-->
    <LinearGradientBrush x:Key="Button.Static.Background.custom"
        EndPoint="0.5,1" StartPoint="0.5,0">
            <GradientStop Color="#FF2C0606" Offset="1"/>
            <GradientStop Color="#FFE6C7C7"/>
    </LinearGradientBrush>
<Window.Resources>
```

Here I’m storing a 


![Image](img/View.cs.jpg)

