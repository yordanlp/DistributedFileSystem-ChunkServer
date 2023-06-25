using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChunkServer {
    public class Chunk {
        public int Id { get; set; }
        public string Data { get; set; }
    }
}
