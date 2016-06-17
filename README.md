# unity-messagedispatching
Message Behaviours implemented in Unity according to Mat Buckland

# Installation
- The Folder has to be added inside the Plugin folde


# Usage
- Create empty Game Object called "UnityEventRedispatcher"
- Add Script "UnityEventRedispatcher" to the GameObject

## Set correct Interface
A Message Receiver has to:
 - Implement the Interface from **IMessageReceiver**
 - Register itself in the **Start()** Method as an Entity to the EntityManager either by **group** or by **name**
 
### Examples
#### Self registration and Sending Messages
```C#
public class TimingBehaviour : MonoBehaviour, IMessageReceiver {
    public static string MSG_START = "S";
    public static string MSG_DELAYED = "D";

    // <MORE CODE HERE>
    void Start() {
        EntityManager.RegisterEntity("timing", this);
        // <MORE CODE HERE>
    }
    
    void GameStart() {
        // <MORE CODE HERE>  
        MessageDispatcher.Instance().Dispatch(0,     "timing", "group:start", TimingBehaviour.MSG_START);
        MessageDispatcher.Instance().Dispatch(0.75f, "timing", "timing",      TimingBehaviour.MSG_DELAYED);
    }
    
    void IMessageReceiver.OnMessage(Message msg) {
        if (msg.From != "timing" || msg.Content != TimingBehaviour.MSG_DELAYED)
            return;
        // <EXECUTE CODE FOR THIS EVENT>
        // Eg. Start stop watch
    }


}
```
#### Message Receiver in a Group
```C#
public class CarBehaviour : MonoBehaviour, IMessageReceiver {
    void Start () {  
    	// <MORE CODE HERE>
        EntityManager.RegisterEntity("car", this);
        EntityManager.AddToGroup("start", this);
    }
    // <MORE CODE HERE>
    void IMessageReceiver.OnMessage(Message message) {
        if (message.From == "timing" && message.Content == TimingBehaviour.MSG_START) {
            this.OnStart();
        }
    }
}
```

Infos zum Code:

Registrierung eines IMessageReceiver beim EntityManager: (Beispiel in "msg_CarBehaviour.cs")
 - Entity: EntityManager.RegisterEntity(ENTITY_NAME, this);
 - Gruppe: EntityManager.AddToGroup(GROUP_NAME, this); // ohne "group:"-prefix

Für das abschicken einer Nachricht hat es ein Beispiel bei "msg_TimingBehaviour.cs".
- Gruppen-Namen sind prefixed mit "group:"

Der EntityManager bietet ausserdem das auslesen von registrierten Entities oder Gruppen.
