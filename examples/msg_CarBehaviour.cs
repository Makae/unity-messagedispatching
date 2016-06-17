public class CarBehaviour : MonoBehaviour, IMessageReceiver {

    // Use this for initialization
    void Start () {  
    	// <MORE CODE HERE>
        EntityManager.RegisterEntity("car", this);
        EntityManager.AddToGroup("start", this);

    void IMessageReceiver.OnMessage(Message message) {
        if (message.From == "timing" && message.Content == TimingBehaviour.MSG_START) {
            this.OnStart();
        }
    }
}