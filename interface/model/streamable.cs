namespace Interface.Model {
 interface IStreamable
 {
     public IMessage ConvertToMessage();
     public bool ShouldBroadcast();
 }
}