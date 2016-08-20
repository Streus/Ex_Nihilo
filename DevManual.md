# Developer Manual / List of coding paradigms in use

## If you want to:

### Access a specific instance of a Manager-like object (GameManager, BackgroundManager, *Manager)...

...They should be subclasses of the Singleton<T> class, with the T being the type of the manager itself. If that is the case, then just call *Manager.Instance to grab that instance. Most variables should be public/static anyway, however.

### Create a Manager-like object...

...Make sure to subclass Singleton<T>, passing in the name of the manager class as T. Furthermore, make sure to name it as *Manager, and keep it in the "Game" folder. 

### Create *any* GameObject...

...Use the Game.create(string) method, or any of the overloaded methods for your needs (like position, rotation, etc). This allows the game to keep track of the variables in a faster way than default Unity's methods. Be sure to destroy it using Game.destroy(GameObject) rather than Destroy(GameObject) for the same reason- otherwise you'll be getting strange null errors everywhere when the game tries to access any regular GameObject.

### Destroy *any* GameObject...

...Do not use Destroy(GameObject); instead use Game.destroy(GameObject). This lets the game internally keep track of everything and work faster. If you ignore this, you will get null errors frequently.