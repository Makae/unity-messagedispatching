/*
 Wichtig: 
  - erbt von IMessage Receiver
  - Implementiert IMessageReceiver.OnMesasge
*/
public class TimingBehaviour : MonoBehaviour, IMessageReceiver
{
    public static string MSG_START = "S";
    public static string MSG_DELAYED = "D";


    // <MORE CODE HERE>

    void Start() {
        EntityManager.RegisterEntity("timing", this);
        // <MORE CODE HERE>
    }

    void IMessageReceiver.OnMessage(Message msg) {
        if (msg.From != "timing" || msg.Content != TimingBehaviour.MSG_DELAYED)
            return;
        lblCountDown.enabled = false;
    }


// GameStart CoRoutine
IEnumerator GameStart() {
        // <MORE CODE HERE>
        
        MessageDispatcher.Instance().Dispatch(0,     "timing", "group:start", TimingBehaviour.MSG_START);
        MessageDispatcher.Instance().Dispatch(0.75f, "timing", "timing",      TimingBehaviour.MSG_DELAYED);
    }
}