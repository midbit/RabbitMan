using Interface.Model;
using System.Threading.Tasks;

namespace Interface.Service {
    interface IBroadcastService {
        public Task<string> Broadcast(IMessage tweet);
    }
}